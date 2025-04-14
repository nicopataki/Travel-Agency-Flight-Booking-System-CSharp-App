namespace FlightNetworking;

public class ServerException: Exception
{
    public ServerException() : base() { }
        
    public ServerException(string message) : base(message) { }
        
    public ServerException(string message, Exception inner) : base(message, inner) { }
        
}