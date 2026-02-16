using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Services;
using AM.ApplicationCore.Data;
using System.IO;
using System.Text.Json;
using MySqlConnector;
using Microsoft.EntityFrameworkCore;

namespace AM.UI.Console;

class Program
{
    static void Main(string[] args)
    {
        // Load DB connection string from appsettings.json
        string connectionString = null;
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
        System.Console.WriteLine($"DB Connection: {connectionString}");

        // Configure DbContext with options
        var optionsBuilder = new DbContextOptionsBuilder<AMContext>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        // Create database and apply migrations
        using (var context = new AMContext(optionsBuilder.Options))
        {
            try
            {
                System.Console.WriteLine("Applying database migrations...");
                context.Database.Migrate();
                System.Console.WriteLine("✓ Database 'DotNetTd' is ready with EF Core.\n");

                // Seed data if database is empty
                if (!context.Planes.Any())
                {
                    System.Console.WriteLine("Seeding initial data...");
                    SeedData(context);
                    System.Console.WriteLine("✓ Data seeded successfully.\n");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"⚠ Database migration failed: {ex.Message}\n");
            }
        }

        FlightMethods flightService = new FlightMethods();
        flightService.Flights = TestData.listFlights;




        // TEST 1: Polymorphism - PassengerType()
        System.Console.WriteLine("\nTEST 1: Polymorphism - PassengerType()");
        
        Passenger passenger = new Passenger { FirstName = "Generic", LastName = "Passenger" };
        System.Console.Write("Regular Passenger: ");
        passenger.PassengerType();

        System.Console.Write("\nStaff Member: ");
        TestData.Captain.PassengerType();

        System.Console.Write("\nTraveller: ");
        TestData.Traveller1.PassengerType();

        // TEST 2: CheckProfile overloading
        System.Console.WriteLine("\n\nTEST 2: CheckProfile Method Overloading");
        
        System.Console.WriteLine($"Check profile (FirstName, LastName): {TestData.Traveller1.CheckProfile("John", "Doe")}");
        System.Console.WriteLine($"Check profile (FirstName, LastName, Email): {TestData.Traveller1.CheckProfile("John", "Doe", "john.doe@email.com")}");
        System.Console.WriteLine($"Check profile with optional email: {TestData.Traveller1.CheckProfile("John", "Doe")}");

        // TEST 3: Extension Method - UpperFullName
        System.Console.WriteLine("\n\n TEST 3: Extension Method - UpperFullName");
        
        System.Console.WriteLine($"Traveller1 Upper Full Name: {TestData.Traveller1.UpperFullName()}");
        System.Console.WriteLine($"Captain Upper Full Name: {TestData.Captain.UpperFullName()}");
        System.Console.WriteLine($"Hostess1 Upper Full Name: {TestData.Hostess1.UpperFullName()}");

        // TEST 4: GetFlightDates (FOR loop)
        System.Console.WriteLine("\n\n TEST 4: GetFlightDates - FOR Loop");
        
        var parisFlightDates = flightService.GetFlightDates("Paris");
        System.Console.WriteLine("Paris Flight Dates:");
        foreach (var date in parisFlightDates)
        {
            System.Console.WriteLine($"  - {date:dd/MM/yyyy HH:mm:ss}");
        }

        // TEST 5: GetFlightDates (LINQ Query)
        System.Console.WriteLine("\n\n TEST 5: GetFlightDatesLinq - LINQ Query Syntax");
        
        var madridFlightDates = flightService.GetFlightDatesLinq("Madrid");
        System.Console.WriteLine("Madrid Flight Dates:");
        foreach (var date in madridFlightDates)
        {
            System.Console.WriteLine($"  - {date:dd/MM/yyyy HH:mm:ss}");
        }

        // TEST 6: GetFlightDates (LINQ Methods)
        System.Console.WriteLine("\n\n TEST 6: GetFlightDatesLinqMethods - LINQ Predefined Methods");
        
        var parisFlightDatesLinq = flightService.GetFlightDatesLinqMethods("Paris");
        System.Console.WriteLine("Paris Flight Dates (LINQ Methods):");
        foreach (var date in parisFlightDatesLinq)
        {
            System.Console.WriteLine($"  - {date:dd/MM/yyyy HH:mm:ss}");
        }

        // TEST 7: GetFlights with Reflection
        System.Console.WriteLine("\n\nTEST 7: GetFlights - Reflection Filter");
        
        var filteredFlights = flightService.GetFlights("Destination", "Paris");
        System.Console.WriteLine($"Flights to Paris (using reflection): {filteredFlights.Count} flights");
        foreach (var flight in filteredFlights)
        {
            System.Console.WriteLine($"  - {flight}");
        }

        // TEST 8: ShowFlightDetails 
        System.Console.WriteLine("\n\n TEST 8: ShowFlightDetails - LINQ Query Syntax");
        
        flightService.ShowFlightDetails(TestData.BoingPlane);

        // TEST 9: ShowFlightDetails
        System.Console.WriteLine("\n\n TEST 9: ShowFlightDetails - LINQ Predefined Methods");
        
        flightService.ShowFlightDetailsMethods(TestData.AirbusPlane);

        // TEST 10: ProgrammedFlightNumber
        System.Console.WriteLine("\n\n TEST 10: ProgrammedFlightNumber - LINQ Query Syntax");
        
        var startDate = new DateTime(2022, 1, 1);
        var flightCount = flightService.ProgrammedFlightNumber(startDate);
        System.Console.WriteLine($"Flights within 7 days from {startDate:dd/MM/yyyy}: {flightCount}");

        // TEST 11: ProgrammedFlightNumber 
        System.Console.WriteLine("\n\n TEST 11: ProgrammedFlightNumber - LINQ Predefined Methods");
        
        var flightCountMethods = flightService.ProgrammedFlightNumberMethods(startDate);
        System.Console.WriteLine($"Flights within 7 days from {startDate:dd/MM/yyyy}: {flightCountMethods}");






        // TEST 12: DurationAverage 
        System.Console.WriteLine("\n\n TEST 12: DurationAverage - LINQ Query Syntax");
        
        var avgDuration = flightService.DurationAverage("Paris");
        System.Console.WriteLine($"Average duration for Paris flights: {avgDuration:F2} minutes");

        // TEST 13: DurationAverage 
        System.Console.WriteLine("\n\n TEST 13: DurationAverage - LINQ Predefined Methods");
        
        var avgDurationMethods = flightService.DurationAverageMethods("Paris");
        System.Console.WriteLine($"Average duration for Paris flights: {avgDurationMethods:F2} minutes");

        // TEST 14: OrderedDurationFlights 
        System.Console.WriteLine("\n\n TEST 14: OrderedDurationFlights - LINQ Query Syntax");
        
        var orderedFlights = flightService.OrderedDurationFlights();
        System.Console.WriteLine("Flights ordered by duration (descending):");
        foreach (var flight in orderedFlights)
        {
            System.Console.WriteLine($"  - {flight.Destination}: {flight.EstimatedDuration} min");
        }

        // TEST 15: OrderedDurationFlights 
        System.Console.WriteLine("\n\n TEST 15: OrderedDurationFlights - LINQ Predefined Methods");
        
        var orderedFlightsMethods = flightService.OrderedDurationFlightsMethods();
        System.Console.WriteLine("Flights ordered by duration (descending):");
        foreach (var flight in orderedFlightsMethods)
        {
            System.Console.WriteLine($"  - {flight.Destination}: {flight.EstimatedDuration} min");
        }

        // TEST 16: SeniorTravellers 
        System.Console.WriteLine("\n\n TEST 16: SeniorTravellers - LINQ Query Syntax");
        
        var seniorTravellers = flightService.SeniorTravellers(TestData.Flight1);
        System.Console.WriteLine("3 Oldest Travellers on Flight 1:");
        foreach (var traveller in seniorTravellers)
        {
            System.Console.WriteLine($"  - {traveller.FirstName} {traveller.LastName}, Born: {traveller.BirthDate:dd/MM/yyyy}");
        }

        // TEST 17: SeniorTravellers 
        System.Console.WriteLine("\n\n TEST 17: SeniorTravellers - LINQ Predefined Methods");
        
        var seniorTravellersMethods = flightService.SeniorTravellersMethods(TestData.Flight1);
        System.Console.WriteLine("3 Oldest Travellers on Flight 1:");
        foreach (var traveller in seniorTravellersMethods)
        {
            System.Console.WriteLine($"  - {traveller.FirstName} {traveller.LastName}, Born: {traveller.BirthDate:dd/MM/yyyy}");
        }

        // TEST 18: DestinationGroupedFlights 
        System.Console.WriteLine("\n\n TEST 18: DestinationGroupedFlights - LINQ Query Syntax");
        
        flightService.DestinationGroupedFlights();

        // TEST 19: DestinationGroupedFlights 
        System.Console.WriteLine("\n\n TEST 19: DestinationGroupedFlights - LINQ Predefined Methods");
        
        flightService.DestinationGroupedFlightsMethods();

        // TEST 20: ToString() Overrides
        System.Console.WriteLine("\n\n TEST 20: ToString() Overrides");
        
        System.Console.WriteLine($"Plane: {TestData.BoingPlane}");
        System.Console.WriteLine($"Passenger: {TestData.Traveller1}");
        System.Console.WriteLine($"Staff: {TestData.Captain}");
        System.Console.WriteLine($"Flight: {TestData.Flight1}");

        // Verify Database
        DatabaseVerification.VerifyDatabase();
    }

