namespace FlightNetworking.jsonprotocol;

public class Response
{
    public ResponseType Type { get; set; }
    public string ErrorMessage { get; set; }
    public ManagerDTO Manager { get; set; }
    public ManagerDTO[] Managers { get; set; }
    public TripDTO Trip { get; set; }
    public TripDTO[] Trips { get; set; }
    public ReservationDTO Reservation { get; set; }
    public ReservationDTO[] Reservations { get; set; }

    public Response() { }

    public string ToString()
    {
        return base.ToString();
    }
}