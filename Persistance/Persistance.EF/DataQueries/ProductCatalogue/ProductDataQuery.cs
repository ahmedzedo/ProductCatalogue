using Microsoft.EntityFrameworkCore;
using ProductCatalogue.Application.ProductCatalogue.IDataQueries;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using ProductCatalogue.Persistence.EF;

namespace Persistence.EF.DataQueries.ProductCatalogue
{
    public class ProductDataQuery : DataQuery<Product>, IProductDataQuery
    {
        #region Constructor
        public ProductDataQuery(CatalogueDbContext dbContext) : base(dbContext)
        {
        }
        #endregion

        public virtual IProductDataQuery IncludeCartItems(string userName)
        {

            DbQuery = DbQuery.Include(p => p.CartItems).ThenInclude(c => c.Cart);

            return this;
        }

    }
}
