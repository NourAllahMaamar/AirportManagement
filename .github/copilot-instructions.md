# Airport Management System - AI Coding Agent Instructions

## Architecture Overview

This is a .NET 10.0 Airport Management System using **Entity Framework Core 9.0** with **MySQL database** (via Pomelo provider). The solution follows a clean architecture pattern with two main projects:

- **AM.ApplicationCore**: Domain models, EF Core DbContext, configurations, services, and migrations
- **AM.UI.Console**: Console application for testing and demonstration

## Database Configuration

**Critical**: This project uses MySQL, NOT SQLite (despite old package references).

- **Database**: MySQL on `localhost`, database name `DotNetTd`, user `root`, no password
- **Connection string** is in `AM.UI.Console/appsettings.json` AND hardcoded in `AMContext.cs` and `AMContextFactory.cs`
- **Lazy Loading** is enabled via `UseLazyLoadingProxies()` - all navigation properties are `virtual`
- **DateTime convention**: All DateTime properties mapped to SQL `date` type (see `AMContext.ConfigureConventions`)

## Entity Framework Patterns

### TPT (Table Per Type) Inheritance Strategy
The `Passenger` hierarchy uses TPT inheritance with separate tables:
- Base table: `Passengers`
- Derived tables: `Staffs`, `Travellers`
- Configured in `AMContext.OnModelCreating()` with `ToTable()` calls
- **Note**: TPH strategy code is commented out but preserved for reference

### Fluent API Configuration
Each entity has a dedicated configuration class implementing `IEntityTypeConfiguration<T>`:
- Located in `AM.ApplicationCore/Configurations/`
- Auto-discovered via `modelBuilder.ApplyConfigurationsFromAssembly()`
- Example: `FlightConfiguration.cs` defines Flight-Plane relationship and many-to-many Flight-Passenger

### Required Properties Pattern
All non-nullable reference types use C# 11 `required` modifier:
```csharp
public required string PassportNumber { get; set; }
public required virtual Passenger Passenger { get; set; }
```
**Important**: When creating entities with required navigation properties, you MUST initialize them in object initializers:
```csharp
var ticket = new Ticket {
    PassengerId = passenger.PassportNumber,
    Passenger = passenger,  // Required!
    FlightId = flight.FlightId,
    Flight = flight         // Required!
};
```

### Type Filtering with LINQ
Use `OfType<T>()` instead of `Where(x => x is T).Select(x => x as T)` to avoid nullability warnings:
```csharp
// ✅ Correct
flight.Passengers.OfType<Traveller>().OrderBy(p => p.BirthDate).ToList();

// ❌ Avoid (nullability issue)
flight.Passengers.Where(p => p is Traveller).Select(p => p as Traveller).ToList();
```

## Migration Workflow

**Always run migrations from the ApplicationCore project directory**:
```bash
cd AM.ApplicationCore

# Create migration
dotnet ef migrations add MigrationName

# Apply migration
dotnet ef database update

# Remove last migration
dotnet ef migrations remove --force

# Drop database (for fresh start)
dotnet ef database drop --force
```

The `AMContextFactory` provides design-time DbContext for migrations with hardcoded connection string.

## Build & Run

```bash
# Build entire solution
dotnet build

# Run console app
cd AM.UI.Console
dotnet run

# Restore packages
dotnet restore
```

## Code Organization Conventions

### Namespaces
- Domain models: `AM.ApplicationCore.Domain`
- DbContext: `AM.ApplicationCore.Data`
- Configurations: `AM.ApplicationCore.Configurations`
- Services: `AM.ApplicationCore.Services`
- Interfaces: `AM.ApplicationCore.Interfaces`

### Domain Model Patterns
- **Constructors**: Parameterless constructor initializes collections (required for EF Core)
- **Collections**: Always initialize as `new List<T>()` in constructor
- **Virtual properties**: All navigation properties marked `virtual` for lazy loading
- **ToString()**: Override for readable object representation

### Validation Attributes
The project uses Data Annotations extensively:
- `[StringLength(7, MinimumLength = 7)]` for PassportNumber
- `[RegularExpression(@"^\d{8}$")]` for TelNumber (exactly 8 digits)
- `[EmailAddress]` for email validation
- `[Range(1, int.MaxValue)]` for positive integers
- `[Required]` on non-nullable properties

## Known Gotchas

1. **Nullable Context**: Uses `<Nullable>enable</Nullable>` - be strict about nullability
2. **Connection String Duplication**: Must update in THREE places when changing database:
   - `appsettings.json`
   - `AMContext.OnConfiguring()`
   - `AMContextFactory.CreateDbContext()`
3. **MySQL Package**: Uses Pomelo.EntityFrameworkCore.MySql 9.0.0 (not Microsoft's provider)
4. **Lazy Loading Performance**: Be aware that navigation property access triggers DB queries
5. **TPT Queries**: Joining derived types generates complex SQL - prefer base table queries when possible

## Testing Data

`TestData.cs` provides static sample data for all entities. When creating new test passengers:
- PassportNumber must be exactly 7 characters
- TelNumber must be exactly 8 digits
- All required string properties must be set
- Staff: Add `EmployementDate` and `Salary`
- Traveller: Add `HealthInformation` and `Nationality`
