using System.ComponentModel.Design;
using System.Configuration;
using FlightNetworking.jsonprotocol;
using FlightServices;
using System;
using System.Net.Mime;
using LabMPP;

namespace FlightClient;

public class StartJsonClient
{
    private static int defaultChatPort = 12345;
    private static string defaultServer = "localhost";

    public static void Main()
    {
        Console.WriteLine("In start");

        string serverIP = ConfigurationManager.AppSettings["TransportServer.Host"] ?? defaultServer;
        int serverPort = defaultChatPort;

        try
        {
            serverPort = int.Parse(ConfigurationManager.AppSettings["TransportServer.Port"]);
        }
        catch (FormatException ex)
        {
            Console.Error.WriteLine("Wrong port number " + ex.Message);
            Console.WriteLine("Using default port: " + defaultChatPort);
        }
        Console.WriteLine("Using server IP " + serverIP);
        Console.WriteLine("Using server port " + serverPort);

        //ITransportServices server = new TransportServicesRpcProxy(serverIP, serverPort);
        IFlightServices server = new TransportServicesJsonProxy(serverIP, serverPort);
            
        LogIn loginForm = new LogIn();
        loginForm.SetServer(server);

        User mainForm = new User();
        mainForm.SetServer(server);

        loginForm.SetMainForm(mainForm);

        Application.Run(loginForm);
    }
}