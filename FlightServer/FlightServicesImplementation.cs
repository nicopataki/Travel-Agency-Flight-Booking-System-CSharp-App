using System.Runtime.InteropServices.ComTypes;
using FlightModel;
using FlightServices;
using LabMPP.repository.interfaces;

namespace FlightServer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class FlightServicesImplementation : IFlightServices
{
    private readonly IManagerRepo reservationManagerRepository;
    private readonly IReservationRepo reservationRepository;
    private readonly ITripRepo tripRepository;
    
    private readonly Dictionary<string, IFlightObserver> loggedClients;

    public FlightServicesImplementation(IManagerRepo reservationManagerRepository, IReservationRepo reservationRepository, ITripRepo tripRepository)
    {
        this.reservationManagerRepository = reservationManagerRepository;
        this.reservationRepository = reservationRepository;
        this.tripRepository = tripRepository;
        loggedClients = new Dictionary<string, IFlightObserver>();
    }

    public Manager Login(Manager reservationManager, IFlightObserver client)
    {
        Manager foundManager = SearchManagerByName(reservationManager.Name, reservationManager.Password);
        if (foundManager != null)
        {
            if (loggedClients.ContainsKey(reservationManager.Name))
                throw new FlightException("User already logged in.");
            if (reservationManager.Password.Equals(foundManager.Password))
            {
                loggedClients[reservationManager.Name] = client;
                Console.WriteLine("Logged clients: " + string.Join(", ", loggedClients.Keys));
            }
            else
                throw new FlightException("Authentication failed.");
        }
        else
            throw new FlightException("Authentication failed.");
        return foundManager;
    }

    public Manager SearchManagerByName(string name, string password)
    {
        IEnumerable<Manager> reservationManagers = reservationManagerRepository.FindAll();
        return reservationManagers.FirstOrDefault(manager => manager.Name.Equals(name) && manager.Password.Equals(password));
    }

    public Trip[] GetAllTrips()
    {
        IEnumerable<Trip> trips = tripRepository.FindAll();
        Console.WriteLine("Getting all trips...");
        return trips.ToArray();
    }

    public Trip SearchTripById(long id)
    {
        Trip trip = tripRepository.FindOne(id);
        if (trip == null)
            throw new FlightException("Error getting trip");
        return trip;
    }

    public Reservation AddReservation(Reservation reservation)
    {
        Trip trip = SearchTripById(reservation.trip.Id);
        trip.NoOfSeatsAvailable -= reservation.ReservedSeats;
        trip = tripRepository.Update(trip) ?? throw new FlightException("Error updating trip");
        Reservation r = reservationRepository.Save(reservation) ?? throw new FlightException("Error adding reservation");

        var tasks = loggedClients.Values.Select(client => Task.Run(() =>
        {
            try
            {
                client.ReservationAdded(reservation);
            }
            catch (FlightException e)
            {
                Console.Error.WriteLine("Error notifying client: " + e.Message);
            }
        }));

        Task.WhenAll(tasks);
        return reservation;
    }

    public Reservation[] GetReservationsByTrip(long tripId)
    {
        IEnumerable<Reservation> reservations = reservationRepository.FindAll();
        return reservations.Where(reservation => reservation.Id == tripId).ToArray();
    }


    public void Logout(Manager reservationManager, IFlightObserver client)
    {
        if (!loggedClients.Remove(reservationManager.Name))
            throw new FlightException($"User {reservationManager.Name} is not logged in.");
    }

    public Trip[] SearchTripsByDestination(string destination, DateTime date)
    {
        var allTrips = tripRepository.FindAll();
        var trips = allTrips.Where(trip => trip.Destination.Equals(destination) &&
                                            trip.DepartureTime.Date == date.Date)
                            .ToHashSet();
        return trips.ToArray();
    }
}
