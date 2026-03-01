using Microsoft.EntityFrameworkCore;
using AM.ApplicationCore.Domain;

namespace AM.ApplicationCore.Data;

/// <summary>
/// Airport Management DbContext with EF Core configuration
/// Supports TPT (Table Per Type) inheritance strategy
/// Uses Lazy Loading Proxies for navigation properties
/// </summary>
public class AMContext : DbContext
{
    // DbSets for all entities
    public DbSet<Passenger> Passengers { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Plane> Planes { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Traveller> Travellers { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    // Constructor accepting options (used by DI)
    public AMContext(DbContextOptions<AMContext> options) : base(options)
    {
    }

    // Parameterless constructor for migrations
    public AMContext()
    {
    }

    /// <summary>
    /// Configure the database connection with Lazy Loading and SQL Server
    /// </summary>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "AirportManagement.db");
            string connectionString = $"Data Source={dbPath}";
            
            optionsBuilder
                .UseLazyLoadingProxies()  // Enable Lazy Loading
                .UseSqlite(connectionString);  // Use SQLite (cross-platform)
        }
    }

    /// <summary>
    /// Configure conventions: All DateTime properties mapped to SQL 'date' type
    /// </summary>
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // Map all DateTime properties to SQL 'date' type instead of 'datetime2'
        configurationBuilder
            .Properties<DateTime>()
            .HaveColumnType("date");
    }

    /// <summary>
    /// Configure entity relationships and apply all configurations from assembly
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all IEntityTypeConfiguration implementations from the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AMContext).Assembly);

        // TPT (Table Per Type) inheritance configuration
        // Note: This can be easily switched to TPH by commenting out these lines
        // and uncommenting the discriminator configuration in PassengerConfiguration
        modelBuilder.Entity<Staff>().ToTable("Staffs");
        modelBuilder.Entity<Traveller>().ToTable("Travellers");
        
        // For TPH (Table Per Hierarchy) - comment the above TPT lines and use this:
        /*
        modelBuilder.Entity<Passenger>()
            .HasDiscriminator<int>("IsTraveller")
            .HasValue<Passenger>(0)
            .HasValue<Traveller>(1)
            .HasValue<Staff>(2);
        */
    }
}
