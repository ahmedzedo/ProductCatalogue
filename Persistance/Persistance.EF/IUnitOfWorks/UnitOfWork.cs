using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.EF.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.EF.IUnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Constructors
        public UnitOfWork(ApplicationDbContext context)
        {
            this.Context = context;
        }
        #endregion

        #region Context
        private ApplicationDbContext Context { get; }
        #endregion

        #region Saving Methods
        public int Save(string userId = null)
        {
            //In all versions of Entity Framework, whenever you execute SaveChanges() to insert, update or delete on the 
            //database the framework will wrap that operation in a transaction. This transaction lasts only long enough to             
            //execute the operation and then completes. When you execute another such operation a new transaction is started.
            int rowsCount = 0;
            if (Context.ValidateEntities())
            {
                this.UpdatePropertiesBeforeSave(userId);
                rowsCount = this.Context.SaveChanges();
            }

            return rowsCount;
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken, string userId = null)
        {
            int rowsCount = 0;

            if (Context.ValidateEntities())
            {
                this.UpdatePropertiesBeforeSave(userId);
                rowsCount = await this.Context.SaveChangesAsync(cancellationToken);
            }

            return rowsCount;
        }

        #endregion

        #region Update Common Fields
        private void UpdatePropertiesBeforeSave(string userId = null)
        {
            //if (string.IsNullOrEmpty(userId)
            //       && HttpContext.Current != null
            //       && HttpContext.Current.User != null
            //       && HttpContext.Current.User.Identity.IsAuthenticated)
            //{
            //    userId = HttpContext.Current.User.Identity.Name;
            //}

            //const string CreatedOnProperty = "CreatedOn";
            //const string ModifiedOnPropery = "UpdatedOn";
            //// const string IsActiveProperty = "IsActive";
            const string IdProperty = "Id";

            //IEnumerable<EntityEntry> entitiesWithCreatedOn =
            //    this.Context.ChangeTracker.Entries()
            //        .Where(
            //            e => e.State == EntityState.Added && e.Entity.GetType().GetProperty(CreatedOnProperty) != null);
            //foreach (EntityEntry entry in entitiesWithCreatedOn)
            //{
            //    entry.Property(CreatedOnProperty).CurrentValue = DateTime.Now;
            //}

            

            IEnumerable<EntityEntry> entitiesWithId =
                this.Context.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added && e.Entity.GetType().GetProperty(IdProperty) != null);
            foreach (EntityEntry entry in entitiesWithId)
            {
                Guid id = Guid.Empty;
                if (Guid.TryParse(entry.Property(IdProperty).CurrentValue.ToString(), out id) && id == Guid.Empty)
                {
                    entry.Property(IdProperty).CurrentValue = Guid.NewGuid();
                }
            }

            //IEnumerable<EntityEntry> entitiesWithModifiedOn =
            //    this.Context.ChangeTracker.Entries()
            //        .Where(
            //            e => e.State == EntityState.Modified && e.Entity.GetType().GetProperty(ModifiedOnPropery) != null);
            //foreach (EntityEntry entry in entitiesWithModifiedOn)
            //{
            //    entry.Property(ModifiedOnPropery).CurrentValue = DateTime.Now;
            //}

            if (!string.IsNullOrEmpty(userId))
            {
                const string CreatedByPropery = "CreatedBy";
                const string ModifiedByPropery = "UpdatedBy";
                IEnumerable<EntityEntry> entitiesWithCreatedBy =
                     this.Context.ChangeTracker.Entries()
                        .Where(
                            e =>
                            e.State == EntityState.Added && e.Entity.GetType().GetProperty(CreatedByPropery) != null);
                foreach (EntityEntry entry in entitiesWithCreatedBy)
                {
                    entry.Property(CreatedByPropery).CurrentValue = userId;
                }

                IEnumerable<EntityEntry> entitiesWithModifiedBy =
                    this.Context.ChangeTracker.Entries()
                        .Where(
                            e =>
                            e.State == EntityState.Modified && e.Entity.GetType().GetProperty(ModifiedByPropery) != null);
                foreach (EntityEntry entry in entitiesWithModifiedBy)
                {
                    entry.Property(ModifiedByPropery).CurrentValue = userId;
                }
            }
        }
        #endregion

        #region Disposing

        private bool disposed = false;

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
