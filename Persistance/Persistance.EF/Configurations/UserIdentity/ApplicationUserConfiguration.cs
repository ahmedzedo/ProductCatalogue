using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalogue.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EF.Configurations
{
  public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(m => m.NormalizedEmail).HasMaxLength(200);
            builder.Property(m => m.Id).HasMaxLength(200);
            builder.Property(m => m.NormalizedUserName).HasMaxLength(200);

        }
    }
}
