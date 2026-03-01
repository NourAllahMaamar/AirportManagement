using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AM.ApplicationCore.Domain;

namespace AM.ApplicationCore.Configurations;

/// <summary>
/// Fluent API configuration for Plane entity
/// </summary>
public class PlaneConfiguration : IEntityTypeConfiguration<Plane>
{
    public void Configure(EntityTypeBuilder<Plane> builder)
    {
        // Configure Primary Key
        builder.HasKey(p => p.PlaneId);
        
        // Configure Table Name
        builder.ToTable("MyPlanes");
        
        // Rename Capacity column to PlaneCapacity
        builder.Property(p => p.Capacity)
            .HasColumnName("PlaneCapacity");
    }
}
