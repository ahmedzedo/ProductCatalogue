using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Application.Common.Messaging;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ProductCatalogue.Application.ProductCatalogue.IRepositories;
using System.Linq;

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

    #region Rquest Handler
    public class AddItemToCartCommandHandler : AbstractBaseRequestHandler<AddItemToCartCommand, Guid>
    {
        #region Dependencies
       // public ICartItemRepository CartItemRepository { get; set; }
        public ICartRepository CartRepository { get; set; }

        public IDataQuery<Cart> CartDataQurey => (IDataQuery<Cart>)ServiceProvider.GetService(typeof(IDataQuery<Cart>));
        public IUnitOfWork UnitOfWork { get; set; }
        #endregion

        #region Constructor
        public AddItemToCartCommandHandler(IServiceProvider serviceProvider,
                                           IUnitOfWork unitOfWork,
                                           //ICartItemRepository cartItemRepository,
                                           ICartRepository cartRepository)
           : base(serviceProvider)
        {
            UnitOfWork = unitOfWork;
           // CartItemRepository = cartItemRepository;
            CartRepository = cartRepository;
        }
        #endregion

        #region Request Handle
        public async override Task<Guid> HandleRequest(AddItemToCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await CartDataQurey.AsTracking().Include("Items").FirstOrDefaultAsync();
            
            if (cart == null)
            {
                cart = await CartRepository.CreateDefaultCart();
                await UnitOfWork.SaveAsync(cancellationToken);
            }

            var item =  cart.Items.Where(c=> c.ProductId == request.ProductId).FirstOrDefault();

            if (item == null)
            {
                item = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = request.ProductId,
                    Count = request.Count,
                };
               cart.Items.Add(item);
            }
            else
            {
                item.Count = request.Count;
                CartRepository.Update(cart);
            }
            await UnitOfWork.SaveAsync(cancellationToken);

            return item.Id;//Response.Success(item.Id);

        }
        #endregion
    }
    #endregion
}
