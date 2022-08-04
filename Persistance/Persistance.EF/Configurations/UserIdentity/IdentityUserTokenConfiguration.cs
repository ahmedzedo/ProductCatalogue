using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EF.Configurations
{
    public class IdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
        {
            builder.Property(m => m.UserId).HasMaxLength(200);
            builder.Property(m => m.LoginProvider).HasMaxLength(200);
            builder.Property(m => m.Name).HasMaxLength(200);
        }
    }
}
