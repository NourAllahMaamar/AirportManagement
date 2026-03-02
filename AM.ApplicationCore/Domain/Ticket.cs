using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.ApplicationCore.Domain;

public class Ticket
{
    [Key]
    public int TicketId { get; set; }
    
    public decimal Price { get; set; }
    
    [ForeignKey("Passenger")]
    public required string PassengerId { get; set; }
    
    [ForeignKey("Flight")]
    public int FlightId { get; set; }
    
    public required virtual Passenger Passenger { get; set; }
    public required virtual Flight Flight { get; set; }
    public virtual ICollection<ReservationTicket> ReservationTickets { get; set; }

    public Ticket()
    {
        ReservationTickets = new List<ReservationTicket>();
    }
}
