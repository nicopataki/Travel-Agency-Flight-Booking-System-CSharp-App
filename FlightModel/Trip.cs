namespace FlightModel;

public class Trip: Entity<long>
{
    public string Destination { get; set; }
    public DateTime DepartureTime { get; set; }
    public long NoOfSeatsAvailable { get; set; }
    public string Aeroport { get; set; }

    public Trip(string destination, DateTime departureTime, long noOfSeatsAvailable, string aeroport)
    {
        Destination = destination;
        DepartureTime = departureTime;
        NoOfSeatsAvailable = noOfSeatsAvailable;
        Aeroport = aeroport;
    }

    public Trip()
    {
    }

    public override string ToString()
    {
        return "Trip " + "destination='" + Destination + "', " + DepartureTime + ", " + NoOfSeatsAvailable + ","+Aeroport+"\n";
    }
    
}