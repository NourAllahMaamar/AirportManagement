using System.ComponentModel.DataAnnotations;

namespace AM.ApplicationCore.Domain;

public class Plane
{
    public int PlaneId { get; set; }
    public PlaneType PlaneType { get; set; }
    
    // Capacity must be a positive integer
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be a positive integer")]
    public int Capacity { get; set; }
    
    public DateTime ManufactureDate { get; set; }
    
    // Virtual for lazy loading
    public virtual ICollection<Flight> Flights { get; set; }

    public Plane()
    {
        Flights = new List<Flight>();
    }

    public Plane(PlaneType pt, int capacity, DateTime date)
    {
        PlaneType = pt;
        Capacity = capacity;
        ManufactureDate = date;
        Flights = new List<Flight>();
    }

    public override string ToString()
    {
        return $"Plane Type: {PlaneType}, Capacity: {Capacity}, Manufacture Date: {ManufactureDate.ToShortDateString()}";
    }
}
