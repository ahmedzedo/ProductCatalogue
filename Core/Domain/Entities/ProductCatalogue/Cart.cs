using ProductCatalogue.Domain.Common;
using ProductCatalogue.Domain.Entities.Users;
using System;
using System.Collections.Generic;

namespace ProductCatalogue.Domain.Entities.ProductCatalogue
{
    public class Cart : AuditableEntity
    {
        public Cart()
        {
            Items = new HashSet<CartItem>();
        }
        public virtual Guid Id { get; set; }
        public Guid UserId { get; set; }

        public virtual ICollection<CartItem> Items { get; set; }
        public virtual User User { get; set; }
    }
}
