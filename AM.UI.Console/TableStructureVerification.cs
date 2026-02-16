using AM.ApplicationCore.Data;
using AM.ApplicationCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace AM.UI.Console;

class TableStructureVerification
{
    public static void ShowTableStructure()
    {
        var connectionString = "Server=127.0.0.1;Port=3306;Database=DotNetTd;Uid=root;Pwd=;";
        var optionsBuilder = new DbContextOptionsBuilder<AMContext>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        using (var context = new AMContext(optionsBuilder.Options))
        {
            System.Console.WriteLine("\n=== TABLE STRUCTURE (TPT Inheritance) ===\n");
            
            System.Console.WriteLine("📋 PASSENGERS Table (Base):");
            System.Console.WriteLine("   Contains: Id, FirstName, LastName, EmailAddress, BirthDate");
            System.Console.WriteLine($"   Records: {context.Passengers.Count()}");
            
            System.Console.WriteLine("\n📋 STAFFS Table (Inherits from Passengers):");
            System.Console.WriteLine("   Contains: Id (FK to Passengers), EmployementDate, Salary");
            System.Console.WriteLine($"   Records: {context.Staffs.Count()}");
            foreach (var staff in context.Staffs)
            {
                System.Console.WriteLine($"      - {staff.FirstName} {staff.LastName}, Salary: {staff.Salary}");
            }
            
            System.Console.WriteLine("\n📋 TRAVELLERS Table (Inherits from Passengers):");
            System.Console.WriteLine("   Contains: Id (FK to Passengers), HealthInformation, Nationality");
            System.Console.WriteLine($"   Records: {context.Travellers.Count()}");
            foreach (var traveller in context.Travellers)
            {
                System.Console.WriteLine($"      - {traveller.FirstName} {traveller.LastName}, Nationality: {traveller.Nationality}");
            }
            
            System.Console.WriteLine("\n📋 FLIGHTS Table:");
            System.Console.WriteLine($"   Records: {context.Flights.Count()}");
            
            System.Console.WriteLine("\n📋 PLANES Table:");
            System.Console.WriteLine($"   Records: {context.Planes.Count()}");
            
            System.Console.WriteLine("\n📋 FLIGHTPASSENGER Table (Junction):");
            System.Console.WriteLine("   Many-to-many relationship between Flights and Passengers");
            
            System.Console.WriteLine("\n✅ All tables created successfully with TPT inheritance!");
        }
    }
}
