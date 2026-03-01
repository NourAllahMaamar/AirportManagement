using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AM.ApplicationCore.Domain;

namespace AM.ApplicationCore.Configurations;

/// <summary>
/// Fluent API configuration for Ticket entity
/// </summary>
public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        // Configure Primary Key
        builder.HasKey(t => t.TicketId);
        
        // Configure relationship: Ticket -> Passenger
        builder.HasOne(t => t.Passenger)
            .WithMany(p => p.Tickets)
            .HasForeignKey(t => t.PassengerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Configure relationship: Ticket -> Flight
        builder.HasOne(t => t.Flight)
            .WithMany(f => f.Tickets)
            .HasForeignKey(t => t.FlightId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
