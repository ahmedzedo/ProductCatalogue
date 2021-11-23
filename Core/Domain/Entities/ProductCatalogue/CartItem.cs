using ProductCatalogue.Domain.Common;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
