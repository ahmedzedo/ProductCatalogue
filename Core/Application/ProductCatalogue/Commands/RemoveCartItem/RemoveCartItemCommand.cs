using ProductCatalogue.Application.Common.Exceptions;
using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Application.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        //public ICartItemRepository CartItemRepository { get; set; }
        //public IUnitOfWork UnitOfWork { get; set; }
        public IApplicationDbContext DbContext => (IApplicationDbContext)ServiceProvider.GetService(typeof(IApplicationDbContext));

        #endregion

        #region Constructor
        public RemoveCartItemCommandHandler(IServiceProvider serviceProvider/*, IUnitOfWork unitOfWork, ICartItemRepository cartItemRepository*/)
           : base(serviceProvider)
        {
            //UnitOfWork = unitOfWork;
            //CartItemRepository = cartItemRepository;
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

