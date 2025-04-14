using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using FlightModel;
using FlightServices;
using Newtonsoft.Json;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace FlightNetworking.jsonprotocol;

public class TransportServicesJsonProxy : IFlightServices
{
    private string host;
    private int port;

    private IFlightObserver client;

    private StreamReader input;
    private StreamWriter output;
    private JsonSerializerOptions options;
    private Socket connection;

    private BlockingCollection<Response> qresponses;
    private volatile bool finished;

    public TransportServicesJsonProxy(string host, int port)
    {
        this.host = host;
        this.port = port;
        options = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() }
        };
        qresponses = new BlockingCollection<Response>();
    }

    public Manager Login(Manager reservationManager, IFlightObserver client)
    {
        InitializeConnection();

        Request req = JsonProtocolUtils.CreateLoginRequest(reservationManager);
        SendRequest(req);
        Response response = ReadResponse();
        if (response.Type == ResponseType.OK)
        {
            this.client = client;
            return reservationManager;
        }

        if (response.Type == ResponseType.ERROR)
        {
            string err = response.ErrorMessage;
            CloseConnection();
            throw new FlightException(err);
        }

        return DTOUtils.GetFromDTO(response.Manager);
    }

    public Reservation AddReservation(Reservation reservation)
    {
        Request req = JsonProtocolUtils.CreateAddReservationRequest(reservation);
        SendRequest(req);
        Response response = ReadResponse();
        if (response.Type == ResponseType.ERROR)
        {
            string err = response.ErrorMessage;
            throw new FlightException(err);
        }

        return reservation;
    }

    public void Logout(Manager reservationManager, IFlightObserver client)
    {
        Request req = JsonProtocolUtils.CreateLogoutRequest(reservationManager);
        SendRequest(req);
        Response response = ReadResponse();
        if (response.Type == ResponseType.ERROR)
        {
            string err = response.ErrorMessage;
            throw new FlightException(err);
        }
        //return reservationManager;
    }

    public Trip[] GetAllTrips()
    {
        Request req = JsonProtocolUtils.CreateGetAllTripsRequest();
        SendRequest(req);
        Response response = ReadResponse();
        if (response.Type == ResponseType.ERROR)
        {
            string err = response.ErrorMessage;
            throw new FlightException(err);
        }

        return DTOUtils.GetFromDTO(response.Trips);
    }

    public Trip SearchTripById(long id)
    {
        Request req = JsonProtocolUtils.CreateSearchTripByIdRequest(id);
        SendRequest(req);
        Response response = ReadResponse();
        if (response.Type == ResponseType.ERROR)
        {
            string err = response.ErrorMessage;
            throw new FlightException(err);
        }

        return DTOUtils.GetFromDTO(response.Trip);
    }

    public Manager SearchManagerByName(string name, string password)
    {
        Request req = JsonProtocolUtils.CreateSearchManagerByNameRequest(name, password);
        SendRequest(req);
        Response response = ReadResponse();
        if (response.Type == ResponseType.ERROR)
        {
            string err = response.ErrorMessage;
            throw new FlightException(err);
        }

        return DTOUtils.GetFromDTO(response.Manager);
    }

    public Trip[] SearchTripsByDestination(string destination, DateTime date)
    {
        var request = JsonProtocolUtils.CreateGetAllTripsByDestinationRequest(destination, date);

        SendRequest(request);

        var response = ReadResponse();

        if (response.Type == ResponseType.ERROR)
        {
            throw new FlightException(response.ErrorMessage);
        }

        return DTOUtils.GetFromDTO(response.Trips);
    }

    public void CloseConnection()
    {
        finished = true;
        try
        {
            input.Close();
            output.Close();
            connection.Close();
            client = null;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error " + e);
        }
    }

    private void SendRequest(Request request)
    {
        string requestJson = JsonConvert.SerializeObject(request, Formatting.None);

        Console.WriteLine(requestJson);

        output.WriteLine(requestJson);
        output.Flush();
    }

    private Response ReadResponse()
    {
        Response response = null;
        try
        {
            response = qresponses.Take();
        }
        catch (InvalidOperationException e)
        {
            throw new FlightException("Error reading response " + e);
        }

        if (response == null)
        {
            throw new FlightException("Received null response from server.");
        }

        return response;
    }

    private void InitializeConnection()
    {
        try
        {
            connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            connection.Connect(host, port);
            NetworkStream networkStream = new NetworkStream(connection);
            output = new StreamWriter(networkStream);
            input = new StreamReader(networkStream);
            finished = false;
            StartReader();
        }
        catch (IOException e)
        {
            throw new FlightException("Error initializing connection " + e);
        }
    }

    private void StartReader()
    {
        Thread tw = new Thread(new ReaderThread(this).Run);
        tw.Start();
    }

    private void HandleUpdate(Response response)
    {
        if (response.Type == ResponseType.NEW_RESERVATION)
        {
            Reservation reservation = DTOUtils.GetFromDTO(response.Reservation);
            try
            {
                client.ReservationAdded(reservation);
            }
            catch (FlightException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }

    private bool IsUpdate(Response response)
    {
        return response.Type == ResponseType.NEW_RESERVATION;
    }

    private class ReaderThread
    {
        private TransportServicesJsonProxy outerClass;

        public ReaderThread(TransportServicesJsonProxy outerClass)
        {
            this.outerClass = outerClass;
        }

        public void Run()
        {
            while (!outerClass.finished)
            {
                try
                {
                    string responseLine = outerClass.input.ReadLine();
                    if (responseLine == null)
                        break;

                    Console.WriteLine("Received response " + responseLine);

                    // Deserializarea folosind Newtonsoft.Json
                    Response response = JsonConvert.DeserializeObject<Response>(responseLine);

                    if (response == null)
                    {
                        Console.WriteLine("Invalid response from the server (Proxy)!");
                        continue;
                    }

                    if (outerClass.IsUpdate(response))
                    {
                        outerClass.HandleUpdate(response);
                    }
                    else
                    {
                        try
                        {
                            outerClass.qresponses.Add(response);
                        }
                        catch (InvalidOperationException e)
                        {
                            Console.WriteLine(e.StackTrace);
                        }
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("Reading error " + e);
                    outerClass.finished = true;
                }
            }
        }

    }
}