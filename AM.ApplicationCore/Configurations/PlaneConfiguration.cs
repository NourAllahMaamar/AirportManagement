using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AM.ApplicationCore.Domain;

namespace AM.ApplicationCore.Configurations;

/// Fluent API configuration for Plane entity
public class PlaneConfiguration : IEntityTypeConfiguration<Plane>
{
    public void Configure(EntityTypeBuilder<Plane> builder)
    {
        builder.HasKey(p => p.PlaneId);
        
        builder.ToTable("MyPlanes");
        
        builder.Property(p => p.Capacity)
            .HasColumnName("PlaneCapacity");
    }
}
