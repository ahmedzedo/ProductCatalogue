using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Application.Common.Messaging;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ProductCatalogue.Application.ProductCatalogue.IRepositories;
using ProductCatalogue.Application.Common.Exceptions;

namespace ProductCatalogue.Application.ProductCatalogue.Commands.UpdateCartItem
{
    #region Request
    public class UpdateCartItemCommand : Request<bool>
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
    }
    #endregion

    #region Request Handler
    public class UpdateCartItemCommandHandler : BaseRequestHandler<UpdateCartItemCommand, bool>
    {
        #region Dependencies
        public ICartItemRepository CartItemRepository { get; set; }
        #endregion

        #region Constructor
        public UpdateCartItemCommandHandler(
            IServiceProvider serviceProvider,
            IUnitOfWork unitOfWork,
            ICartItemRepository cartItemRepository)
           : base(serviceProvider, unitOfWork)
        {
            CartItemRepository = cartItemRepository;
        }
        #endregion

        #region RequestHandle
        public override async Task<Response<bool>> HandleRequest(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {
            var item = await CartItemRepository.GetByIdAsync(request.Id);

            if (item == null)
            {
                throw new NotFoundException("this item not found", request.Id);

            }
            item.Count = request.Count;
            CartItemRepository.Update(item);
            int rows = await UnitOfWork.SaveAsync(cancellationToken);

            return rows > 0 ? Response.Success(true) : Response.Failuer<bool>("Unexpected Expetion");

        }
        #endregion
    }
    #endregion
}
