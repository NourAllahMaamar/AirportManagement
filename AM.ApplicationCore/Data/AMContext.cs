using Microsoft.EntityFrameworkCore;
using AM.ApplicationCore.Domain;

namespace AM.ApplicationCore.Data;


public class AMContext : DbContext
{
    public DbSet<Passenger> Passengers { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Plane> Planes { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Traveller> Travellers { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    public AMContext(DbContextOptions<AMContext> options) : base(options)
    {
    }

    public AMContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = "Server=localhost;Database=DotNetTd;User=root;Password=;";
            
            optionsBuilder
                .UseLazyLoadingProxies()  
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }

  
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<DateTime>()
            .HaveColumnType("date");
    }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AMContext).Assembly);

    
        modelBuilder.Entity<Staff>().ToTable("Staffs");
        modelBuilder.Entity<Traveller>().ToTable("Travellers");
        
        /*
        modelBuilder.Entity<Passenger>()
            .HasDiscriminator<int>("IsTraveller")
            .HasValue<Passenger>(0)
            .HasValue<Traveller>(1)
            .HasValue<Staff>(2);
        */
    }
}
