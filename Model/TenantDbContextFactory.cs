using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MultiTenant.Model
{
    public class TenantDbContextFactory : IDesignTimeDbContextFactory<TenantDbContext>
    {
        public TenantDbContext CreateDbContext(string[] args)
        {
            //Build the configuration by reading from the appsettings.json file(requires Microsoft.Extensions.Configuration.Json Nuget Package)
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Retrieve the connection string from the configuration
            string connectionString = configuration.GetConnectionString("DefaultConnection");


            DbContextOptionsBuilder<TenantDbContext> optionsBuilder = new();
            _ = optionsBuilder.UseSqlServer(connectionString);
            return new TenantDbContext(optionsBuilder.Options);
        }
    }
}
