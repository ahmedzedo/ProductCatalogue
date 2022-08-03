using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.ProductCatalogue.IRepositories
{
    public interface ICartRepository : IRepository<Cart>
    {
       // Task<Cart> CreateDefaultCart();
    }
}
