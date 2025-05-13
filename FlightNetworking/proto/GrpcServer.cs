using System.Collections.Concurrent;
using csharp.flightproto;
using FlightModel;
using FlightServices;
using Grpc.Core;
using Manager = FlightModel.Manager;

namespace FlightNetworking.Protos;

public class GrpcServer : FlightService.FlightServiceBase, IFlightObserver
{
    private readonly IFlightServices _flightServices;
    private readonly List<IFlightObserver> _observers = new List<IFlightObserver>();
    
    private readonly List<IServerStreamWriter<ReservationNotification>> _clients = 
        new List<IServerStreamWriter<ReservationNotification>>();
    
    private readonly ConcurrentDictionary<string, IServerStreamWriter<ReservationNotification>> _activeClients = new();



    // Constructorul pentru injectarea dependenței
    public GrpcServer(IFlightServices flightServices)
    {
        _flightServices = flightServices;
    }
    
    
    
    public void ReservationAdded(FlightModel.Reservation reservation)
    {
        var notification = new ReservationNotification
        {
            Message = $"New reservation for {reservation.trip.Destination} by {reservation.ClientName}",
            TripId = reservation.trip.Id
        };

        // Trimitem notificarea către toți clienții conectați
        foreach (var client in _activeClients.Values)
        {
            Task.Run(async () =>
            {
                try
                {
                    await client.WriteAsync(notification);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send notification: {ex.Message}");
                }
            });
        }

        // Notificăm și observatorii vechi (dacă folosești un sistem clasic de observers)
        foreach (var observer in _observers)
        {
            observer.ReservationAdded(reservation);
        }
    }


    // Adăugăm un observator (de exemplu, clientul gRPC)
    public void AddObserver(IFlightObserver observer)
    {
        _observers.Add(observer);
    }

    // Implementarea metodei Login
    public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        try
        {
            // Accesăm managerul din cererea GRPC
            //FlightModel.Manager manager = ManagerConverter.ToFlightModelManager(request.Manager);
            FlightModel.Manager manager = new Manager(request.Manager.Name, request.Manager.Password);

            // Apelăm metoda de login din serviciul IFlightServices, adăugând clientul (acesta este 'this')
            FlightModel.Manager loggedInManager = _flightServices.Login(manager, this);

            // Returnăm un răspuns de succes
            return new LoginResponse
            {
                Success = true,
                Message = "Login successful"
            };
        }
        catch (FlightException ex)
        {
            // În cazul unei erori, returnăm un răspuns cu succes false
            return new LoginResponse
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
    
    public override Task<TripsResponse> GetAllTrips(Empty request, ServerCallContext context)
    {
        var response = new TripsResponse();

        // Preluăm călătoriile din serviciul IFlightServices
        var trips = _flightServices.GetAllTrips();

        // Adăugăm călătoriile în răspuns
        foreach (var trip in trips)
        {
            response.Trips.Add(new csharp.flightproto.Trip
            {
                Id = trip.Id,
                Destination = trip.Destination,
                DepartureTime = trip.DepartureTime.ToString("yyyy-MM-dd'T'HH:mm:ss"),
                Airport = trip.Aeroport,
                NoOfAvailableSeats = (int)trip.NoOfSeatsAvailable 
            });
        }

        return Task.FromResult(response);
    }
    
    public override Task<ManagerResponse> SearchManagerByName(ManagerSearchRequest request, ServerCallContext context)
    {
        try
        {
            // Căutăm managerul pe baza numelui și a parolei
            var manager = _flightServices.SearchManagerByName(request.Name, request.Password);

            // Dacă managerul este găsit, returnăm un răspuns de succes
            return Task.FromResult(new ManagerResponse
            {
                Found = true,
                Manager = new csharp.flightproto.Manager
                {
                    Name = manager.Name,
                    Password = manager.Password
                }
            });
        }
        catch (FlightException ex)
        {
            // Dacă există o eroare (de exemplu, managerul nu este găsit)
            return Task.FromResult(new ManagerResponse
            {
                Found = false,
                Error = ex.Message
            });
        }
    }
    
    public override Task<TripResponse> SearchTripById(TripIdRequest request, ServerCallContext context)
    {
        try
        {
            var trip = _flightServices.SearchTripById(request.Id);

            return Task.FromResult(new TripResponse
            {
                Trip = new csharp.flightproto.Trip
                {
                    Id = trip.Id,
                    Destination = trip.Destination,
                    DepartureTime = trip.DepartureTime.ToString("yyyy-MM-dd'T'HH:mm:ss"),
                    Airport = trip.Aeroport,
                    NoOfAvailableSeats = (int)trip.NoOfSeatsAvailable
                }
            });
        }
        catch (FlightException ex)
        {
            return Task.FromResult(new TripResponse
            {
                Error = ex.Message
            });
        }
    }

    public override Task<TripsResponse> SearchTripsByDestination(TripSearchRequest request, ServerCallContext context)
    {
        var response = new TripsResponse();

        try
        {
            var date = DateTime.ParseExact(request.Date, "yyyy-MM-dd", null);
            var trips = _flightServices.SearchTripsByDestination(request.Destination, date);

            foreach (var trip in trips)
            {
                response.Trips.Add(new csharp.flightproto.Trip
                {
                    Id = trip.Id,
                    Destination = trip.Destination,
                    DepartureTime = trip.DepartureTime.ToString("yyyy-MM-dd'T'HH:mm:ss"),
                    Airport = trip.Aeroport,
                    NoOfAvailableSeats = (int)trip.NoOfSeatsAvailable
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        return Task.FromResult(response);
    }

    

    public override Task<AddReservationResponse> AddReservation(AddReservationRequest request, ServerCallContext context)
    {
        try
        {
            var protoReservation = request.Reservation;
            var modelTrip = _flightServices.SearchTripById(protoReservation.TripId);

            // Creați un obiect Reservation
            var modelReservation = new FlightModel.Reservation(
                modelTrip,                          // Trip-ul corespunzător
                protoReservation.NoOfSeats,          // Numărul de locuri rezervate
                protoReservation.TouristName
            );

            // Adăugăm rezervarea în serviciu
            _flightServices.AddReservation(modelReservation);


            return Task.FromResult(new AddReservationResponse { Success = true });
        }
        catch (FlightException ex)
        {
            return Task.FromResult(new AddReservationResponse
            {
                Success = false,
                Error = ex.Message
            });
        }
    }
    
    public override Task<Empty> Logout(LogoutRequest request, ServerCallContext context)
    {
        var manager = new FlightModel.Manager(request.Manager.Name, request.Manager.Password);
        _flightServices.Logout(manager, this);
        return Task.FromResult(new Empty());
    }
    
    public override async Task ReservationStream(ReservationNotificationRequest request, IServerStreamWriter<ReservationNotification> responseStream, ServerCallContext context)
    {
        var managerName = request.ManagerName;

        // Adăugăm clientul în lista de streamuri active
        _activeClients[managerName] = responseStream;

        try
        {
            // Ținem streamul deschis până clientul se deconectează
            while (!context.CancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000); // simplă pauză, streamul e ținut deschis
            }
        }
        finally
        {
            _activeClients.TryRemove(managerName, out _); // eliminăm clientul la deconectare
        }
    }



}