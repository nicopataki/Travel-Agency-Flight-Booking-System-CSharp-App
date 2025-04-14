using System.Net.Sockets;
using FlightNetworking.jsonprotocol;
using FlightServices;

namespace FlightNetworking;

public class FlightJsonConcurrentServer : ConcurrentServer
{
    private IFlightServices transportServer;

    public FlightJsonConcurrentServer(int port, IFlightServices transportServer) : base(port)
    {
        this.transportServer = transportServer;
        Console.WriteLine("Transport - TransportRpcConcurrentServer");
    }

    protected override Thread CreateWorker(Socket client)
    {
        //TransportClientRpcReflectionWorker worker = new TransportClientRpcReflectionWorker(transportServer, client);
        FlightClientJsonWorker worker = new FlightClientJsonWorker(transportServer, client);

        Thread tw = new Thread(worker.Run);
        return tw;
    }

    public override void Stop()
    {
        Console.WriteLine("Stopping services ...");
        base.Stop();
    }
}