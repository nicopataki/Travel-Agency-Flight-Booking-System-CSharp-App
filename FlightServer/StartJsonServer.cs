using System.Configuration;
using System.Net.Sockets;
using System.Reflection;
using FlightServices;
using LabMPP.repository.databases;
using LabMPP.repository.interfaces;
using log4net;
using log4net.Config;
using FlightNetworking;
using FlightPersistance.entityFramework;
using Microsoft.EntityFrameworkCore;

namespace FlightServer;

public class StartJsonServer
{
    private static int defaultPort = 12345;
    private static String defaultId = "127.0.0.1";

    /*[STAThread]*/
    public static void Main(string[] args)
    {
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseSqlite("Data Source=C:\\Users\\Asus\\OneDrive\\Desktop\\anul II\\LabMPP\\zboruri.db") 
            .Options;
        var context = new MyDbContext(options);
        IManagerRepo reservationManagerRepository = new ManagerEFRepo(context);
        IReservationRepo reservationRepository = new ReservationRepository();
        ITripRepo tripRepository = new TripEFRepo(context);
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