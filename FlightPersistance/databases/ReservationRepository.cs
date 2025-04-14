using System.Data;
using FlightModel;
using LabMPP.repository.interfaces;
using log4net;

namespace LabMPP.repository.databases;

public class ReservationRepository : IReservationRepo
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(ReservationRepository));

    private TripRepoRepository _tripRepoRepo;
    public ReservationRepository()
    {
        logger.Info("Creating TripRepository");
        _tripRepoRepo = new TripRepoRepository();
    }
    
    public Reservation FindOne(long id)
    {
        logger.Info("Finding Reservation");
        IDbConnection connection = DBUtils.GetConnection();
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Reservation WHERE id = @id";
            IDataParameter dataParameter = command.CreateParameter();
            dataParameter.ParameterName = "@id";
            dataParameter.Value = id;
            command.Parameters.Add(dataParameter);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    long tripId = reader.GetInt64(1);
                    Trip trip = _tripRepoRepo.FindOne(tripId);
                    if (trip == null)
                    {
                        return null;
                    }
                    
                    long reservedSeats = reader.GetInt64(2);
                    
                    string clientName = reader.GetString(3);
                    Reservation reservation = new Reservation(trip, reservedSeats, clientName);
                    return reservation;
                }
            }
        }
        return null;
    }

    public IEnumerable<Reservation> FindAll()
    {
        logger.Info("Finding all Reservations");
        IDbConnection connection = DBUtils.GetConnection();
        List<Reservation> reservations = new List<Reservation>();
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Reservation";
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    long id = reader.GetInt64(0);
                    Reservation reservation = FindOne(id);
                    if (reservation != null)
                        reservations.Add(reservation);
                }
            }
        }
        return reservations;
    }

    public Reservation Save(Reservation reservation)
    {
        logger.Info("Saving Reservation");
        IDbConnection connection = DBUtils.GetConnection();
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO Reservation (trip_id, reserved_seats, client_name) VALUES (@trip_id, @reserved_seats, @client_name)";
            IDataParameter tripParameter = command.CreateParameter();
            tripParameter.ParameterName = "@trip_id";
            tripParameter.Value = reservation.trip.Id;
            command.Parameters.Add(tripParameter);
            IDataParameter seatsParameter = command.CreateParameter();
            seatsParameter.ParameterName = "@reserved_seats";
            seatsParameter.Value = reservation.ReservedSeats;
            command.Parameters.Add(seatsParameter);
            IDataParameter nameParameter = command.CreateParameter();
            nameParameter.ParameterName = "@client_name";
            nameParameter.Value = reservation.ClientName;
            command.Parameters.Add(nameParameter);
            command.ExecuteNonQuery();
        }

        return reservation;
    }

    public Reservation Delete(long id)
    {
        logger.Info("Deleting reservation");
        Reservation reservationToDelete = FindOne(id);
        if (reservationToDelete != null)
        {
            IDbConnection connection = DBUtils.GetConnection();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Reservation WHERE id = @id";
                IDataParameter idParameter = command.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.Value = id;
                command.Parameters.Add(idParameter);
                command.ExecuteNonQuery();
            }
        }
        return reservationToDelete;
    }

    public Reservation Update(Reservation reservation)
    {
        logger.Info("Updating reservation");
        IDbConnection connection = DBUtils.GetConnection();
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "UPDATE Reservation SET trip_id = @trip_id, reserved_seats = @reserved_seats, client_name = @client_name WHERE id = @id";
            IDataParameter tripParameter = command.CreateParameter();
            tripParameter.ParameterName = "@trip_id";
            tripParameter.Value = reservation.trip;
            command.Parameters.Add(tripParameter);
            IDataParameter seatsParameter = command.CreateParameter();
            seatsParameter.ParameterName = "@reserved_seats";
            seatsParameter.Value = reservation.ReservedSeats;
            command.Parameters.Add(seatsParameter);
            IDataParameter nameParameter = command.CreateParameter();
            nameParameter.ParameterName = "@client_name";
            nameParameter.Value = reservation.ClientName;
            command.Parameters.Add(nameParameter);
            IDataParameter idParameter = command.CreateParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = reservation.Id;
            command.Parameters.Add(idParameter);
            command.ExecuteNonQuery();
        }
        return reservation;
    }
}