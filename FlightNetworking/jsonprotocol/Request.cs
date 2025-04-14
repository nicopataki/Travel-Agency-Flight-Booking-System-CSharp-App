namespace FlightNetworking.jsonprotocol;

public class Request
{
    public RequestType Type { get; set; }
    public ManagerDTO Manager { get; set; }
    public ReservationDTO Reservation { get; set; }
    public TripDTO Trip { get; set; }
    public TripDTO[] Trips { get; set; }
    public string Destination { get; set; }
    public long TripId { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public DateTime Date { get; set; }


    public new string ToString()
    {
        return base.ToString();
    }
}