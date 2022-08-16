using ProductCatalogue.Domain.Entities.ProductCatalogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.ProductCatalogue.Queries.GetPagedProducts
{
    public class GetPagedProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid UserId { get; set; }

        public static implicit operator GetPagedProductDto(Product product) 
        {
            return new()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price  
            };
        }
    }
}
