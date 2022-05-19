using System.IO;
using System.Reflection;
using CarDbRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CarSaloonEvidence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CarDbContext>
    {
        public CarDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json")
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .AddEnvironmentVariables()
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnectionString");

            DbContextOptionsBuilder<CarDbContext> optionsBuilder = new DbContextOptionsBuilder<CarDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new CarDbContext(optionsBuilder.Options);
        }
    }
}