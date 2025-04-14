using LabMPP.domain;
using LabMPP.repository.interfaces;
using log4net;

namespace LabMPP.service;

public class Service
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(Service));

    private ITripRepo _tripRepo;
    private IManagerRepo _managerRepo;
    private IReservationRepo _reservationRepo;

    public Service(ITripRepo tripRepo, IManagerRepo managerRepo, IReservationRepo reservationRepo)
    {
        this._tripRepo = tripRepo;
        this._managerRepo = managerRepo;
        this._reservationRepo = reservationRepo;
    }

    public Manager GetManagerByName(string name)
    {
        Log.Info("Getting manager by name");
        IEnumerable<Manager> managers = _managerRepo.FindAll();
        foreach (var manager in managers)
        {
            if(manager.Name == name)
                return manager;
        }
        return null;
    }

    public IEnumerable<Reservation> GetAllReservations()
    {
        Log.Info("Getting all reservations");
        return _reservationRepo.FindAll();
    }

    public IEnumerable<Trip> GetAllTrips()
    {
        Log.Info("Getting all trips");
        return _tripRepo.FindAll();
    }

    public IEnumerable<Trip> GetAllTripsByDestination(string destination, DateTime departureTime)
    {
        Log.Info("Getting all trips by destination");
        IEnumerable<Trip> trips = _tripRepo.FindAll();
        IEnumerable<Trip> finalTrips = trips.Where(trip => trip.Destination == destination && trip.DepartureTime.Date == departureTime.Date);
        return finalTrips;
    }

    public Reservation AddReservation(Reservation reservation)
    {
        Log.Info("Adding reservation");
        return _reservationRepo.Save(reservation);
    }

    public Trip GetTripById(long tripId)
    {
        Log.Info("Getting trip by id");
        return _tripRepo.FindOne(tripId);
    }

    public long GetNoOfSeatsAvailable(Trip trip)
    {
        Log.Info("Getting no of seats available");
        Trip newtrip = _tripRepo.FindOne(trip.Id);
        return newtrip.NoOfSeatsAvailable;
    }

    public Trip ModifyTrip(Trip trip)
    {
        Log.Info("Modifying trip");
        return _tripRepo.Update(trip);
    }
}