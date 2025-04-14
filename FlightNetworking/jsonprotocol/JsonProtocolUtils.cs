using FlightModel;

namespace FlightNetworking.jsonprotocol;

public class JsonProtocolUtils
{
    public static Request CreateLoginRequest(Manager manager)
        {
            var req = new Request
            {
                Type = RequestType.LOGIN,
                Manager = DTOUtils.GetDTO(manager)
            };
            return req;
        }

        public static Request CreateGetAllTripsRequest()
        {
            var req = new Request
            {
                Type = RequestType.GET_ALL_TRIPS
            };
            return req;
        }

        public static Request CreateSearchManagerByNameRequest(string name, string password)
        {
            var req = new Request
            {
                Type = RequestType.SEARCH_MANAGER_BY_NAME,
                Name = name,
                Password = password
            };
            return req;
        }

        public static Request CreateSearchTripByIdRequest(long tripId)
        {
            var req = new Request
            {
                Type = RequestType.SEARCH_TRIP_BY_ID,
                TripId = tripId
            };
            return req;
        }

        public static Request CreateGetAllTripsByDestinationRequest(string destination, DateTime date)
        {
            var req = new Request
            {
                Type = RequestType.GET_ALL_TRIPS_BY_DESTINATION,
                Destination = destination,
                Date = date
            };
            return req;
        }

        public static Request CreateAddReservationRequest(Reservation reservation)
        {
            var req = new Request
            {
                Type = RequestType.ADD_RESERVATION,
                Reservation = DTOUtils.GetDTO(reservation)
            };
            return req;
        }

        public static Request CreateLogoutRequest(Manager manager)
        {
            var req = new Request
            {
                Type = RequestType.LOGOUT,
                Manager = DTOUtils.GetDTO(manager)
            };
            return req;
        }

        public static Response CreateOkResponse()
        {
            var resp = new Response
            {
                Type = ResponseType.OK
            };
            return resp;
        }

        public static Response CreateErrorResponse(string errorMessage)
        {
            var resp = new Response
            {
                Type = ResponseType.ERROR,
                ErrorMessage = errorMessage
            };
            return resp;
        }

        public static Response CreateSearchManagerByNameResponse(Manager manager)
        {
            var resp = new Response
            {
                Type = ResponseType.SEARCH_MANAGER_BY_NAME,
                Manager = DTOUtils.GetDTO(manager)
            };
            return resp;
        }

        public static Response CreateGetAllTripsResponse(Trip[] trips)
        {
            var resp = new Response
            {
                Type = ResponseType.GET_ALL_TRIPS,
                Trips = DTOUtils.GetDTO(trips)
            };
            return resp;
        }

        public static Response CreateSearchTripByIdResponse(Trip trip)
        {
            var resp = new Response
            {
                Type = ResponseType.SEARCH_TRIP_BY_ID,
                Trip = DTOUtils.GetDTO(trip)
            };
            return resp;
        }

        public static Response CreateGetAllTripsByDestinationResponse(Trip[] trips)
        {
            var resp = new Response
            {
                Type = ResponseType.GET_ALL_TRIPS_BY_DESTINATION,
                Trips = DTOUtils.GetDTO(trips)
            };
            return resp;
        }

        public static Response CreateNewReservationResponse(Reservation reservation)
        {
            var resp = new Response
            {
                Type = ResponseType.NEW_RESERVATION,
                Reservation = DTOUtils.GetDTO(reservation)
            };
            return resp;
        }
}