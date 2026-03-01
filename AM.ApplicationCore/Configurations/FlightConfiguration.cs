using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AM.ApplicationCore.Domain;

namespace AM.ApplicationCore.Configurations;

/// <summary>
/// Fluent API configuration for Flight entity
/// </summary>
public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        // Configure Primary Key
        builder.HasKey(f => f.FlightId);
        
        // Configure One-to-Many relationship between Plane and Flight
        builder.HasOne(f => f.Plane)
            .WithMany(p => p.Flights)
            .HasForeignKey(f => f.PlaneId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Configure Many-to-Many relationship between Flight and Passenger
        builder.HasMany(f => f.Passengers)
            .WithMany(p => p.Flights)
            .UsingEntity(j => j.ToTable("FlightPassenger"));
    }
}
