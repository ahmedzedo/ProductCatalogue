using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using Persistence.EF.Repositories.ProductCatalogue.DataQueries;
using ProductCatalogue.Application.Common.Interfaces.Account;
using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Application.ProductCatalogue.IDataQueries;
using ProductCatalogue.Domain.Common;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using ProductCatalogue.Infrastructure.Identity;
using ProductCatalogue.Persistence.EF.Repositories;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogue.Persistence.EF
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
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
        protected internal DbSet<Product> Products { get; set; }
        protected internal DbSet<CartItem> CartItems { get; set; }
        protected internal DbSet<Cart> Carts { get; set; }

        #endregion

        #region Data Queries
        public IProductDataQuery ProductQuery { get => new ProductDataQuery(this.Products); }
        public IDataQuery<CartItem> CartItemQuery { get => new DataQuery<CartItem>(this.CartItems); }
        public IDataQuery<Cart> CartQuery { get => new DataQuery<Cart>(this.Carts); }
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
            UpdateEntitiesShadowProperties();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateEntitiesShadowProperties()
        {
            base.ChangeTracker.Entries<AuditableEntity>()
                              .Where(e => e.State is EntityState.Added or EntityState.Modified)
                              .ToList()
                              .ForEach(entry => UpdateEntityShadowProperties(entry));
        }

        private void UpdateEntityShadowProperties(EntityEntry<AuditableEntity> entry)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    entry.Entity.CreatedOn = DateTime.Now;
                    const string IdProperty = "Id";

                    if (entry.Entity.GetType().GetProperty(IdProperty) != null)
                    {
                        SetNewGuidEntityId(IdProperty, entry);
                    }
                    break;
                case EntityState.Modified:
                    entry.Entity.LastUpdatedBy = _currentUserService.UserId;
                    entry.Entity.LastUpdatedOn = DateTime.Now;
                    break;
            }
        }

        private static void SetNewGuidEntityId(string IdProperty, EntityEntry<AuditableEntity> entry)
        {
            if (Guid.TryParse(entry.Property(IdProperty).CurrentValue.ToString(), out Guid id) && id == Guid.Empty)
            {
                entry.Property(IdProperty).CurrentValue = Guid.NewGuid();
            }
        }

        #endregion

    }
}
