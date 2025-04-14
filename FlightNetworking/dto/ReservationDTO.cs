using FlightModel;

namespace FlightNetworking;

public class ReservationDTO
{
    public TripDTO Trip { get; set; }
    public long ReservedSeats { get; set; }
    public string ClientName { get; set; }

    // Constructor
    public ReservationDTO(string clientName, long reservedSeats, TripDTO trip)
    {
        ClientName = clientName;
        ReservedSeats = reservedSeats;
        Trip = trip;
    }

    // Override pentru ToString()
    public override string ToString()
    {
        return $"ReservationDTO{{trip={Trip}, reservedSeats={ReservedSeats}, clientName='{ClientName}'}}";
    }
}