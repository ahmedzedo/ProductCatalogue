using ProductCatalogue.Domain.Common;
using ProductCatalogue.Domain.Entities.ProductCatalogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogue.Domain.Entities.Users
{
    public class User:AuditableEntity
    {
        public User()
        {
           
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool? Otpactivate { get; set; }
        public bool IsDeleted { get; set; }
        public bool Gender { get; set; }

        public virtual Cart Cart { get; set; }


    }
}
