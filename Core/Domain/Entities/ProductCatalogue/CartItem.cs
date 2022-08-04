using ProductCatalogue.Domain.Common;
using System;

namespace ProductCatalogue.Domain.Entities.ProductCatalogue
{
    public class CartItem : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CartId { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public virtual Cart Cart { get; set; }

    }
}
