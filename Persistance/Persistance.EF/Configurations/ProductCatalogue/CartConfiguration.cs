using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalogue.Domain.Entities.ProductCatalogue;

namespace Persistence.EF.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(e => e.Id);
            //builder.HasOne(t => t.User).
            //    WithOne(p => p.Cart);




        }
    }
}
