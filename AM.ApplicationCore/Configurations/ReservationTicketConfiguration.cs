using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AM.ApplicationCore.Domain;

namespace AM.ApplicationCore.Configurations;


public class ReservationTicketConfiguration : IEntityTypeConfiguration<ReservationTicket>
{
    public void Configure(EntityTypeBuilder<ReservationTicket> builder)
    {
        builder.HasKey(rt => new { rt.PassengerId, rt.TicketId, rt.DateReservation });
        
        builder.HasOne(rt => rt.Passenger)
            .WithMany(p => p.ReservationTickets)
            .HasForeignKey(rt => rt.PassengerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(rt => rt.Ticket)
            .WithMany(t => t.ReservationTickets)
            .HasForeignKey(rt => rt.TicketId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.ToTable("ReservationTickets");
    }
}
