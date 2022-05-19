using CarSaloonEvidence.CarModel;
using Microsoft.EntityFrameworkCore;

namespace CarDbRepository
{
    // DbContext: represents the database.
    public class CarDbContext : DbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> context)
            : base(context)
        {
        }

        // DbSet: represents the table in the database - the collection of Cars in this case.
        public DbSet<Car> Cars { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Car).Assembly);
        }
    }
}
