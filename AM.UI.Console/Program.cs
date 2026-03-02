using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Data;
using System.IO;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace AM.UI.Console;

class Program
{
    static void Main(string[] args)
    {
        System.Console.WriteLine("=".PadRight(60, '='));
        System.Console.WriteLine("Airport Management System - EF Core TP");
        System.Console.WriteLine("=".PadRight(60, '='));

        // Load DB connection
        string? connectionString = null;
        try
        {
            var configText = File.ReadAllText("appsettings.json");
            using var configJson = JsonDocument.Parse(configText);
            if (configJson.RootElement.TryGetProperty("ConnectionStrings", out var csSection) &&
                csSection.TryGetProperty("DefaultConnection", out var csValue))
            {
                connectionString = csValue.GetString();
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Warning: could not read appsettings.json: {ex.Message}");
        }
        System.Console.WriteLine($"\nDB Connection: {connectionString}\n");

        // Configure DbContext with Lazy Loading enabled
        var optionsBuilder = new DbContextOptionsBuilder<AMContext>();
        optionsBuilder
            .UseLazyLoadingProxies() 
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));



        // === PART 1: INSERT TEST DATA ===
        // This section inserts sample data for all entities
        using (var context = new AMContext(optionsBuilder.Options))
        {
            System.Console.WriteLine("\n--- INSERTING TEST DATA ---");
            
            // Only add if database is empty
            if (!context.Planes.Any())
            {
                // Create and add Planes
                var boeingPlane = new Plane
                {
                    PlaneType = PlaneType.Boing,
                    Capacity = 300,
                    ManufactureDate = new DateTime(2020, 6, 15)
                };
                
                var airbusPlane = new Plane
                {
                    PlaneType = PlaneType.Airbus,
                    Capacity = 250,
                    ManufactureDate = new DateTime(2019, 3, 20)
                };
                
                context.Planes.AddRange(boeingPlane, airbusPlane);
                System.Console.WriteLine($"✓ Added 2 Planes");
                
                // Create Staff members
                var captain = new Staff
                {
                    PassportNumber = "ABC1234",
                    FirstName = "John",
                    LastName = "Smith",
                    EmailAddress = "john.smith@airline.com",
                    BirthDate = new DateTime(1985, 5, 15),
                    TelNumber = "12345678",
                    EmployementDate = new DateTime(2010, 1, 1),
                    Salary = 8500.00m
                };
                
                var hostess = new Staff
                {
                    PassportNumber = "DEF5678",
                    FirstName = "Emma",
                    LastName = "Johnson",
                    EmailAddress = "emma.j@airline.com",
                    BirthDate = new DateTime(1990, 8, 22),
                    TelNumber = "87654321",
                    EmployementDate = new DateTime(2015, 6, 15),
                    Salary = 4200.00m
                };
                
                context.Staffs.AddRange(captain, hostess);
                System.Console.WriteLine($"✓ Added 2 Staff members");
                
                // Create Travellers
                var traveller1 = new Traveller
                {
                    PassportNumber = "GHI9012",
                    FirstName = "Alice",
                    LastName = "Brown",
                    EmailAddress = "alice.b@email.com",
                    BirthDate = new DateTime(1995, 12, 10),
                    TelNumber = "11223344",
                    HealthInformation = "No allergies",
                    Nationality = "American"
                };
                
                var traveller2 = new Traveller
                {
                    PassportNumber = "JKL3456",
                    FirstName = "Bob",
                    LastName = "Wilson",
                    EmailAddress = "bob.w@email.com",
                    BirthDate = new DateTime(1988, 3, 25),
                    TelNumber = "55667788",
                    HealthInformation = "Diabetic",
                    Nationality = "Canadian"
                };
                
                var traveller3 = new Traveller
                {
                    PassportNumber = "MNO7890",
                    FirstName = "Clara",
                    LastName = "Davis",
                    EmailAddress = "clara.d@email.com",
                    BirthDate = new DateTime(2000, 7, 5),
                    TelNumber = "99887766",
                    HealthInformation = "Healthy",
                    Nationality = "British"
                };
                
                context.Travellers.AddRange(traveller1, traveller2, traveller3);
                System.Console.WriteLine($"✓ Added 3 Travellers");
                
                // Save passengers first
                context.SaveChanges();
                
                // Create Flights
                var flight1 = new Flight
                {
                    Destination = "Paris",
                    FlightDate = new DateTime(2026, 4, 15),
                    EffectiveArrival = new DateTime(2026, 4, 15, 14, 30, 0),
                    EstimatedDuration = 180,
                    AirlineLogo = "AirFrance",
                    Plane = boeingPlane
                };
                flight1.Passengers.Add(traveller1);
                flight1.Passengers.Add(traveller2);
                flight1.Passengers.Add(captain);
                
                var flight2 = new Flight
                {
                    Destination = "London",
                    FlightDate = new DateTime(2026, 4, 20),
                    EffectiveArrival = new DateTime(2026, 4, 20, 10, 15, 0),
                    EstimatedDuration = 120,
                    AirlineLogo = "BritishAir",
                    Plane = airbusPlane
                };
                flight2.Passengers.Add(traveller3);
                flight2.Passengers.Add(hostess);
                
                context.Flights.AddRange(flight1, flight2);
                System.Console.WriteLine($"✓ Added 2 Flights with passengers");
                
                // Save flights
                context.SaveChanges();
                
                // Create Tickets
                var ticket1 = new Ticket
                {
                    Price = 450.50m,
                    PassengerId = traveller1.PassportNumber,
                    FlightId = flight1.FlightId,
                    Passenger = traveller1,
                    Flight = flight1
                };
                
                var ticket2 = new Ticket
                {
                    Price = 475.00m,
                    PassengerId = traveller2.PassportNumber,
                    FlightId = flight1.FlightId,
                    Passenger = traveller2,
                    Flight = flight1
                };
                
                var ticket3 = new Ticket
                {
                    Price = 320.00m,
                    PassengerId = traveller3.PassportNumber,
                    FlightId = flight2.FlightId,
                    Passenger = traveller3,
                    Flight = flight2
                };
                
                var ticket4 = new Ticket
                {
                    Price = 0.00m,  // Staff ticket (free)
                    PassengerId = captain.PassportNumber,
                    FlightId = flight1.FlightId,
                    Passenger = captain,
                    Flight = flight1
                };
                
                context.Tickets.AddRange(ticket1, ticket2, ticket3, ticket4);
                System.Console.WriteLine($"✓ Added 4 Tickets");
                
                context.SaveChanges();
                System.Console.WriteLine("✓ All changes saved to database\n");
            }
            else
            {
                System.Console.WriteLine("✓ Database already contains data (skipping insert)\n");
            }
        }



