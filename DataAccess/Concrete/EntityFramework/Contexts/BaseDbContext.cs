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
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
    }

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
            new() { Id = 1, FirstName = "Cahit", LastName = "Arslan", UserName = "cahit@xyz.com", NormalizedUserName = "CAHIT@XYZ.COM", Email = "cahit@xyz.com", NormalizedEmail = "CAHIT@XYZ.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEIVtkjUir56hvfjooppnSKWa4rz5sFyQXa8JFZ1gK2tquXGT6Mrpm0xV4cGMnGOuqg==", SecurityStamp = "BGL56R3AGG36QC3L57PCNBCKR67KUNDJ", ConcurrencyStamp = "8589cfe2-1fd1-4081-8497-6354da859193", PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0  },
            new() { Id = 2, FirstName = "Ahmet", LastName = "Yılmaz", UserName = "ahmet@xyz.com", NormalizedUserName = "AHMET@XYZ.COM", Email = "ahmet@xyz.com", NormalizedEmail = "AHMET@XYZ.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEJ0mB4+wZ3qjHPz9vp5g/i1B8tkrS8Bu5T3FxQliPBoZ2tqaLj+cmDUdN6LMiEzubw==", SecurityStamp = "US75LHFSJCOKMLCGFKDNOIONICCEUOSX", ConcurrencyStamp = "53242214-d1ef-41f5-90f2-78b8a085bd76", PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0  },
             new() { Id = 3, FirstName = "Ayşe", LastName = "Öztürk", UserName = "ayse@xyz.com", NormalizedUserName = "AYSE@XYZ.COM", Email = "ayse@xyz.com", NormalizedEmail = "AYSE@XYZ.COM", EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAEL7K3WoDfVir09s0qhsY/MWEGwtkGUqCvDPFSVR++cnpsO3yTU8JqdXFBhBf1AJbIw==", SecurityStamp = "MYBC2DBVXHIKOTZI6O4BNFMCNNETLGQ2", ConcurrencyStamp = "4e13caf9-bbc6-4a85-b9fb-2aa25f9a20fe", PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0  },
        };

        modelBuilder.Entity<Product>().HasData(productEntitySeeds);
        modelBuilder.Entity<AppRole>().HasData(roleEntitySeeds);
        modelBuilder.Entity<AppUser>().HasData(userEntitySeeds);

        base.OnModelCreating(modelBuilder);
    }
}
