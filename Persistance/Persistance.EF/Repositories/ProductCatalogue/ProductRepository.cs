using ProductCatalogue.Application.ProductCatalogue.IRepositories;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using ProductCatalogue.Persistence.EF;
using ProductCatalogue.Persistence.EF.Repositories;

namespace Persistence.EF.Repositories.Packages
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        #region Constructors
        public ProductRepository(ApplicationDbContext context) : base(context)
        {

        }
        #endregion
    }
}
