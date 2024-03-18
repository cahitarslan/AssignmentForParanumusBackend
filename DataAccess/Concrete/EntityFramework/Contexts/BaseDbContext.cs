using DataAccess.Concrete.EntityFramework.Mapping;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.EntityFramework.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<Product> Products { get; set; }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    base.OnConfiguring(optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ParanusmusDb;Trusted_connection=true"));
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductMap());


        Product[] productEntitySeeds = { new() { Id = 1, Name = "Phone", Price = 10000, Description = "Description for Phone" },
                                         new() { Id = 2, Name = "Desktop", Price = 30000, Description = "Description for Desktop" },
                                         new() { Id = 3, Name = "Laptop", Price = 40000, Description = "Description for Laptop" },
                                         new() { Id = 4, Name = "TV", Price = 50000, Description = "Description for TV" } };

        modelBuilder.Entity<Product>().HasData(productEntitySeeds);

        base.OnModelCreating(modelBuilder);
    }
}
