using ProductCatalogue.Domain.Common;
using ProductCatalogue.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.Domain.Entities.ProductCatalogue
{
    public class Cart : AuditableEntity, IEntity<Guid>
    {
        public Cart()
        {
            Items = new HashSet<CartItem>();
        }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public virtual ICollection<CartItem> Items { get; set; }
        public virtual User User { get; set; }
    }
}
