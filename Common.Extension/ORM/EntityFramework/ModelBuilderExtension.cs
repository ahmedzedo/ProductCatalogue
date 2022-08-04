using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Common.Extension.ORM.EntityFramework
{
    public static class ModelBuilderExtension
    {
        public static void ApplyGlobalFilter<T>(this ModelBuilder modelBuilder, string propertyName, T value)
        {
            modelBuilder.Model.GetEntityTypes()
                .Where(x => x.FindProperty(propertyName) != null)
                .Select(x => x.ClrType)
                .ToList()
                .ForEach(entityType =>
                {
                    var newParam = Expression.Parameter(entityType);
                    var filter = Expression.Lambda(Expression.Equal(Expression.Convert(Expression.Property(newParam, propertyName),
                                                                                       typeof(T)), Expression.Constant(value)), newParam);
                    modelBuilder.Entity(entityType).HasQueryFilter(filter);
                });
        }
    }
}
