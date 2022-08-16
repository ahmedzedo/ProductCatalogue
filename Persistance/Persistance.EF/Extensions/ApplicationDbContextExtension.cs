using Microsoft.EntityFrameworkCore;
using ProductCatalogue.Persistence.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Persistence.EF.Extensions
{
    public static class DbCatalogueDbContextExtension
    {
        public static bool ValidateEntities(this CatalogueDbContext context)
        {
            bool isValid = true;
            var entities = (from entry in context.ChangeTracker.Entries()
                            where entry.State == EntityState.Modified || entry.State == EntityState.Added
                            select entry.Entity);
            var validationResults = new List<ValidationResult>();
            foreach (var entity in entities)
            {
                if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
                {
                    isValid = false;
                    validationResults.ForEach(error =>
                    {
                        var errorMessage =
                              $"Entity: {entity.GetType().ToString()}\nProperty: {error.MemberNames}\n{error.ErrorMessage}";
                        var EntitiesException = new Exception(errorMessage);
                        EntitiesException.Data["PropertyName"] = error.MemberNames;
                        EntitiesException.Data["EntityName"] = entity.GetType().ToString();
                        // Logger.Error(exception, errorMessage);
                        throw EntitiesException;
                    });

                }
            }

            return isValid;

        }
    }
}
