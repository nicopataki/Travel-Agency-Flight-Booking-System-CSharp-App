namespace FlightNetworking;

public class TripDTO
{
    public long Id { get; set; }
    public string Destination { get; set; }
    public string DepartureTime { get; set; }
    public long NoOfAvailableSeats { get; set; }
    public string Airport { get; set; }

    // Constructor
    public TripDTO(long id, string destination, string departureTime, long noOfAvailableSeats, string airport)
    {
        Id = id;
        Destination = destination;
        DepartureTime = departureTime;
        NoOfAvailableSeats = noOfAvailableSeats;
        Airport = airport;
    }

    // Override pentru ToString()
    public override string ToString()
    {
        return $"TripDTO{{destination='{Destination}', departureTime={DepartureTime}, noOfAvailableSeats={NoOfAvailableSeats}, airport='{Airport}'}}";
    }
}