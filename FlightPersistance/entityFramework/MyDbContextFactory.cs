using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FlightPersistance.entityFramework;

public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
{
    public MyDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
        optionsBuilder.UseSqlite("Data Source=C:\\Users\\Asus\\OneDrive\\Desktop\\anul II\\LabMPP\\zboruri.db"); 

        return new MyDbContext(optionsBuilder.Options);
    }
}