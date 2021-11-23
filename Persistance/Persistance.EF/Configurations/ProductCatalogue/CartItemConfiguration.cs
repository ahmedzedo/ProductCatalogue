using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EF.Configurations
{
  public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasOne(t => t.Cart)
                .WithMany(p => p.Items)
                .HasForeignKey(d => d.CartId);
            builder.HasOne(t => t.Product);
            
        }
    }
}
