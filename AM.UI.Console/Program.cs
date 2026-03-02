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

        var optionsBuilder = new DbContextOptionsBuilder<AMContext>();
        optionsBuilder
            .UseLazyLoadingProxies() 
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));



        // PART 1 insert test data 
        using (var context = new AMContext(optionsBuilder.Options))
        {
            System.Console.WriteLine("\nINSERTING TEST DATA");
            
            if (!context.Planes.Any())
            {
                context.Planes.AddRange(TestData.BoingPlane, TestData.AirbusPlane);
                System.Console.WriteLine($"✓ Added 2 Planes");
                
                context.SaveChanges();
                
                context.Flights.AddRange(TestData.listFlights);
                System.Console.WriteLine($"✓ Added {TestData.listFlights.Count} Flights");
                
                context.SaveChanges();
                System.Console.WriteLine("✓ All changes saved to database\n");
            }
            else
            {
                System.Console.WriteLine("✓ Database already contains data (skipping insert)\n");
            }
        }



        // PART 2: lazy loading
        System.Console.WriteLine("\nLAZY LOADING");
        
        using (var context = new AMContext(optionsBuilder.Options))
        {
            context.Database.EnsureCreated();
            
            var flight = context.Flights.FirstOrDefault();
            
            if (flight != null)
            {
                System.Console.WriteLine($"\n✓ Retrieved Flight ID: {flight.FlightId}");
                System.Console.WriteLine($"  Destination: {flight.Destination}");
                System.Console.WriteLine($"  Flight Date: {flight.FlightDate:yyyy-MM-dd}");
                
                
               
                if (flight.Plane != null)
                {
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
                
            }
        }

        System.Console.WriteLine("\n" + "=".PadRight(60, '='));
        System.Console.WriteLine("Press any key to exit...");
        System.Console.ReadKey();
    }
}
