using ProductCatalogue.Application.ProductCatalogue.IRepositories;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using ProductCatalogue.Persistence.EF;
using ProductCatalogue.Persistence.EF.Repositories;

namespace Persistence.EF.Repositories.Packages
{
    public class CartItemRepository : Repository<CartItem>,ICartItemRepository
    {
        #region Constructors
        public CartItemRepository(ApplicationDbContext context) : base(context)
        {

        }
        #endregion
    }
}
