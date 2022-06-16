using ProductCatalogue.Application.Common.Exceptions;
using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Application.Common.Messaging;
using ProductCatalogue.Application.ProductCatalogue.IRepositories;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.ProductCatalogue.Queries.GetCart
{
    public class GetCartQuery : Request<Cart>
    {
        public Guid Id { get; set; }
        public List<CartItem> Items { get; set; }
    }
    public class GetCartQueryHandler : BaseRequestHandler<GetCartQuery, Cart>
    {
        #region Dependencies
        private ICartRepository CartRepository { get; set; }
        #endregion

        #region Constructor
        public GetCartQueryHandler(IServiceProvider serviceProvider, IUnitOfWork unitOfWork, ICartRepository cartRepository)
           : base(serviceProvider, unitOfWork)
        {
            CartRepository = cartRepository;
        }
        #endregion

        #region Handel
        public async override Task<IResponse<Cart>> HandleRequest(GetCartQuery request, CancellationToken cancellationToken)
        {
            string includeEntities = $"Items.Product.{nameof(Domain.Entities.ProductCatalogue.Product)}";

            var cart = await CartRepository.GetQuery().Include("Items").Include("Items.Product").FirstOrDefaultAsync();

            if (cart == null)
            {
                await CartRepository.CreateDefaultCart();
                await UnitOfWork.SaveAsync(cancellationToken);
            }
            return Response.Success(cart);
        }
        #endregion
    }
}
