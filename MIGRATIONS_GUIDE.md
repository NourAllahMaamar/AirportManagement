# Entity Framework Core Migrations Guide

## Current Migration Status
Your project has these migrations:
- `20260216085732_InitialCreate` - Initial database schema
- `20260216091654_ConvertToTPT` - Converted to Table-Per-Type inheritance

---

## 📋 Prerequisites

### Update dotnet-ef Tool (Recommended)
Your current tool version (7.0.20) is older than EF Core (9.0.1). Update it:

```bash
# Update global tool
dotnet tool update --global dotnet-ef --version 9.0.1
```

Or install if not present:
```bash
dotnet tool install --global dotnet-ef --version 9.0.1
```

---

## ➕ Adding a New Migration

When you change your domain models or DbContext configuration:

```bash
cd AM.UI.Console
dotnet ef migrations add YourMigrationName --project ../AM.ApplicationCore
```

**Example:**
```bash
dotnet ef migrations add AddPhoneNumberToPassenger --project ../AM.ApplicationCore
```

This creates three files in `AM.ApplicationCore/Migrations/`:
- `{timestamp}_YourMigrationName.cs` - Migration operations
- `{timestamp}_YourMigrationName.Designer.cs` - Metadata
- `AMContextModelSnapshot.cs` - Current model snapshot (updated)

---

## 🚀 Applying Migrations to Database

### Apply All Pending Migrations
```bash
cd AM.UI.Console
dotnet ef database update --project ../AM.ApplicationCore
```

### Apply to Specific Migration
```bash
dotnet ef database update MigrationName --project ../AM.ApplicationCore
```

**Example:**
```bash
dotnet ef database update InitialCreate --project ../AM.ApplicationCore
```

---

## 📜 Listing Migrations

### List All Migrations
```bash
cd AM.UI.Console
dotnet ef migrations list --project ../AM.ApplicationCore
```

Output shows:
- Migration names (applied migrations may be marked differently)
- Timestamps

---

## ⏪ Reverting Migrations

### Option 1: Revert Database to Previous Migration (Recommended)

1. **List migrations to find the target:**
   ```bash
   dotnet ef migrations list --project ../AM.ApplicationCore
   ```

2. **Revert database to that migration:**
   ```bash
   dotnet ef database update PreviousMigrationName --project ../AM.ApplicationCore
   ```
   
   **Example - Revert to InitialCreate:**
   ```bash
   dotnet ef database update InitialCreate --project ../AM.ApplicationCore
   ```

3. **Optionally remove the migration files you don't want:**
   ```bash
   dotnet ef migrations remove --project ../AM.ApplicationCore
   ```
   (Removes the last migration files only if not applied)

### Option 2: Remove Last Migration (Before Applying)

If you added a migration but haven't applied it yet:

```bash
cd AM.UI.Console
dotnet ef migrations remove --project ../AM.ApplicationCore
```

**Note:** This only works if the migration hasn't been applied to the database yet.

### Option 3: Revert All Migrations (Nuclear Option)

Reverts database to empty state:

```bash
cd AM.UI.Console
dotnet ef database update 0 --project ../AM.ApplicationCore
```

Then remove all migration files manually from `AM.ApplicationCore/Migrations/` if desired.

---

## 🔄 Common Workflows

### Scenario 1: Made a mistake in the last migration (not applied yet)
```bash
# Remove the migration files
dotnet ef migrations remove --project ../AM.ApplicationCore

# Make your model changes
# Add the migration again
dotnet ef migrations add FixedMigration --project ../AM.ApplicationCore
```

### Scenario 2: Migration was applied but needs to be reverted
```bash
# Revert database to previous migration
dotnet ef database update PreviousMigrationName --project ../AM.ApplicationCore

# Remove the migration files
dotnet ef migrations remove --project ../AM.ApplicationCore

# Make fixes and create new migration
dotnet ef migrations add CorrectedMigration --project ../AM.ApplicationCore

# Apply it
dotnet ef database update --project ../AM.ApplicationCore
```

### Scenario 3: Start fresh (development only!)
```bash
# Drop all migrations from database
dotnet ef database update 0 --project ../AM.ApplicationCore

# Delete migration files manually
rm -rf AM.ApplicationCore/Migrations/*

# Recreate initial migration
dotnet ef migrations add InitialCreate --project ../AM.ApplicationCore

# Apply it
dotnet ef database update --project ../AM.ApplicationCore
```

---

## 🛠️ Generating SQL Scripts (Production)

For production deployments, generate SQL scripts instead of direct updates:

```bash
# Generate script for all migrations
dotnet ef migrations script --project ../AM.ApplicationCore --output migration.sql

# Generate script from one migration to another
dotnet ef migrations script FromMigration ToMigration --project ../AM.ApplicationCore --output migration.sql

# Generate script for pending migrations only
dotnet ef migrations script --idempotent --project ../AM.ApplicationCore --output migration.sql
```

---

## 📝 Tips & Best Practices

1. **Always backup your database** before applying/reverting migrations in production
2. **Test migrations** in development before production
3. **Use descriptive names** for migrations (e.g., `AddEmailIndexToPassenger` not `Update1`)
4. **Review generated migration code** before applying
5. **Keep dotnet-ef tool version** matching your EF Core package version
6. **Use `--idempotent` flag** for production SQL scripts (can run multiple times safely)
7. **Never delete migrations** that have been applied in production
8. **Commit migrations to source control** with your code changes

---

## 🔍 Troubleshooting

### "Unable to create an object of type 'AMContext'"
- Ensure `AMContextFactory.cs` exists in `AM.ApplicationCore/Data/`
- Check connection string in factory matches your database

### "The migration has already been applied to the database"
- Use `dotnet ef database update PreviousMigration` first, then remove

### Version mismatch warnings
- Update dotnet-ef: `dotnet tool update --global dotnet-ef --version 9.0.1`

### "Build failed"
- Fix compilation errors first
- Ensure all projects build: `dotnet build`

---

## 📂 Project Structure Reference

```
AM.ApplicationCore/
  ├── Data/
  │   ├── AMContext.cs              # Your DbContext
  │   └── AMContextFactory.cs       # Design-time factory for migrations
  └── Migrations/                   # All migration files here
      ├── 20260216085732_InitialCreate.cs
      ├── 20260216091654_ConvertToTPT.cs
      └── AMContextModelSnapshot.cs

AM.UI.Console/
  └── appsettings.json             # Connection string used by migrations
```

---

## Quick Reference Commands

```bash
# From AM.UI.Console directory:

# Add migration
dotnet ef migrations add MigrationName --project ../AM.ApplicationCore

# Apply migrations
dotnet ef database update --project ../AM.ApplicationCore

# List migrations
dotnet ef migrations list --project ../AM.ApplicationCore

# Remove last migration
dotnet ef migrations remove --project ../AM.ApplicationCore

# Revert to specific migration
dotnet ef database update MigrationName --project ../AM.ApplicationCore

# Revert all
dotnet ef database update 0 --project ../AM.ApplicationCore

# Generate SQL script
dotnet ef migrations script --project ../AM.ApplicationCore --output migration.sql
```
