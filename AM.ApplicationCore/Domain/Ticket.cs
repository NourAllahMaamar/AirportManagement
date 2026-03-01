using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.ApplicationCore.Domain;

public class Ticket
{
    [Key]
    public int TicketId { get; set; }
    
    public decimal Price { get; set; }
    
    // Foreign Keys
    [ForeignKey("Passenger")]
    public string PassengerId { get; set; }
    
    [ForeignKey("Flight")]
    public int FlightId { get; set; }
    
    // Navigation properties - virtual for lazy loading
    public virtual Passenger Passenger { get; set; }
    public virtual Flight Flight { get; set; }
}
