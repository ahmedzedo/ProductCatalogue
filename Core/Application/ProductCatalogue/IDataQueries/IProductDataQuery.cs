using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using System;

namespace ProductCatalogue.Application.ProductCatalogue.IDataQueries
{
    public interface IProductDataQuery : IDataQuery<Product>
    {
        IProductDataQuery IncludeCartItems(string userName);
        
    }
}
