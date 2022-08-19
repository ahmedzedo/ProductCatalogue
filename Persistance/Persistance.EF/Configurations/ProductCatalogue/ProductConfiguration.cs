using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalogue.Domain.Entities.ProductCatalogue;

namespace Persistence.EF.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(t => t.Id).HasDefaultValueSql("NEWID()");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.Price).HasColumnType("decimal").HasPrecision(18, 2);

        }
    }
}
