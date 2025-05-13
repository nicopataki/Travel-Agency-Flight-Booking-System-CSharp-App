using FlightModel;
using Microsoft.EntityFrameworkCore;
//using DbContext = System.Data.Entity.DbContext;

namespace FlightPersistance.entityFramework;

public class MyDbContext : DbContext
{
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Trip> Trips { get; set; }
    
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    /*protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=zboruri.db");*/
}