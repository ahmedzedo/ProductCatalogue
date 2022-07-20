using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.ProductCatalogue.IDataQueries
{
    public interface IProductDataQuery : IDataQuery<Product>
    {
        IProductDataQuery IncludeCartItems();
    }
}
