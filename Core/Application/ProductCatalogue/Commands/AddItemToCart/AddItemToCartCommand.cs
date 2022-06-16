using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Application.Common.Messaging;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ProductCatalogue.Application.ProductCatalogue.IRepositories;

namespace ProductCatalogue.Application.ProductCatalogue.Commands.AddItemToCart
{
    #region Request
    public class AddItemToCartCommand : BaseRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
    #endregion

    #region Request Handler
    public class AddItemToCartCommandHandler : AbstractBaseRequestHandler<AddItemToCartCommand, Guid>
    {
        #region Dependencies
        public ICartItemRepository CartItemRepository { get; set; }
        public ICartRepository CartRepository { get; set; }
        #endregion

        #region Constructor
        public AddItemToCartCommandHandler(IServiceProvider serviceProvider,
                                           IUnitOfWork unitOfWork,
                                           ICartItemRepository cartItemRepository,
                                           ICartRepository cartRepository)
           : base(serviceProvider, unitOfWork)
        {
            CartItemRepository = cartItemRepository;
            CartRepository = cartRepository;
        }
        #endregion

        #region Request Handle
        public async override Task<Guid> HandleRequest(AddItemToCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await CartRepository.GetQuery().FirstOrDefaultAsync();
            
            if (cart == null)
            {
                cart = await CartRepository.CreateDefaultCart();
                await UnitOfWork.SaveAsync(cancellationToken);
            }
            
            var item = await CartItemRepository
                .GetQuery()
                .FirstOrDefaultAsync(i => i.ProductId == request.ProductId);

            if (item == null)
            {
                item = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = request.ProductId,
                    Count = request.Count,
                };
                CartItemRepository.Add(item);
            }
            else
            {
                item.Count = request.Count;
                CartItemRepository.Update(item);
            }
            await UnitOfWork.SaveAsync(cancellationToken);

            return item.Id;//Response.Success(item.Id);

        }
        #endregion
    }
    #endregion
}
