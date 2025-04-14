using FlightModel;

namespace FlightServices;

public interface IFlightObserver
{
    void ReservationAdded(Reservation reservation);
}
