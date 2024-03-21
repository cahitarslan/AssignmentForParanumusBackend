using DataAccess.Concrete.EntityFramework.Mapping;
using Entities.Concrete;
using Entities.Concrete.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.EntityFramework.Contexts;

public class BaseDbContext : IdentityDbContext<AppUser, AppRole, int>
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

        AppRole[] roleEntitySeeds = { new() { Id = 1, Name = "regular" }, new() { Id = 2, Name = "employee" } };

        AppUser[] userEntitySeeds =
        {
            new() { Id = 1, FirstName = "Cahit", LastName = "Arslan", UserName = "cahit@xyz.com", NormalizedUserName = "CAHIT@XYZ.COM", Email = "cahit@xyz.com", NormalizedEmail = "CAHIT@XYZ.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEIVtkjUir56hvfjooppnSKWa4rz5sFyQXa8JFZ1gK2tquXGT6Mrpm0xV4cGMnGOuqg==", SecurityStamp = "BGL56R3AGG36QC3L57PCNBCKR67KUNDJ", ConcurrencyStamp = "8589cfe2-1fd1-4081-8497-6354da859193", PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0  }
        };

        modelBuilder.Entity<Product>().HasData(productEntitySeeds);
        modelBuilder.Entity<AppRole>().HasData(roleEntitySeeds);
        modelBuilder.Entity<AppUser>().HasData(userEntitySeeds);

        base.OnModelCreating(modelBuilder);
    }
}
