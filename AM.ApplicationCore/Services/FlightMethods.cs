using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;

namespace AM.ApplicationCore.Services;

public class FlightMethods : IFlightMethods
{
    public List<Flight> Flights { get; set; } = new();


    /// Get flight dates for a destination using FOR loop
    public List<DateTime> GetFlightDates(string destination)
    {
        List<DateTime> dates = new List<DateTime>();
        for (int i = 0; i < Flights.Count; i++)
        {
            if (Flights[i].Destination == destination)
            {
                dates.Add(Flights[i].FlightDate);
            }
        }
        return dates;
    }

    /// Get flight dates 
    public List<DateTime> GetFlightDatesForeach(string destination)
    {
        List<DateTime> dates = new List<DateTime>();
        foreach (var flight in Flights)
        {
            if (flight.Destination == destination)
            {
                dates.Add(flight.FlightDate);
            }
        }
        return dates;
    }

    /// Get flights filtered by name and value 
    public List<Flight> GetFlights(string filterType, string filterValue)
    {
        List<Flight> result = new List<Flight>();
        
        foreach (var flight in Flights)
        {
            var propertyInfo = typeof(Flight).GetProperty(filterType);
            if (propertyInfo != null)
            {
                var value = propertyInfo.GetValue(flight);
                if (value != null && value.ToString() == filterValue)
                {
                    result.Add(flight);
                }
            }
        }
        
        return result;
    }


    /// Get flight dates using
    public List<DateTime> GetFlightDatesLinq(string destination)
    {
        var query = from f in Flights
                    where f.Destination == destination
                    select f.FlightDate;
        return query.ToList();
    }

    /// Show flight details for a plane
    public void ShowFlightDetails(Plane plane)
    {
        var query = from f in Flights
                    where f.Plane == plane
                    select f;

        Console.WriteLine($"\nFlight Details for {plane.PlaneType}");
        foreach (var flight in query)
        {
            Console.WriteLine($"Destination: {flight.Destination}, Date: {flight.FlightDate}, Duration: {flight.EstimatedDuration} min");
        }
    }

    /// Count flights in 7 days from startDate
    public int ProgrammedFlightNumber(DateTime startDate)
    {
        var query = from f in Flights
                    where f.FlightDate >= startDate && f.FlightDate < startDate.AddDays(7)
                    select f;
        return query.Count();
    }

    /// Calculate average duration for flights
    public double DurationAverage(string destination)
    {
        var query = from f in Flights
                    where f.Destination == destination
                    select f.EstimatedDuration;
        
        return query.Any() ? query.Average() : 0;
    }

    /// Get flights ordered by duration (descending) 
    public List<Flight> OrderedDurationFlights()
    {
        var query = from f in Flights
                    orderby f.EstimatedDuration descending
                    select f;
        return query.ToList();
    }

    /// Get 3 oldest travellers from a flight 
    public List<Traveller> SeniorTravellers(Flight flight)
    {
        var query = (from p in flight.Passengers
                     where p is Traveller
                     orderby p.BirthDate
                     select p as Traveller).Take(3);
        return query.ToList();
    }


    /// Group flights by destination and display using
    public void DestinationGroupedFlights()
    {
        var query = from f in Flights
                    group f by f.Destination into g
                    select g;

        Console.WriteLine("\nFlights Grouped by Destination");
        foreach (var group in query)
        {
            Console.WriteLine($"\nDestination {group.Key}");
            foreach (var flight in group)
            {
                Console.WriteLine($"Décollage : {flight.FlightDate:dd/MM/yyyy HH:mm:ss}");
            }
        }
    }

    /// Get flight dates 
    public List<DateTime> GetFlightDatesLinqMethods(string destination)
    {
        return Flights
            .Where(f => f.Destination == destination)
            .Select(f => f.FlightDate)
            .ToList();
    }

    /// Show flight details
    public void ShowFlightDetailsMethods(Plane plane)
    {
        var flights = Flights.Where(f => f.Plane == plane);

        Console.WriteLine($"\nFlight Details for {plane.PlaneType}");
        foreach (var flight in flights)
        {
            Console.WriteLine($"Destination: {flight.Destination}, Date: {flight.FlightDate}, Duration: {flight.EstimatedDuration} min");
        }
    }

    /// Count flights within 7 days 
    public int ProgrammedFlightNumberMethods(DateTime startDate)
    {
        return Flights
            .Where(f => f.FlightDate >= startDate && f.FlightDate < startDate.AddDays(7))
            .Count();
    }

    /// Calculate average duration 
    public double DurationAverageMethods(string destination)
    {
        var flights = Flights.Where(f => f.Destination == destination);
        return flights.Any() ? flights.Average(f => f.EstimatedDuration) : 0;
    }

    /// Get flights ordered by duration 
    public List<Flight> OrderedDurationFlightsMethods()
    {
        return Flights
            .OrderByDescending(f => f.EstimatedDuration)
            .ToList();
    }

    /// Get 3 oldest travellers 
    public List<Traveller> SeniorTravellersMethods(Flight flight)
    {
        return flight.Passengers
            .Where(p => p is Traveller)
            .OrderBy(p => p.BirthDate)
            .Take(3)
            .Select(p => p as Traveller)
            .ToList();
    }

    /// Group flights by destination
    public void DestinationGroupedFlightsMethods()
    {
        var groups = Flights.GroupBy(f => f.Destination);

        Console.WriteLine("\n=== Flights Grouped by Destination (Methods) ===");
        foreach (var group in groups)
        {
            Console.WriteLine($"\nDestination {group.Key}");
            foreach (var flight in group)
            {
                Console.WriteLine($"Décollage : {flight.FlightDate:dd/MM/yyyy HH:mm:ss}");
            }
        }
    }
}
