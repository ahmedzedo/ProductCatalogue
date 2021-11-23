using System;

namespace ProductCatalogue.Domain.Common
{
    public abstract class AuditableEntity : Entity
    {
        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastUpdatedOn { get; set; }

        public string LastUpdatedBy { get; set; }

    }
}
