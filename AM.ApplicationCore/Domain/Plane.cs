namespace AM.ApplicationCore.Domain;

public class Plane
{
    public int Id { get; set; }
    public PlaneType PlaneType { get; set; }
    public int Capacity { get; set; }
    public DateTime ManufactureDate { get; set; }
    public ICollection<Flight> Flights { get; set; }

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
