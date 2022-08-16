using ProductCatalogue.Application.ProductCatalogue.IDataQueries;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.Common.Interfaces.Persistence
{
    public interface IApplicationDbContext
    {
        public IProductDataQuery ProductQuery { get; }
        public IDataQuery<CartItem> CartItemQuery { get; }
        public IDataQuery<Cart> CartQuery { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
