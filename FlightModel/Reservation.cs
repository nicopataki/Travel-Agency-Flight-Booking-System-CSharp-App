namespace FlightModel;

public class Reservation: Entity<long>
{
    public Trip trip { get; set; }
    public long ReservedSeats { get; set; } 
    public string ClientName { get; set; }

    public Reservation(Trip trip, long reservedSeats, string clientName)
    {
        this.trip = trip;
        ReservedSeats = reservedSeats;
        ClientName = clientName;
    }

    public override string ToString()
    {
        return "Reservation: " + "trip=" + trip + ", reservedSeats=" + string.Join(", ", ReservedSeats) + ", clientName=" + ClientName;
    }
}