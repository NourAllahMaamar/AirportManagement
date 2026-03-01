using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AM.ApplicationCore.Domain;

namespace AM.ApplicationCore.Configurations;

/// <summary>
/// Fluent API configuration for Passenger entity
/// Supports both TPT and TPH inheritance strategies
/// </summary>
public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
{
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        // Configure Primary Key
        builder.HasKey(p => p.PassportNumber);
        
        // Base table for Passenger
        builder.ToTable("Passengers");
        
        // For TPH (Table Per Hierarchy) - uncomment to switch from TPT to TPH:
        /*
        builder.HasDiscriminator<int>("IsTraveller")
            .HasValue<Passenger>(0)
            .HasValue<Traveller>(1)
            .HasValue<Staff>(2);
        */
    }
}
