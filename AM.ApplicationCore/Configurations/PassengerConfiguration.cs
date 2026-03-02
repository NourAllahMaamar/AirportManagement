using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AM.ApplicationCore.Domain;

namespace AM.ApplicationCore.Configurations;

/// Fluent API configuration for Passenger entity

public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
{
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        builder.HasKey(p => p.PassportNumber);
        
        builder.ToTable("Passengers");
        
        /*
        builder.HasDiscriminator<int>("IsTraveller")
            .HasValue<Passenger>(0)
            .HasValue<Traveller>(1)
            .HasValue<Staff>(2);
        */
    }
}
