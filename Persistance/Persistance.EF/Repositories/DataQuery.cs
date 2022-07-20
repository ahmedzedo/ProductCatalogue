using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Common.Extension.Linq.IQueryableExtension;

namespace ProductCatalogue.Persistence.EF.Repositories
{
    public class DataQuery<T> : IDataQuery<T> where T : class
    {
        #region Properties


        protected internal IQueryable<T> DbQuery { get; set; }
        protected internal DbSet<T> DbSet { get; set; }
        #endregion

        #region Constructors
        public DataQuery(ApplicationDbContext applicationDbContext)
        {
            this.DbSet = applicationDbContext.Set<T>();
            this.DbQuery = DbSet.AsNoTracking().AsQueryable();
        }
        #endregion

        #region IDataQuery<T> Implementation
        public virtual IDataQuery<T> AsTracking()
        {
            this.DbQuery = this.DbQuery.AsTracking();
            return this;
        }                 
        /// <summary>
        /// The where.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The result.</returns>
        public virtual IDataQuery<T> Where(Expression<Func<T, bool>> filter)
        {
            if (filter != null)
            {
                this.DbQuery = this.DbQuery.Where(filter);
                //var x = this.DbQuery.ToQueryString();
            }
            return this;
        }

        
        /// <summary>
        /// The WhereIf to assert from condition before applying filter
        /// </summary>
        /// <param name="IfCondition">the precondition to apply the filter or not</param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual IDataQuery<T> WhereIf(bool IfCondition, Expression<Func<T, bool>> filter)
        {
            if (IfCondition == true && filter != null)
            {
                this.DbQuery = this.DbQuery.Where(filter);
            }
            return this;
        }

        /// <summary>
        /// Include string
        /// </summary>
        /// <param name="include"></param>
        /// <returns></returns>
        public virtual IDataQuery<T> Include(string include)
        {
            if (!string.IsNullOrEmpty(include))
            {
                this.DbQuery = this.DbQuery.Include(include);
            }

            return this;
        }

        /// <summary>
        /// The order by.
        /// </summary>
        /// <param name="keySelector">The key selector.</param>
        /// <returns>The result.</returns>
        public virtual IDataQuery<T> OrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            if (orderBy != null)
            {
                this.DbQuery = orderBy(this.DbQuery).AsQueryable();
            }

            return this;
        }

