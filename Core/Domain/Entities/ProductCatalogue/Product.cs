using ProductCatalogue.Domain.Common;
using System;
using System.Collections.Generic;

namespace ProductCatalogue.Domain.Entities.ProductCatalogue
{
    public partial class Product : AuditableEntity
    {
        public Product()
        {
            //CartItems = new HashSet<CartItem>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public decimal Price { get; set; }
        //public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
