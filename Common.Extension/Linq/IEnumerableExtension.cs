using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extension.Linq
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<T> WhereIf<T>(
            this IEnumerable<T> query,
            bool IfCondition,
           Func<T, bool> filter)
            where T : class
        {
            if (IfCondition == true && filter != null)
            {
                query = query.Where(filter);
            }
            return query;
        }
    }
}