        /// <summary>
        /// Dynamic Order
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="sortDirection"></param>
        /// <param name="anotherLevel"></param>
        /// <returns></returns>
        public virtual IDataQuery<T> OrderBy(string propertyName, SortDirection sortDirection = SortDirection.Ascending, bool anotherLevel = false)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                this.DbQuery = this.DbQuery.Order(propertyName, sortDirection, anotherLevel);
            }

            return this;
        }

        /// <summary>
        /// The fist or default
        /// </summary>
        /// <returns></returns>
        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate = null)
        {
            var result = predicate != null ? this.DbQuery.FirstOrDefault(predicate) : this.DbQuery.FirstOrDefault();
            this.DbQuery = default;

            return result;
        }

        /// <summary>
        /// The fist or default async
        /// </summary>
        /// <returns></returns>
        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null)
        {
            var result = predicate != null ? this.DbQuery.FirstOrDefault(predicate) : this.DbQuery.FirstOrDefault();
            this.DbQuery = default;

            return await Task.FromResult(result);
        }
        /// <summary>
        /// Any
        /// </summary>
        /// <returns></returns>
        public virtual bool Any()
        {
            var result = this.DbQuery.Any();
            this.DbQuery = default;

            return result;
        }

        /// <summary>
        /// AnyAsync
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync()
        {
            var result = await this.DbQuery.AnyAsync();
            this.DbQuery = default;

            return result;
        }

        /// <summary>
        /// AnyAsync
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            var result = await this.DbQuery.AnyAsync(expression);
            this.DbQuery = default;

            return result;
        }

        /// <summary>
        /// The Top
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public virtual List<T> Top(int count)
        {
            var result = this.DbQuery.Take(count).ToList();
            DbQuery = null;

            return result;
        }

        /// <summary>
        /// The Top async
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> TopAsync(int count)
        {
            var result = await Task.FromResult(this.DbQuery.Take(count).ToList());
            DbQuery = null;

            return result;
        }

        /// <summary>
        /// the GetLast get the last items 
        /// </summary>
        /// <param name="count">count of items</param>
        /// <returns></returns>
        public virtual List<T> Last(int count)
        {
            var result = this.DbQuery.TakeLast(count).ToList();
            this.DbQuery = default;

            return result;
        }
        /// <summary>
        ///  the last items 
        /// </summary>
        /// <param name="count">count of items</param>
        /// <returns></returns>
        public virtual async Task<List<T>> LastAsync(int count)
        {
            var result = await Task.FromResult(this.DbQuery.TakeLast(count).ToList());
            this.DbQuery = default;

            return result;
        }

        /// <summary>
        /// the Count
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            var result = this.DbQuery.Count();
            this.DbQuery = default;

            return result;
        }

        /// <summary>
        /// the CountAsync
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> CountAsync()
        {
            var result = await Task.FromResult(this.DbQuery.Count());
            this.DbQuery = default;

            return result;
        }

        /// <summary>
        /// Get Maximum value
        /// </summary>
        /// <returns></returns>
        public virtual T Max()
        {
            var result = this.DbQuery.Max();
            this.DbQuery = default;

            return result;
        }

        /// <summary>
        /// MaxAsync
        /// </summary>
        /// <returns>Maximium value in operation</returns>
        public virtual async Task<T> MaxAsync()
        {
            var result = await Task.FromResult(this.DbQuery.Max());
            this.DbQuery = default;

            return result;
        }

        /// <summary>
        /// Min value
        /// </summary>
        /// <returns></returns>
        public virtual T Min()
        {
            var result = this.DbQuery.Min();
            this.DbQuery = default;

            return result;
        }

        /// <summary>
        /// Min value async 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<T> MinAsync()
        {
            var result = await Task.FromResult(this.DbQuery.Min());
            this.DbQuery = default;

            return result;
        }

        /// <summary>
        /// The to list.
        /// </summary>
        /// <returns>The result.</returns>
        public virtual List<T> ToList()
        {
            var result = this.DbQuery.ToList();
            DbQuery = null;

            return result;
        }

        /// <summary>
        /// AsEnumerable
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> AsEnumerable()
        {
            var result = this.DbQuery.AsEnumerable();
            DbQuery = null;

            return result;
        }

        /// <summary>
        /// The to list async.
        /// </summary>
        /// <returns>The result.</returns>
        public virtual async Task<List<T>> ToListAsync()
        {
            var result = await Task.FromResult(this.DbQuery.ToList());
            DbQuery = null;

            return result;
        }

        /// <summary>
        /// AsAsyncEnumerable
        /// </summary>
        /// <returns></returns>
        public virtual IAsyncEnumerable<T> AsAsyncEnumerable()
        {
            var result = this.DbQuery.AsAsyncEnumerable();
            DbQuery = null;

            return result;
        }
        /// <summary>
        /// The to pages list.
        /// </summary>
        /// <param name="pageIndex">The page index.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The result.</returns>
        public virtual (List<T>, int totalCount) ToPagedList(int pageIndex, int pageSize)
        {
            int totalCount = this.DbQuery.Count();
            var result = this.DbQuery.Skip(pageIndex * pageSize).Take(pageSize).ToList();

            DbQuery = null;

            return (result, totalCount);
        }

        /// <summary>
        /// The to pages list.
        /// </summary>
        /// <param name="pageIndex">The page index.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The result.</returns>
        public virtual async Task<(List<T>, int totalCount)> ToPagedListAsync(int pageIndex, int pageSize)
        {
            int totalCount = this.DbQuery.Count();
            var result = await Task.FromResult(this.DbQuery.Skip(pageIndex * pageSize).Take(pageSize).ToList());

            DbQuery = null;

            return (result, totalCount);
        }
        #endregion
    }
}
