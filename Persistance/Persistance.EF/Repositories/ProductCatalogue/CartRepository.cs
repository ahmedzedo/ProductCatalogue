using ProductCatalogue.Application.ProductCatalogue.IRepositories;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using ProductCatalogue.Domain.Entities.Users;
using ProductCatalogue.Persistence.EF;
using ProductCatalogue.Persistence.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.EF.Repositories.Packages
{
    public class CartRepository : Repository<Cart>,ICartRepository
    {
        #region Constructors
        public CartRepository(ApplicationDbContext context) : base(context)
        {
           
        }
        #endregion
        public async Task<Cart> CreateDefaultCart()
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
            await this.DbSet.AddAsync(cart);

            return cart;
        }
    }
}
