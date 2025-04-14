using FlightModel;

namespace FlightNetworking;

using System;

public static class DTOUtils
    {
        // Convertire ManagerDTO în Manager
        public static Manager GetFromDTO(ManagerDTO managerDTO)
        {
            string name = managerDTO.Name;
            string password = managerDTO.Password;
            return new Manager(name, password);
        }

        // Convertire Manager în ManagerDTO
        public static ManagerDTO GetDTO(Manager manager)
        {
            string name = manager.Name;
            string password = manager.Password;
            return new ManagerDTO(name, password);
        }

        // Convertire ReservationDTO în Reservation
        public static Reservation GetFromDTO(ReservationDTO reservationDTO)
        {
            Trip trip = GetFromDTO(reservationDTO.Trip);
            long reservedSeats = reservationDTO.ReservedSeats;
            string clientName = reservationDTO.ClientName;
            return new Reservation(trip, reservedSeats, clientName);
        }

        // Convertire Reservation în ReservationDTO
        public static ReservationDTO GetDTO(Reservation reservation)
        {
            TripDTO trip = GetDTO(reservation.trip);
            long reservedSeats = reservation.ReservedSeats;
            string clientName = reservation.ClientName;
            return new ReservationDTO(clientName, reservedSeats, trip);
        }

        // Convertire TripDTO în Trip
        public static Trip GetFromDTO(TripDTO tripDTO)
        {
            string destination = tripDTO.Destination;
            DateTime dateTime = DateTime.Parse(tripDTO.DepartureTime);  // Assuming departureTime is a string in the format "yyyy-MM-ddTHH:mm:ss"
            long noOfAvailableSeats = tripDTO.NoOfAvailableSeats;
            string airport = tripDTO.Airport;
            Trip trip = new Trip(destination, dateTime, noOfAvailableSeats, airport);
            trip.Id = tripDTO.Id;  // Set the ID
            return trip;
        }

        // Convertire Trip în TripDTO
        public static TripDTO GetDTO(Trip trip)
        {
            long id = trip.Id;
            string destination = trip.Destination;
            string dateTime = trip.DepartureTime.ToString("yyyy-MM-ddTHH:mm:ss");  // Format the DateTime
            long noOfAvailableSeats = trip.NoOfSeatsAvailable;
            string airport = trip.Aeroport;
            return new TripDTO(id, destination, dateTime, noOfAvailableSeats, airport);
        }

        // Convertire TripDTO[] în Trip[]
        public static Trip[] GetFromDTO(TripDTO[] tripDTOs)
        {
            Trip[] trips = new Trip[tripDTOs.Length];
            for (int i = 0; i < tripDTOs.Length; i++)
            {
                trips[i] = GetFromDTO(tripDTOs[i]);
            }
            return trips;
        }

        // Convertire Trip[] în TripDTO[]
        public static TripDTO[] GetDTO(Trip[] trips)
        {
            TripDTO[] tripDTOS = new TripDTO[trips.Length];
            for (int i = 0; i < trips.Length; i++)
            {
                tripDTOS[i] = GetDTO(trips[i]);
            }
            return tripDTOS;
        }
    }
