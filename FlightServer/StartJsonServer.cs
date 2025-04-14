using System.Configuration;
using System.Net.Sockets;
using System.Reflection;
using FlightServices;
using LabMPP.repository.databases;
using LabMPP.repository.interfaces;
using log4net;
using log4net.Config;
using FlightNetworking;

namespace FlightServer;

public class StartJsonServer
{
    private static int defaultPort = 55555;
    private static String defaultId = "127.0.0.1";

    /*[STAThread]*/
    public static void Main(string[] args)
    {
        IManagerRepo reservationManagerRepository = new ManagerRepository();
        IReservationRepo reservationRepository = new ReservationRepository();
        ITripRepo tripRepository = new TripRepoRepository();
        IFlightServices transportServerImpl =
            new FlightServicesImplementation(reservationManagerRepository, reservationRepository, tripRepository);

        int transportServerPort = defaultPort;
        try
        {
            transportServerPort = int.Parse(ConfigurationManager.AppSettings["TransportServer.Port"]);
        }
        catch (FormatException nef)
        {
            Console.Error.WriteLine("Wrong Port Number" + nef.Message);
            Console.Error.WriteLine("Using default port " + defaultPort);
        }

        Console.WriteLine("Starting server on port: " + transportServerPort);
        AbstractServer server = new FlightJsonConcurrentServer(transportServerPort, transportServerImpl);
        try
        {
            server.Start();
        }
        catch (FlightException e)
        {
            Console.Error.WriteLine("Error starting the server" + e.Message);
        }
        finally
        {
            try
            {
                server.Stop();
            }
            catch (FlightException e)
            {
                Console.Error.WriteLine("Error stopping server " + e.Message);
            }
        }
    }
}