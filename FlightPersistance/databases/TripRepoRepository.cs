using System.Data;
using FlightModel;
using LabMPP.repository.interfaces;
using log4net;

namespace LabMPP.repository.databases;

public class TripRepoRepository: ITripRepo
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(TripRepoRepository));

    public TripRepoRepository()
    {
        logger.Info("Creating TripRepository");
    }

    public Trip FindOne(long id)
    {
        logger.Info("Finding Trip");
        IDbConnection connection = DBUtils.GetConnection();
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Trip WHERE id = @id";
            IDataParameter dataParameter = command.CreateParameter();
            dataParameter.ParameterName = "@id";
            dataParameter.Value = id;
            command.Parameters.Add(dataParameter);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    string destination = reader.GetString(1);
                    DateTime date = reader.GetDateTime(2);
                    long noOfSeats = reader.GetInt64(3);
                    string aeroport = reader.GetString(4);
                    Trip trip = new Trip(destination, date, noOfSeats, aeroport);
                    trip.Id = id;
                    return trip;
                }
            }
        }
        return null;
    }

    public IEnumerable<Trip> FindAll()
    {
        logger.Info("Finding all Trips");
        IDbConnection connection = DBUtils.GetConnection();
        List<Trip> trips = new List<Trip>();
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Trip";
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    long id = reader.GetInt64(0);
                    string destination = reader.GetString(1);
                    //long timestamp = reader.GetInt64(2);
                    string departureTimeString = reader.GetString(2); // Citim data ca șir de caractere
                    DateTime departureTime = DateTime.Parse(departureTimeString); 
                    long noOfSeats = reader.GetInt64(3);
                    string aeroport = reader.GetString(4);
                    Trip trip = new Trip(destination, departureTime, noOfSeats, aeroport);
                    trip.Id = id;
                    trips.Add(trip);
                }
            }
        }
        return trips;
    }

    public Trip Save(Trip trip)
    {
        logger.Info("Saving Trip");
        IDbConnection connection = DBUtils.GetConnection();
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO Trip (destination, departureTime, noOfSeatsAvailable, aeroport) VALUES (@destination, @date, @noOfSeats, @aeroport)";
            IDataParameter destinationParameter = command.CreateParameter();
            destinationParameter.ParameterName = "@destination";
            destinationParameter.Value = trip.Destination;
            command.Parameters.Add(destinationParameter);
            IDataParameter dateParameter = command.CreateParameter();
            dateParameter.ParameterName = "@date";
            dateParameter.Value = trip.DepartureTime.ToString("yyyy-MM-dd HH:mm");
            command.Parameters.Add(dateParameter);
            IDataParameter noOfSeatsParameter = command.CreateParameter();
            noOfSeatsParameter.ParameterName = "@noOfSeats";
            noOfSeatsParameter.Value = trip.NoOfSeatsAvailable;
            command.Parameters.Add(noOfSeatsParameter);
            IDataParameter aeroportParameter = command.CreateParameter();
            aeroportParameter.ParameterName = "@aeroport";
            aeroportParameter.Value = trip.Aeroport;
            command.Parameters.Add(aeroportParameter);
            command.ExecuteNonQuery();
        }

        return trip;
    }

    public Trip Delete(long id)
    {
        logger.Info("Deleting Trip");
        Trip tripToDelete = FindOne(id);
        if (tripToDelete != null)
        {
            IDbConnection connection = DBUtils.GetConnection();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Trip WHERE id = @id";
                IDataParameter idParameter = command.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.Value = id;
                command.Parameters.Add(idParameter);
                command.ExecuteNonQuery();
            }
        }
        return tripToDelete;
    }

    public Trip Update(Trip trip)
    {
        logger.Info("Updating Trip");
        IDbConnection connection = DBUtils.GetConnection();
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "UPDATE Trip SET destination = @destination, departureTime = @departureTime, noOfSeatsAvailable = @noOfSeats WHERE id = @id";
            IDataParameter destinationParameter = command.CreateParameter();
            destinationParameter.ParameterName = "@destination";
            destinationParameter.Value = trip.Destination;
            command.Parameters.Add(destinationParameter);
            IDataParameter departureTimeParameter = command.CreateParameter();
            departureTimeParameter.ParameterName = "@departureTime";
            departureTimeParameter.Value = trip.DepartureTime;
            command.Parameters.Add(departureTimeParameter);
            IDataParameter noOfSeatsParameter = command.CreateParameter();
            noOfSeatsParameter.ParameterName = "@noOfSeats";
            noOfSeatsParameter.Value = trip.NoOfSeatsAvailable;
            command.Parameters.Add(noOfSeatsParameter);
            IDataParameter idParameter = command.CreateParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = trip.Id;
            command.Parameters.Add(idParameter);
            IDataParameter aeroportParameter = command.CreateParameter();
            aeroportParameter.ParameterName = "@aeroport";
            aeroportParameter.Value = trip.Aeroport;
            command.Parameters.Add(aeroportParameter);
            command.ExecuteNonQuery();
        }
        return trip;
    }
}