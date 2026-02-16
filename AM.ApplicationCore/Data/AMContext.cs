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

    public AMContext(DbContextOptions<AMContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure TPH (Table Per Hierarchy) for Passenger inheritance
        modelBuilder.Entity<Passenger>()
            .HasDiscriminator<string>("PassengerType")
            .HasValue<Passenger>("Passenger")
            .HasValue<Staff>("Staff")
            .HasValue<Traveller>("Traveller");

        // Configure Flight-Plane relationship
        modelBuilder.Entity<Flight>()
            .HasOne(f => f.Plane)
            .WithMany(p => p.Flights)
            .HasForeignKey(f => f.PlaneId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure many-to-many relationship between Flight and Passenger
        modelBuilder.Entity<Flight>()
            .HasMany(f => f.Passengers)
            .WithMany(p => p.Flights)
            .UsingEntity<Dictionary<string, object>>(
                "FlightPassenger",
                j => j.HasOne<Passenger>().WithMany().HasForeignKey("PassengersId"),
                j => j.HasOne<Flight>().WithMany().HasForeignKey("FlightsId")
            );

        // Configure PlaneType enum to be stored as string
        modelBuilder.Entity<Plane>()
            .Property(p => p.PlaneType)
            .HasConversion<string>();
    }
}
