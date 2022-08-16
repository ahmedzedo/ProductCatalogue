using ProductCatalogue.Application.Common.Exceptions;
using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Application.Common.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.ProductCatalogue.Commands.UpdateCartItem
{
    #region Request
    public class UpdateCartItemCommand : BaseCommand<bool>
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
    }
    #endregion

    #region Request Handler
    public class UpdateCartItemCommandHandler : BaseCommandHandler<UpdateCartItemCommand, bool>
    {
        #region Dependencies


        #endregion

        #region Constructor
        public UpdateCartItemCommandHandler(
            IServiceProvider serviceProvider,IApplicationDbContext dbContext)
           : base(serviceProvider, dbContext)
        {

        }
        #endregion

        #region RequestHandle
        public override async Task<IResponse<bool>> HandleRequest(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {
            var item = await DbContext.CartItemQuery.GetByIdAsync(request.Id);

            if (item == null)
            {
                throw new NotFoundException("this item not found", request.Id);

            }
            item.Count = request.Count;
            DbContext.CartItemQuery.Update(item);
            int rows = await DbContext.SaveChangesAsync(cancellationToken);

            return rows > 0 ? Response.Success(true) : Response.Failuer<bool>("Unexpected Expetion");

        }
        #endregion
    }
    #endregion
}
