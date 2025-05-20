using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

class MainClass
{
    static HttpClient client = new HttpClient();

    public static void Main(string[] args)
    {
        RunAsync().Wait();
    }

    static async Task RunAsync()
    {
        client.BaseAddress = new Uri("http://localhost:8080/flight/trip");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        String text = await GetTextAsync("http://localhost:8080/flight/trip");
        Console.WriteLine("Am obtinut " + text);
        String id = "3";
        Console.WriteLine("Get trip " + id);
        TripDTO result = await GetTripAsync("http://localhost:8080/flight/trip/" + id);
        Console.WriteLine("Am primit " + result);
        Console.ReadLine();
    }

    static async Task<String> GetTextAsync(string url)
    {
        String trip = null;
        HttpResponseMessage response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            trip = await response.Content.ReadAsStringAsync();
        }
        Console.WriteLine("Result: " + trip);
        return trip;
    }

    static async Task<TripDTO> GetTripAsync(string url)
    {
        TripDTO trip = null;
        HttpResponseMessage response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            trip = await response.Content.ReadAsAsync<TripDTO>();
        }
        return trip;
    }
    
    public class TripDTO
    {
        public string Id { get; set; }
        public string Destination { get; set; }
        public string DepartureTime { get; set; }
        public string NoOfAvailableSeats { get; set; }
        public string Airport { get; set; }

        public override string ToString()
        {
            return string.Format("[Trip: Id={0}, Destination={1}, DateDeparture={2}, NoOfAvailableSeats={3}, Airport={4}]", Id, Destination, DepartureTime, NoOfAvailableSeats, Airport);
        }
    }
}