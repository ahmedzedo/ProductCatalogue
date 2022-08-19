using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Application.ProductCatalogue.Queries.GetPagedProducts;
using ProductCatalogue.Domain.Entities.ProductCatalogue;

namespace ProductCatalogue.Application.ProductCatalogue.IDataQueries
{
    public interface ICartDataQuery : IDataQuery<Cart>
    {
        IDataQuery<CartDetailsDto> GetCartDetailes();
    }
}
