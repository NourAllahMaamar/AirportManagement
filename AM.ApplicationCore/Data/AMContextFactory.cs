using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AM.ApplicationCore.Data;

/// <summary>
/// Factory for creating AMContext instances at design time (used by EF Core tools for migrations)
/// </summary>
public class AMContextFactory : IDesignTimeDbContextFactory<AMContext>
{
    public AMContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AMContext>();
        
        // Connection string for SQLite (cross-platform) - use absolute path
        string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "AirportManagement.db");
        var connectionString = $"Data Source={dbPath}";
        
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlite(connectionString);
        
        return new AMContext(optionsBuilder.Options);
    }
}
