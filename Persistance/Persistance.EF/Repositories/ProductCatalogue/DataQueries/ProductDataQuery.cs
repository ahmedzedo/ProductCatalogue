using Microsoft.EntityFrameworkCore;
using ProductCatalogue.Application.ProductCatalogue.IDataQueries;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using ProductCatalogue.Persistence.EF.Repositories;

namespace Persistence.EF.Repositories.ProductCatalogue.DataQueries
{
    public class ProductDataQuery : DataQuery<Product>, IProductDataQuery
    {
        #region Constructor
        public ProductDataQuery(DbSet<Product> products) : base(products)
        {
        }
        #endregion

        public IProductDataQuery IncludeCartItems()
        {
            this.DbQuery = DbQuery.Include(p => p.CartItems);
            return this;
        }
    }
}
