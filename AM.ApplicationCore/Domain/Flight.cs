namespace AM.ApplicationCore.Domain;

public class Flight
{
    public DateTime FlightDate { get; set; }
    public string Destination { get; set; }
    public DateTime EffectiveArrival { get; set; }
    public int EstimatedDuration { get; set; }
    public Plane Plane { get; set; }
    public ICollection<Passenger> Passengers { get; set; }

    public Flight()
    {
        Passengers = new List<Passenger>();
    }

    public override string ToString()
    {
        return $"Flight to {Destination}, Date: {FlightDate}, Duration: {EstimatedDuration} min, Plane: {Plane?.PlaneType}";
    }
}
