using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Domain.Entities.ProductCatalogue;

namespace ProductCatalogue.Application.ProductCatalogue.IRepositories
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
    }
}
