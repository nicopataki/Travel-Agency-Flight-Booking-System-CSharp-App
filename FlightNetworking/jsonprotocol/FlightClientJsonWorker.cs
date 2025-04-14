using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using FlightModel;
using FlightServices;

namespace FlightNetworking.jsonprotocol;

public class FlightClientJsonWorker : IFlightObserver
    {
        private IFlightServices server;
        private Socket connection;

        private StreamReader input;
        private StreamWriter output;
        private JsonSerializerOptions options;
        private volatile bool connected;

        public FlightClientJsonWorker(IFlightServices server, Socket connection)
        {
            this.server = server;
            this.connection = connection;
            options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() }
            };
            try
            {
                NetworkStream networkStream = new NetworkStream(this.connection);
                output = new StreamWriter(networkStream);
                input = new StreamReader(networkStream);
                connected = true;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void Run()
        {
            while (connected)
            {
                try
                {
                    string requestLine = input.ReadLine();
                    if (requestLine == null)
                    {
                        Console.WriteLine("Client disconnected.");
                        connected = false;
                        break;
                    }

                    Console.WriteLine("Received request line: " + requestLine);
                    Request request = JsonSerializer.Deserialize<Request>(requestLine, options);
                    Response response = HandleRequest(request);
                    if (response != null)
                    {
                        SendResponse(response);
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("[IO Exception] " + e.Message);
                    connected = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine("[General Exception] " + e.Message);
                    connected = false;
                }

                Thread.Sleep(100); // mai mic, să nu îngreunezi CPU-ul
            }

            try
            {
                input?.Close();
                output?.Close();
                connection?.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine("[Cleanup Error] " + e.Message);
            }
        }


        public void ReservationAdded(Reservation reservation)
        {
            Response response = JsonProtocolUtils.CreateNewReservationResponse(reservation);
            Console.WriteLine("Reservation added " + reservation);
            try
            {
                SendResponse(response);
            }
            catch (IOException e)
            {
                throw new FlightException("Sending error: " + e);
            }
        }

        private static Response okResponse = JsonProtocolUtils.CreateOkResponse();

        private Response HandleRequest(Request request)
        {
            Response response = null;
            if (request.Type == RequestType.LOGIN)
            {
                Console.WriteLine("Login request..." + request.Type);
                Manager reservationManager = DTOUtils.GetFromDTO(request.Manager);
                try
                {
                    server.Login(reservationManager, this);
                    return okResponse;
                }
                catch (FlightException e)
                {
                    return JsonProtocolUtils.CreateErrorResponse(e.Message);
                }
            }
            if (request.Type == RequestType.LOGOUT)
            {
                Console.WriteLine("Logout request..." + request.Type);
                Manager reservationManager = DTOUtils.GetFromDTO(request.Manager);
                try
                {
                    server.Logout(reservationManager, this);
                    connected = false;
                    return okResponse;
                }
                catch (FlightException e)
                {
                    return JsonProtocolUtils.CreateErrorResponse(e.Message);
                }
            }
            if (request.Type == RequestType.GET_ALL_TRIPS)
            {
                Console.WriteLine("Get all trips request..." + request.Type);
                try
                {
                    Trip[] trips = server.GetAllTrips();
                    Console.WriteLine("Got all trips from Worker HandleRequest: ");
                    foreach (Trip trip in trips)
                        Console.WriteLine(trip);
                    return JsonProtocolUtils.CreateGetAllTripsResponse(trips);
                }
                catch (FlightException e)
                {
                    return JsonProtocolUtils.CreateErrorResponse(e.Message);
                }
            }
            if (request.Type == RequestType.SEARCH_TRIP_BY_ID)
            {
                Console.WriteLine("Search trip by id request..." + request.Type);
                if (request.TripId == null)
                    return JsonProtocolUtils.CreateErrorResponse("Trip id in request is null");
                try
                {
                    Trip trip = server.SearchTripById(request.TripId);
                    return JsonProtocolUtils.CreateSearchTripByIdResponse(trip);
                }
                catch (FlightException e)
                {
                    return JsonProtocolUtils.CreateErrorResponse(e.Message);
                }
            }
            if (request.Type == RequestType.ADD_RESERVATION)
            {
                Console.WriteLine("Add reservation request..." + request.Type);
                Reservation reservation = DTOUtils.GetFromDTO(request.Reservation);
                try
                {
                    Reservation reservationAdded = server.AddReservation(reservation);
                    return okResponse;
                }
                catch (FlightException e)
                {
                    return JsonProtocolUtils.CreateErrorResponse(e.Message);
                }
            }
            if (request.Type == RequestType.GET_ALL_TRIPS_BY_DESTINATION)
            {
                Console.WriteLine("Getting trips by destination request..." + request.Type);
                try
                {
                    Trip[] trips = server.SearchTripsByDestination(request.Destination, request.Date);
                    Console.WriteLine("Got all trips from Worker HandleRequest: ");
                    foreach (Trip trip in trips)
                        Console.WriteLine(trip);
                    return JsonProtocolUtils.CreateGetAllTripsByDestinationResponse(trips);
                }
                catch (FlightException e)
                {
                    return JsonProtocolUtils.CreateErrorResponse(e.Message);
                }
            }
            return response;
        }

        private void SendResponse(Response response)
        {
            string responseLine = JsonSerializer.Serialize(response, options);
            Console.WriteLine("Sending response " + responseLine);
            lock (output)
            {
                output.WriteLine(responseLine);
                output.Flush();
            }
        }
    }