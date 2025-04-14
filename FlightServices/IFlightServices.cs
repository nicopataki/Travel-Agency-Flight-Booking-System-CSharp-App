using FlightModel;

namespace FlightServices;

public interface IFlightServices
{
    Manager Login(Manager manager, IFlightObserver client);

    Trip[] GetAllTrips();

    Trip SearchTripById(long id);

    Manager SearchManagerByName(string name, string password);

    Reservation AddReservation(Reservation reservation);

    void Logout(Manager manager, IFlightObserver client);

    Trip[] SearchTripsByDestination(string destination, DateTime date);

    //void ModifyTrip(Trip trip);
}
