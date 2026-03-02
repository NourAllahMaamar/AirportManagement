using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AM.ApplicationCore.Domain;

namespace AM.ApplicationCore.Configurations;

/// Fluent API configuration for Flight entity

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.HasKey(f => f.FlightId);
        
        builder.HasOne(f => f.Plane)
            .WithMany(p => p.Flights)
            .HasForeignKey(f => f.PlaneId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(f => f.Passengers)
            .WithMany(p => p.Flights)
            .UsingEntity(j => j.ToTable("FlightPassenger"));
    }
}
