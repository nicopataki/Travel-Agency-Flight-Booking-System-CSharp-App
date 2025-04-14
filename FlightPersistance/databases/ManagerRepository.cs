using System.Data;
using FlightModel;
using LabMPP.repository.interfaces;
using log4net;

namespace LabMPP.repository.databases;

public class ManagerRepository: IManagerRepo
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(ManagerRepository));

    public ManagerRepository()
    {
        logger.Info("Creating ManagerRepository");
    }

    public Manager FindOne(long id)
    {
        logger.Info("Finding Manager");
        IDbConnection connection = DBUtils.GetConnection();
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Manager WHERE id = @id";
            IDataParameter dataParameter = command.CreateParameter();
            dataParameter.ParameterName = "@id";
            dataParameter.Value = id;
            command.Parameters.Add(dataParameter);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    string name = reader.GetString(1);
                    string password = reader.GetString(2);
                    Manager manager = new Manager(name, password);
                    manager.Id = id;
                    return manager;
                }
            }
        }
        return null;
    }

    public IEnumerable<Manager> FindAll()
    {
        logger.Info("Finding all Managers");
        IDbConnection connection = DBUtils.GetConnection();
        List<Manager> managers = new List<Manager>();
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Manager";
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    long id = reader.GetInt64(0);
                    string name = reader.GetString(1);
                    string password = reader.GetString(2);
                    Manager manager = new Manager(name, password);
                    manager.Id = id;
                    managers.Add(manager);
                }
            }
        }
        return managers;
    }

    public Manager Save(Manager manager)
    {
        logger.Info("Saving Manager");
        IDbConnection connection = DBUtils.GetConnection();
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO Manager (name, password) VALUES (@name, @password)";
            IDataParameter dataParameter = command.CreateParameter();
            dataParameter.ParameterName = "@name";
            dataParameter.Value = manager.Name;
            command.Parameters.Add(dataParameter);
            IDataParameter dataParameter2 = command.CreateParameter();
            dataParameter2.ParameterName = "@password";
            dataParameter2.Value = manager.Password;
            command.Parameters.Add(dataParameter2);
            command.ExecuteNonQuery();
        }

        return manager;
    }

    public Manager Delete(long id)
    {
        logger.Info("Deleting Trip");
        Manager managerToDelete = FindOne(id);
        if (managerToDelete != null)
        {
            IDbConnection connection = DBUtils.GetConnection();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Manager WHERE id = @id";
                IDataParameter idParameter = command.CreateParameter();
                idParameter.ParameterName = "@id";
                idParameter.Value = id;
                command.Parameters.Add(idParameter);
                command.ExecuteNonQuery();
            }
        }
        return managerToDelete;
    }

    public Manager Update(Manager manager)
    {
        logger.Info("Updating Manager");
        IDbConnection connection = DBUtils.GetConnection();
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "UPDATE Manager SET name = @name, password = @password WHERE id = @id";
            IDataParameter nameParameter = command.CreateParameter();
            nameParameter.ParameterName = "@name";
            nameParameter.Value = manager.Name;
            command.Parameters.Add(nameParameter);
            IDataParameter passwordParameter = command.CreateParameter();
            passwordParameter.ParameterName = "@password";
            passwordParameter.Value = manager.Password;
            command.Parameters.Add(passwordParameter);
            IDataParameter idParameter = command.CreateParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = manager.Id;
            command.Parameters.Add(idParameter);
            command.ExecuteNonQuery();
        }
        return manager;
    }
}