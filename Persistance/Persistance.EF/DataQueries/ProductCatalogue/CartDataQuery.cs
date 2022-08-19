using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Application.ProductCatalogue.IDataQueries;
using ProductCatalogue.Application.ProductCatalogue.Queries.GetPagedProducts;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using ProductCatalogue.Persistence.EF;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.EF.DataQueries.ProductCatalogue
{
    public class CartDataQuery : DataQuery<Cart>, ICartDataQuery
    {
        public CartDataQuery(CatalogueDbContext DbContext) : base(DbContext)
        {

        }
        public IDataQuery<CartDetailsDto> GetCartDetailes()
        {
            var query = from c in Context.Carts
                        join ci in Context.CartItems
                        on c.Id equals ci.CartId
                        select new CartDetailsDto
                        {
                            CartId = c.Id,
                            CartItemId = ci.Id,
                            CartUserId = c.UserId,
                            Count = ci.Count,
                        };
            return new DataQuery<CartDetailsDto>(query);
            //Context.Carts.Join(Context.CartItems,
            //cart => cart.Id,
            //cartItem => cartItem.CartId,
            //(cart, cartItem) => new CartDetailsDto
            //{
            //    CartId = cart.Id,
            //    CartItemId = cartItem.Id,
            //    CartUserId = cart.UserId,
            //    Count = cartItem.Count,
            //}));
        }
    }
}
