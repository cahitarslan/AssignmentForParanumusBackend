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


        Product[] productEntitySeeds = { new() { Id = 1, Name = "Book 1", Price = 10, Description = "Description for Book 1" },
                                         new() { Id = 2, Name = "Book 2", Price = 20, Description = "Description for Book 2" },
                                         new() { Id = 3, Name = "Book 3", Price = 15, Description = "Description for Book 3" },
                                         new() { Id = 4, Name = "Book 4", Price = 30, Description = "Description for Book 4" } };

        modelBuilder.Entity<Product>().HasData(productEntitySeeds);

        base.OnModelCreating(modelBuilder);
    }
}
