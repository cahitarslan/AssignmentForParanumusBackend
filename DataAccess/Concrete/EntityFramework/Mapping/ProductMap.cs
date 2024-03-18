using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityFramework.Mapping;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).UseIdentityColumn();

        builder.Property(p => p.Name).IsRequired(true);
        builder.Property(p => p.Name).HasMaxLength(100);

        builder.Property(p => p.Description).IsRequired(false);
        builder.Property(p => p.Description).HasMaxLength(1000);

        builder.Property(p => p.Price).IsRequired(true);
    }
}