        // === PART 2: DEMONSTRATE LAZY LOADING ===
        System.Console.WriteLine("\n--- DEMONSTRATING LAZY LOADING ---");
        
        using (var context = new AMContext(optionsBuilder.Options))
        {
            // Ensure database is created
            context.Database.EnsureCreated();
            
            // Retrieve a flight without explicitly loading the Plane
            var flight = context.Flights.FirstOrDefault();
            
            if (flight != null)
            {
                System.Console.WriteLine($"\n✓ Retrieved Flight ID: {flight.FlightId}");
                System.Console.WriteLine($"  Destination: {flight.Destination}");
                System.Console.WriteLine($"  Flight Date: {flight.FlightDate:yyyy-MM-dd}");
                
                System.Console.WriteLine("\n⚡ LAZY LOADING IN ACTION:");
                System.Console.WriteLine("   Accessing flight.Plane property...");
                
               
                if (flight.Plane != null)
                {
                    System.Console.WriteLine($"   ✓ Plane loaded automatically via Lazy Loading!");
                    System.Console.WriteLine($"   Plane Type: {flight.Plane.PlaneType}");
                    System.Console.WriteLine($"   Capacity: {flight.Plane.Capacity}");
                    System.Console.WriteLine($"   Manufacture Date: {flight.Plane.ManufactureDate:yyyy-MM-dd}");
                }
                else
                {
                    System.Console.WriteLine("   No Plane associated with this Flight");
                }
            }
            else
            {
                System.Console.WriteLine("\nNo flights found in database.");
                System.Console.WriteLine("Please uncomment the INSERT TEST DATA section above,");
                System.Console.WriteLine("run the program once to add data, then comment it again");
                System.Console.WriteLine("and run again to see Lazy Loading in action.");
            }
        }

        System.Console.WriteLine("\n" + "=".PadRight(60, '='));
        System.Console.WriteLine("Press any key to exit...");
        System.Console.ReadKey();
    }
}
