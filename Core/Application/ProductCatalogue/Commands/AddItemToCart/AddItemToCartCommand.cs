using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Application.Common.Messaging;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using ProductCatalogue.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.ProductCatalogue.Commands.AddItemToCart
{
    #region Request
    public class AddItemToCartCommand : BaseCommand<Guid>
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
    #endregion

    #region Rquest Handler
    public class AddItemToCartCommandHandler : BaseCommandHandler<AddItemToCartCommand, Guid>
    {
        #region Dependencies

    
        #endregion

        #region Constructor
        public AddItemToCartCommandHandler(IServiceProvider serviceProvider,IApplicationDbContext dbContext)
           : base(serviceProvider, dbContext)
        {
          
        }
        #endregion

        #region Request Handle
        public async override Task<IResponse<Guid>> HandleRequest(AddItemToCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await DbContext.CartQuery.AsTracking().Include("Items").FirstOrDefaultAsync();

            if (cart == null)
            {
                cart = await CreateDefaultCart();
                await DbContext.SaveChangesAsync(cancellationToken);
            }

            var item = cart.Items.Where(c => c.ProductId == request.ProductId).FirstOrDefault();

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
                DbContext.CartQuery.Update(cart);
            }
            await DbContext.SaveChangesAsync(cancellationToken);

            return Response.Success(item.Id);

        }

        private async Task<Cart> CreateDefaultCart()
        {
            var cart = new Cart
            {
                Items = new List<CartItem>(),
                User = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    Email = "admin@localhost.com",
                    Gender = true,
                    IsDeleted = false
                }
            };
            await DbContext.CartQuery.AddAsync(cart);

            return cart;
        }
        #endregion
    }
    #endregion
}