    static void CreateDatabaseIfNotExists(string connectionString)
    {
        var builder = new MySqlConnectionStringBuilder(connectionString);
        string dbName = builder.Database;
        builder.Database = null; 

        using (var connection = new MySqlConnection(builder.ConnectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"CREATE DATABASE IF NOT EXISTS `{dbName}` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;";
                command.ExecuteNonQuery();
                System.Console.WriteLine($"Database '{dbName}' checked/created successfully.");
            }
        }
    }

    static void SeedData(AMContext context)
    {
        // Add planes
        context.Planes.AddRange(TestData.BoingPlane, TestData.AirbusPlane);
        context.SaveChanges();

        // Add passengers (Staff and Travellers)
        context.Passengers.AddRange(
            TestData.Captain,
            TestData.Hostess1,
            TestData.Hostess2,
            TestData.Traveller1,
            TestData.Traveller2,
            TestData.Traveller3,
            TestData.Traveller4,
            TestData.Traveller5
        );
        context.SaveChanges();

        // Add flights with their relationships
        foreach (var flight in TestData.listFlights)
        {
            // Attach the plane to avoid duplicate insertion
            if (flight.Plane != null)
            {
                var existingPlane = context.Planes.Local.FirstOrDefault(p => p.PlaneType == flight.Plane.PlaneType && p.Capacity == flight.Plane.Capacity);
                if (existingPlane != null)
                {
                    flight.Plane = existingPlane;
                    flight.PlaneId = existingPlane.Id;
                }
            }

            // Attach passengers to avoid duplicate insertion
            var attachedPassengers = new List<Passenger>();
            foreach (var passenger in flight.Passengers.ToList())
            {
                var existingPassenger = context.Passengers.Local.FirstOrDefault(p => 
                    p.FirstName == passenger.FirstName && 
                    p.LastName == passenger.LastName && 
                    p.EmailAddress == passenger.EmailAddress);
                
                if (existingPassenger != null)
                {
                    attachedPassengers.Add(existingPassenger);
                }
            }
            
            flight.Passengers.Clear();
            foreach (var passenger in attachedPassengers)
            {
                flight.Passengers.Add(passenger);
            }
        }

        context.Flights.AddRange(TestData.listFlights);
        context.SaveChanges();
    }
}
