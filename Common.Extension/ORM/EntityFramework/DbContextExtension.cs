using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extension.ORM.EntityFramework
{
    public static class DbContextExtension
    {
        public static IQueryable<object> GetDbSetByEntityName(this DbContext dbContext, string entityName)
        {
            Type entityType = dbContext.Model.GetEntityTypes()
                .Where(e => e.ClrType.Name == entityName)
                .FirstOrDefault()?
                .ClrType;

            return (dynamic)typeof(DbContextExtension)
                .GetMethod(nameof(GetDbset))
                .MakeGenericMethod(entityType)
                .Invoke(null, new object[] { dbContext });
        }

        public static IQueryable<T> GetDbset<T>(DbContext dbContext) where T : class
        {
            return dbContext.Set<T>().AsNoTracking().AsQueryable();
        }




    }
}
