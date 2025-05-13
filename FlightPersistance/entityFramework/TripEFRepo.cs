using FlightModel;
using LabMPP.repository.databases;
using LabMPP.repository.interfaces;
using log4net;

namespace FlightPersistance.entityFramework;

public class TripEFRepo : ITripRepo
{
    private readonly MyDbContext _context;

    public TripEFRepo(MyDbContext context)
    {
        _context = context;
    }

    public Trip FindOne(long id)
    {
        return _context.Trips.Find(id);
    }

    public IEnumerable<Trip> FindAll()
    {
        return _context.Trips.ToList();
    }

    public Trip Save(Trip trip)
    {
        _context.Trips.Add(trip);
        _context.SaveChanges();
        return trip;
    }

    public Trip Delete(long id)
    {
        var trip = _context.Trips.Find(id);
        if (trip != null)
        {
            _context.Trips.Remove(trip);
            _context.SaveChanges();
        }
        return trip;
    }

    public Trip Update(Trip trip)
    {
        _context.Trips.Update(trip);
        _context.SaveChanges();
        return trip;
    }
}