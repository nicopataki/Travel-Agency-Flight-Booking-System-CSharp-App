// See https://aka.ms/new-console-template for more information

using System.Configuration;
using System.Data.SQLite;
using System.Net.Mime;
using LabMPP;
using log4net;
using log4net.Config;
using LabMPP.domain;
using LabMPP.repository;
using LabMPP.repository.databases;
using LabMPP.repository.interfaces;
using LabMPP.service;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

public class Program
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(Program));
    private static readonly ITripRepo tripRepo = new TripRepoRepository();
    private static readonly IManagerRepo managerRepo = new ManagerRepository();
    private static readonly IReservationRepo reservationRepo = new ReservationRepository();
    private static readonly Service service = new Service(tripRepo, managerRepo, reservationRepo);
    [STAThread]
    static void Main(string[] args)
    {
        XmlConfigurator.Configure(new FileInfo("log4net.config"));

        Log.Info("Aplicația a pornit!");
        Console.WriteLine("Program started...");
        
        if (TestConnection())
            Log.Info("Test connection successful");
        else
            Log.Error("Test connection failed");
        
        string connectionString = ConfigurationManager.ConnectionStrings["Default"]?.ConnectionString;

        if (string.IsNullOrEmpty(connectionString))
        {
            Console.WriteLine("Eroare: String-ul de conexiune nu a fost găsit!");
            return;
        }

        // Deschiderea conexiunii cu baza de date
        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            try
            {
                conn.Open();
                Console.WriteLine("Conexiune la baza de date reusita!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la conectare: {ex.Message}");
            }
        }

        //TestTrip();
        
        //TestManager();
        
        //TestReservation();
        
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        //Application.Run(new LogIn(service));

    }
    
    public static bool TestConnection()
    {
        try
        {
            using (var connection = DBUtils.GetConnection())
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Log.Error("Test connection failed: " + ex.Message);
            return false;
        }
    }

    public static void TestTrip()
    {
        TripRepoRepository repoRepository = new TripRepoRepository();
        
        /* FIND ALL */
        Console.WriteLine("Finding all trips:");
        if (repoRepository.FindAll() != null)
        {
            foreach (var trip in repoRepository.FindAll())
            {
                Console.WriteLine(trip);
            }
            Console.WriteLine("=======================================================");
        }

        DateTime date = DateTime.Now;
        Trip tripp = new Trip("Paris", DateTime.Now, 20 ,"Aeroportul Internațional Avram Iancu Cluj-Napoca");
        repoRepository.Save(tripp);
        
        foreach (var trip in repoRepository.FindAll())
        {
            Console.WriteLine(trip);
        }
        Console.WriteLine("=======================================================");

    }
    
    public static void TestManager()
    {
        ManagerRepository repoRepository = new ManagerRepository();
        
        /* FIND ALL */
        Console.WriteLine("Finding all managers:");
        if (repoRepository.FindAll() != null)
        {
            foreach (var manager in repoRepository.FindAll())
            {
                Console.WriteLine(manager);
            }
            Console.WriteLine("=======================================================");
        }

        Manager managerr = new Manager("Pataki Nicoleta", "nico.pataki");
        repoRepository.Save(managerr);
        
        foreach (var manager in repoRepository.FindAll())
        {
            Console.WriteLine(manager);
        }
        Console.WriteLine("=======================================================");

    }
    
    public static void TestReservation()
    {
        ReservationRepository repoRepository = new ReservationRepository();
        
        /* FIND ALL */
        Console.WriteLine("Finding all reservations:");
        if (repoRepository.FindAll() != null)
        {
            foreach (var reservation in repoRepository.FindAll())
            {
                Console.WriteLine(reservation);
            }
            Console.WriteLine("=======================================================");
        }

        Trip trip = new Trip("Paris", DateTime.Now, 20, "Aeroportul Internațional Avram Iancu Cluj-Napoca");
        trip.Id = 1;
        Reservation reservationn = new Reservation(trip, 2, "Rus Mihai");
        repoRepository.Save(reservationn);
        
        foreach (var reservation in repoRepository.FindAll())
        {
            Console.WriteLine(reservation);
        }
        Console.WriteLine("=======================================================");

    }

}