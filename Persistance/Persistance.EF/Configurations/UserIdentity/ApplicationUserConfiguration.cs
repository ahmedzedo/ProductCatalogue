using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalogue.Infrastructure.Identity;

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
