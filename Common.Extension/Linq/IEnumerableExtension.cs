using System;
using System.Collections.Generic;
using System.Linq;

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
        #region distinct By
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new();

            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        #endregion

    }


}

