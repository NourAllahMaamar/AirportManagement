using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AM.ApplicationCore.Data;

public class AMContextFactory : IDesignTimeDbContextFactory<AMContext>
{
    public AMContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AMContext>();
        
        var connectionString = "Server=localhost;Database=DotNetTd;User=root;Password=;";
        
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        
        return new AMContext(optionsBuilder.Options);
    }
}
