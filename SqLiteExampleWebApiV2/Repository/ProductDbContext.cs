using Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repositories
{
    public class ProductDbContext : DbContext
    {
        private IConfiguration _configuration;

        public ProductDbContext()
        {
        }

        public ProductDbContext(DbContextOptions<ProductDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>().HasKey(k => k.Id);
            modelBuilder.Entity<Product>(e => e.Property<int>("Id").ValueGeneratedOnAdd());
            modelBuilder.Entity<Product>().HasIndex(i => i.Name).IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("ProductsDB"), option => { });
            base.OnConfiguring(optionsBuilder);
        }
    }
}
