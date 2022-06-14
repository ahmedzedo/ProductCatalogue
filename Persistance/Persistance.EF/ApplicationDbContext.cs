using ProductCatalogue.Application.Common.Interfaces;
using ProductCatalogue.Application.Common.Interfaces.Account;
using ProductCatalogue.Domain.Common;
using ProductCatalogue.Infrastructure.Identity;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ProductCatalogue.Domain.Entities.ProductCatalogue;

namespace ProductCatalogue.Persistence.EF
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        #region Properties
        private readonly ICurrentUserService _currentUserService;
        #endregion

        #region Constructor
        public ApplicationDbContext(
           DbContextOptions options,
           IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService
           ) : base(options, operationalStoreOptions)
        {
            _currentUserService = currentUserService;
        }
        #endregion

        #region DB Sets
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        #endregion

        #region On Model Creating
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
        #endregion

        #region Save Changes
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.CreatedOn = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastUpdatedBy = _currentUserService.UserId;
                        entry.Entity.LastUpdatedOn = DateTime.Now;
                        break;
                }
            }


            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        #endregion

    }
}
