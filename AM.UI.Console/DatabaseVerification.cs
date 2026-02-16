using AM.ApplicationCore.Data;
using AM.ApplicationCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace AM.UI.Console;

class DatabaseVerification
{
    public static void VerifyDatabase()
    {
        var connectionString = "Server=127.0.0.1;Port=3306;Database=DotNetTd;Uid=root;Pwd=;";
        var optionsBuilder = new DbContextOptionsBuilder<AMContext>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        using (var context = new AMContext(optionsBuilder.Options))
        {
            System.Console.WriteLine("\n=== DATABASE VERIFICATION ===\n");
            
            System.Console.WriteLine($"Total Passengers in DB: {context.Passengers.Count()}");
            System.Console.WriteLine($"Total Staff in DB: {context.Staffs.Count()}");
            System.Console.WriteLine($"Total Travellers in DB: {context.Travellers.Count()}");
            System.Console.WriteLine($"Total Flights in DB: {context.Flights.Count()}");
            System.Console.WriteLine($"Total Planes in DB: {context.Planes.Count()}");
            
            System.Console.WriteLine("\nPlanes in database:");
            foreach (var plane in context.Planes)
            {
                System.Console.WriteLine($"  - {plane.PlaneType}, Capacity: {plane.Capacity}");
            }
            
            System.Console.WriteLine("\nFlights in database:");
            foreach (var flight in context.Flights.Include(f => f.Plane))
            {
                System.Console.WriteLine($"  - To {flight.Destination} on {flight.FlightDate:dd/MM/yyyy} with {flight.Plane?.PlaneType}");
            }
            
            System.Console.WriteLine("\nPassengers in database:");
            foreach (var passenger in context.Passengers)
            {
                var type = passenger is Staff ? "Staff" : passenger is Traveller ? "Traveller" : "Passenger";
                System.Console.WriteLine($"  - {passenger.FirstName} {passenger.LastName} ({type})");
            }
        }
    }
}
