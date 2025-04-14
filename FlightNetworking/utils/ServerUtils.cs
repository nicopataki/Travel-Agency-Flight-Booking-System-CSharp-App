using System.Net;
using System.Net.Sockets;
using log4net;

namespace FlightNetworking;

public abstract class AbstractServer
{
    private int port;
    private TcpListener server = null;

    public AbstractServer(int port)
    {
        this.port = port;
    }

    public void Start()
    {
        try
        {
            server = new TcpListener(System.Net.IPAddress.Any, port);
            server.Start();

            while (true)
            {
                Console.WriteLine("Waiting for clients ...");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client connected ...");
                ProcessRequest(client.Client);
            }
        }
        catch (Exception e)
        {
            throw new ServerException("Starting server error ", e);
        }
        /*finally
        {
            Stop();
        }*/
    }

    protected abstract void ProcessRequest(Socket client);

    public void Stop()
    {
        try
        {
            server.Stop();
        }
        catch (Exception e)
        {
            throw new Exception("Closing server error ", e);
        }
    }
        
}

    
public abstract class ConcurrentServer:AbstractServer
{
            
    public ConcurrentServer(int port) : base(port)
    {
        Console.WriteLine("Concurrent AbstractServer");
    }

    protected override void ProcessRequest(Socket client)
    {
        Thread tw = CreateWorker(client);
        tw.Start();
    }

    protected abstract Thread CreateWorker(Socket client);

    public virtual void Stop()
    {
    }
            
}