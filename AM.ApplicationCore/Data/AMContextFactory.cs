using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MySqlConnector;

namespace AM.ApplicationCore.Data;

public class AMContextFactory : IDesignTimeDbContextFactory<AMContext>
{
    public AMContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AMContext>();
        
        // Use a default connection string for migrations
        // This will use the connection string from appsettings.json in the Console project
        var connectionString = "Server=127.0.0.1;Port=3306;Database=DotNetTd;Uid=root;Pwd=;";
        
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        
        return new AMContext(optionsBuilder.Options);
    }
}
