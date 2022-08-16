using Microsoft.Extensions.DependencyInjection;
using Persistence.EF.DataQueries;
using Persistence.EF.DataQueries.ProductCatalogue;
using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Application.ProductCatalogue.IDataQueries;
using ProductCatalogue.Domain.Common;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using ProductCatalogue.Persistence.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.EF
{
    public class ApplicationDbContext : IApplicationDbContext
    {
        public CatalogueDbContext  DbContext{ get; set; }
        public IServiceProvider ServiceProvider { get; }

        #region Constructor
        public ApplicationDbContext(CatalogueDbContext dbContext, IServiceProvider serviceProvider)
        {
            DbContext = dbContext;
            ServiceProvider = serviceProvider;
        }
        #endregion

        #region Data Queries
        public IProductDataQuery ProductQuery { get => ServiceProvider.GetService<IProductDataQuery>(); }
        public IDataQuery<CartItem> CartItemQuery { get => ServiceProvider.GetService<IDataQuery<CartItem>>(); }
        public IDataQuery<Cart> CartQuery { get =>  ServiceProvider.GetService<IDataQuery<Cart>>(); }
        #endregion

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
           return DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
