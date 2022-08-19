using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Application.Common.Messaging;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.ProductCatalogue.Queries.GetCart
{
    #region Request
    public class GetCartQuery : BaseQuery<Cart>
    {
        public Guid Id { get; set; }
        public List<CartItem> Items { get; set; }
    }
    #endregion

    #region Request Handler
    public class GetCartQueryHandler : BaseQueryHandler<GetCartQuery, Cart>
    {
        #region Dependencies


        #endregion

        #region Constructor
        public GetCartQueryHandler(IServiceProvider serviceProvider, IApplicationDbContext dbContext)
           : base(serviceProvider, dbContext)
        {
        }
        #endregion

        #region Handel
        public async override Task<IResponse<Cart>> HandleRequest(GetCartQuery request, CancellationToken cancellationToken)
        {
            string includeEntities = $"Items.Product.{nameof(Domain.Entities.ProductCatalogue.Product)}";

            var cart = await DbContext.CartQuery.Include("Items")
                                          .Include("Items.Product")
                                          .FirstOrDefaultAsync();

            return cart == null ? Response.Failuer<Cart>("No item in cart") : Response.Success(cart);
        }
        #endregion
    }
    #endregion
}
