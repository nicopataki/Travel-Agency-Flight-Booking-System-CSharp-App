using csharp.flightproto;
using FlightNetworking.Protos;
using Grpc.Core;
using csharp.flightproto;
using FlightNetworking.Protos;
using FlightPersistance.entityFramework;
using FlightServices;
using LabMPP.repository.databases;
using LabMPP.repository.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FlightServer;

public class StartGrpcServer
{
    const int port = 12345;

    /*static void Main(string[] args)
    {
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseSqlite("Data Source=C:\\Users\\Asus\\OneDrive\\Desktop\\anul II\\LabMPP\\zboruri.db") // Asigură-te că aceasta este calea corectă către fișierul tău SQLite
            .Options;
        var context = new MyDbContext(options);
        var flightService = new FlightServicesImplementation(
            new ManagerEFRepo(context),        
            new ReservationRepository(),
            new TripRepoRepository()
        );
        
        Server server = new Server
        {
            Services = { FlightService.BindService(new GrpcServer(flightService)) },
            Ports = { new ServerPort("localhost", 12345, ServerCredentials.Insecure) }
        };

        server.Start();

        Console.WriteLine("gRPC server is listening on port 12345");
        Console.WriteLine("Press any key to stop...");
        Console.ReadKey();

        server.ShutdownAsync().Wait();
    }*/

}