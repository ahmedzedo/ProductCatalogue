using Microsoft.EntityFrameworkCore;
using ProductCatalogue.Application.ProductCatalogue.IDataQueries;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using ProductCatalogue.Persistence.EF;
using ProductCatalogue.Persistence.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EF.Repositories.ProductCatalogue.DataQueries
{
    public class ProductDataQuery : DataQuery<Product>, IProductDataQuery
    {
        #region Constructor
        public ProductDataQuery(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        } 
        #endregion

        public IProductDataQuery IncludeCartItems()
        {
            this.DbQuery = DbQuery.Include(p => p.CartItems);
            return this;
        }
    }
}
