using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.ApplicationCore.Domain;

public class Flight
{
    public int FlightId { get; set; }
    public DateTime FlightDate { get; set; }
    public required string Destination { get; set; }
    public DateTime EffectiveArrival { get; set; }
    public int EstimatedDuration { get; set; }
    
    public string? AirlineLogo { get; set; }
    
    [ForeignKey("Plane")]
    public int PlaneId { get; set; }
    
    public required virtual Plane Plane { get; set; }
    public virtual ICollection<Passenger> Passengers { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; }

    public Flight()
    {
        Passengers = new List<Passenger>();
        Tickets = new List<Ticket>();
    }

    public override string ToString()
    {
        return $"Flight to {Destination}, Date: {FlightDate}, Duration: {EstimatedDuration} min, Plane: {Plane?.PlaneType}";
    }
}
