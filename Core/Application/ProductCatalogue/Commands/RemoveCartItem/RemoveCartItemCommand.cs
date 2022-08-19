using ProductCatalogue.Application.Common.Exceptions;
using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Application.Common.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.ProductCatalogue.Commands.RemoveCartItem
{
    #region Request
    public class RemoveCartItemCommand : BaseCommand<bool>
    {
        public Guid Id { get; set; }
    }
    #endregion

    #region Request Handler
    public class RemoveCartItemCommandHandler : BaseCommandHandler<RemoveCartItemCommand, bool>
    {
        #region Dependencies


        #endregion

        #region Constructor
        public RemoveCartItemCommandHandler(IServiceProvider serviceProvider, IApplicationDbContext dbContext)
           : base(serviceProvider, dbContext)
        {

        }
        #endregion

        #region Request Handle
        public async override Task<IResponse<bool>> HandleRequest(RemoveCartItemCommand request, CancellationToken cancellationToken)
        {
            var item = await DbContext.CartItemQuery.GetByIdAsync(request.Id);

            if (item == null)
            {
                throw new NotFoundException("this item not found", request.Id);
            }

            DbContext.CartItemQuery.Delete(item);

            int rowsCount = await DbContext.SaveChangesAsync(cancellationToken);

            return rowsCount > 0 ? Response.Success<bool>() : Response.Failuer<bool>("Unexpected error");
        }
        #endregion
    }
}
#endregion

