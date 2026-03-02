using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.ApplicationCore.Domain;


public class ReservationTicket
{
    [ForeignKey("Passenger")]
    public required string PassengerId { get; set; }
    
    [ForeignKey("Ticket")]
    public required int TicketId { get; set; }
    
    public required DateTime DateReservation { get; set; }
    
    public required virtual Passenger Passenger { get; set; }
    public required virtual Ticket Ticket { get; set; }
}
