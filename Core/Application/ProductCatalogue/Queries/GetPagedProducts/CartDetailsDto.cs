using System;

namespace ProductCatalogue.Application.ProductCatalogue.Queries.GetPagedProducts
{
    public class CartDetailsDto
    {
        public Guid CartId { get; set; }
        public Guid CartItemId { get; set; }
        public Guid CartUserId { get; set; }
        public int Count { get; set; }
    }
}
