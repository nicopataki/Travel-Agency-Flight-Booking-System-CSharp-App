using System.Configuration;
using System.Data;
using System.Data.SQLite;
using log4net;

namespace LabMPP.repository.databases;

public static class DBUtils
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(DBUtils));

    public static IDbConnection GetConnection()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Default"]?.ConnectionString;
        
        if (connectionString == null)
        {
            Log.Error("Connection string is null. Exiting.");
            return null;
        }
        try
        {
            IDbConnection instance = new SQLiteConnection(connectionString);
            instance.Open();
        
            Log.Info("Connection opened successfully.");
            return instance;
        }
        catch (Exception ex)
        {
            Log.Error($"Error while opening the connection: {ex.Message}");
            return null;
        }
    }
}