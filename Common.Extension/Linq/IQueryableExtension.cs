using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extension.Linq
{
    public static class IQueryableExtension
    {
        public enum SortDirection
        {
            Ascending = 0,
            Descending = 1
        }
        public static (IEnumerable<T>, int totalCount) ToPagedList<T>(this IQueryable<T> query, int pageIndex, int pageSize) where T : class
        {
            int totalCount = query.Count();
            var result = query.Skip(pageIndex * pageSize).Take(pageSize).ToList();

            return (result, totalCount);
        }

        public static async Task<(IEnumerable<T>, int totalCount)> ToPagedListAsync<T>(this IQueryable<T> query, int pageIndex, int pageSize) where T : class
        {
            int totalCount = query.Count();
            var result = await Task.FromResult(query.Skip(pageIndex * pageSize).Take(pageSize).ToList());

            return (result, totalCount);
        }

        /// <summary>
        /// The WhereIf to assert from condition before applying filter
        /// </summary>
        /// <param name="IfCondition">the precondition to apply the filter or not</param>
        /// <param name="filter"></param>
        /// <returns></returns>

        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> query,
            bool IfCondition,
            Expression<Func<T, bool>> filter)
            where T : class
        {
            if (IfCondition == true && filter != null)
            {
                query = query.Where(filter);
            }
            return query;
        }


        public static IOrderedQueryable<T> Order<T>(this IQueryable<T> source, string propertyName, SortDirection descending = SortDirection.Ascending, bool anotherLevel = false)
        {
            var param = Expression.Parameter(typeof(T), string.Empty);
            var property = Expression.PropertyOrField(param, propertyName);
            var sort = Expression.Lambda(property, param);

            var call = Expression.Call(
                typeof(Queryable),
                (!anotherLevel ? "OrderBy" : "ThenBy") +
                (descending == SortDirection.Descending ? "Descending" : string.Empty),
                new[] { typeof(T), property.Type },
                source.Expression,
                Expression.Quote(sort));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }
    }
}
