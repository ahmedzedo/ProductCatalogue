using Microsoft.EntityFrameworkCore;
using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Persistence.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Common.Extension.Linq.IQueryableExtension;

namespace Persistence.EF.DataQueries
{
    public class DataQuery<T> : IDataQuery<T> where T : class
    {
        #region Properties


        protected internal IQueryable<T> DbQuery { get; set; }
        protected internal DbSet<T> DbSet { get; set; }
        protected internal CatalogueDbContext DbContext { get; set; }
        #endregion

        #region Constructors
        public DataQuery(CatalogueDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
            DbQuery = DbSet.AsNoTracking().AsQueryable();
        }
        #endregion

        //protected void ResetQuery()
        //{
        //    this.DbQuery = DbSet.AsNoTracking().AsQueryable();
        //}

        #region Write Methods 
        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Add(T entity)
        {
            return DbSet.Add(entity).Entity;
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        public void Add(IEnumerable<T> entities)
        {
            DbSet.AddRange(entities);
        }

        /// <summary>
        /// The add async.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual async Task<T> AddAsync(T entity)
        {
            var addedEntity = await DbSet.AddAsync(entity);

            return addedEntity.Entity;
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        public void AddAsync(IEnumerable<T> entities)
        {
            DbSet.AddRangeAsync(entities);
        }

        /// <summary>
        /// Update by Specific Object 
        /// </summary>
        /// <param name="id">Key</param>
        /// <param name="t"> updated Object</param>
        public virtual void Update(object id, T entity)
        {
            _ = DbSet.Find(id);

            DbSet.Update(entity);
        }

        /// <summary>
        /// Updated
        /// </summary>
        /// <param name="entityToUpdate"> Updated Object</param>
        public virtual void Update(T updatedEntity)
        {
            //DbSet.Attach(entityToUpdate);
            //Context.Entry(entityToUpdate).State = EntityState.Modified;
            if (!DbSet.Contains(updatedEntity))
            {
                DbSet.Attach(updatedEntity);
            }

            DbSet.Update(updatedEntity);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool Delete(T entity)
        {
            return DbSet.Remove(entity).State == EntityState.Deleted;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        public virtual void Delete(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        /// <summary>
        /// The delete by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool DeleteById(object id)
        {
            var entity = DbSet.Find(id);

            return Delete(entity);
        }


        #endregion

        #region Read Methods

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T GetById(object id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// The get by id async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<T> GetByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        /// <summary>
        /// Track the eniities of the query
        /// </summary>
        /// <returns></returns>
        public virtual IDataQuery<T> AsTracking()
        {
            DbQuery = DbQuery.AsTracking();

            return this;
        }

        /// <summary>
        /// The where.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The result.</returns>
        public virtual IDataQuery<T> Where(Expression<Func<T, bool>> filter)
        {
            DbQuery = DbQuery.Where(filter);

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
                DbQuery = DbQuery.Where(filter);
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
                DbQuery = DbQuery.Include(include);
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
                DbQuery = orderBy(DbQuery).AsQueryable();
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
                DbQuery = DbQuery.Order(propertyName, sortDirection, anotherLevel);
            }

            return this;
        }

        /// <summary>
        /// The fist or default
        /// </summary>
        /// <returns></returns>
        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? DbQuery.FirstOrDefault() : DbQuery.FirstOrDefault(predicate);
        }

        /// <summary>
        /// The fist or default with project each element of sequence into a new form
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns>TResult</returns>
        public virtual TResult FirstOrDefault<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null)
        {
            var query = predicate == null ? DbQuery.Select(selector)
                                          : DbQuery.Where(predicate).Select(selector);

            return query.FirstOrDefault();
        }

        /// <summary>
        /// The fist or default async
        /// </summary>
        /// <returns></returns>
        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null)
        {
            return predicate != null ? await DbQuery.FirstOrDefaultAsync(predicate) : await DbQuery.FirstOrDefaultAsync();
        }

        /// <summary>
        /// The fist or default with project each element of sequence into a new form
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns>TResult</returns>
        public virtual async Task<TResult> FirstOrDefaultAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null)
        {
            var query = predicate != null ?
                 DbQuery.Where(predicate).Select(selector)
                : DbQuery.Select(selector);

            return await query.FirstOrDefaultAsync();
        }
        /// <summary>
        /// Any
        /// </summary>
        /// <returns></returns>
        public virtual bool Any()
        {
            return DbQuery.Any();
        }

        /// <summary>
        /// AnyAsync
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync()
        {
            return await DbQuery.AnyAsync();
        }

        /// <summary>
        /// AnyAsync
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await DbQuery.AnyAsync(expression);
        }

        /// <summary>
        /// The Top
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public virtual List<T> Top(int count)
        {
            return DbQuery.Take(count).ToList();
        }

        /// <summary>
        /// Get top result of sequence  
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="count"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual List<TResult> Top<TResult>(int count, Expression<Func<T, TResult>> selector)
        {
            return DbQuery.Select(selector).Take(count).ToList();
        }

        /// <summary>
        /// The Top async
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> TopAsync(int count)
        {
            return await DbQuery.Take(count).ToListAsync();
        }

        /// <summary>
        /// The Top async
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="count"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual async Task<List<TResult>> TopAsync<TResult>(int count, Expression<Func<T, TResult>> selector)
        {
            return await DbQuery.Select(selector).Take(count).ToListAsync();
        }

        /// <summary>
        /// the Last get the last items 
        /// </summary>
        /// <param name="count">count of items</param>
        /// <returns></returns>
        public virtual List<T> Last(int count)
        {
            return DbQuery.TakeLast(count).ToList();
        }

        /// <summary>
        /// The last  get the last elements
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="count"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual List<TResult> Last<TResult>(int count, Expression<Func<T, TResult>> selector)
        {
            return DbQuery.Select(selector).TakeLast(count).ToList();
        }

        /// <summary>
        ///  the last items 
        /// </summary>
        /// <param name="count">count of items</param>
        /// <returns></returns>
        public virtual async Task<List<T>> LastAsync(int count)
        {
            return await Task.FromResult(DbQuery.TakeLast(count).ToList());
        }

        public virtual async Task<List<TResult>> LastAsync<TResult>(int count, Expression<Func<T, TResult>> selector)
        {
            return await DbQuery.Select(selector).TakeLast(count).ToListAsync();
        }

        /// <summary>
        /// the Count
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            return DbQuery.Count();
        }

        /// <summary>
        /// the CountAsync
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> CountAsync()
        {
            return await Task.FromResult(DbQuery.Count());
        }

        /// <summary>
        /// Get Maximum value
        /// </summary>
        /// <returns></returns>
        public virtual T Max()
        {
            return DbQuery.Max();
        }

        /// <summary>
        /// MaxAsync
        /// </summary>
        /// <returns>Maximium value in operation</returns>
        public virtual async Task<T> MaxAsync()
        {
            return await DbQuery.MaxAsync();
        }

        /// <summary>
        /// Min value
        /// </summary>
        /// <returns></returns>
        public virtual T Min()
        {
            return DbQuery.Min();
        }

        /// <summary>
        /// Min value async 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<T> MinAsync()
        {
            return await Task.FromResult(DbQuery.Min());
        }

        /// <summary>
        /// The to list.
        /// </summary>
        /// <returns>The result.</returns>
        public virtual List<T> ToList()
        {
            return DbQuery.ToList();
        }

        /// <summary>
        /// The to list.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual List<TResult> ToList<TResult>(Expression<Func<T, TResult>> selector)
        {
            return DbQuery.Select(selector).ToList();
        }

        /// <summary>
        /// The to list async.
        /// </summary>
        /// <returns>The result.</returns>
        public virtual async Task<List<T>> ToListAsync()
        {
            return await DbQuery.ToListAsync();
        }

        // <summary>
        /// ToListAsync
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual async Task<List<TResult>> ToListAsync<TResult>(Expression<Func<T, TResult>> selector)
        {
            return await DbQuery.Select(selector).ToListAsync();
        }

        /// <summary>
        /// AsEnumerable
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> AsEnumerable()
        {
            return DbQuery.AsEnumerable();
        }

        /// <summary>
        /// AsEnumerable
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual IEnumerable<TResult> AsEnumerable<TResult>(Expression<Func<T, TResult>> selector)
        {
            return DbQuery.Select(selector).AsEnumerable();
        }

        /// <summary>
        /// AsAsyncEnumerable
        /// </summary>
        /// <returns></returns>
        public virtual IAsyncEnumerable<T> AsAsyncEnumerable()
        {
            return DbQuery.AsAsyncEnumerable();
        }

        /// <summary>
        /// AsAsyncEnumerable
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual IAsyncEnumerable<TResult> AsAsyncEnumerable<TResult>(Expression<Func<T, TResult>> selector)
        {
            return DbQuery.Select(selector).AsAsyncEnumerable();
        }

        /// <summary>
        /// The to pages list.
        /// </summary>
        /// <param name="pageIndex">The page index.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The result.</returns>
        public virtual (List<T>, int totalCount) ToPagedList(int pageIndex, int pageSize)
        {
            int totalCount = DbQuery.Count();
            var result = DbQuery.Skip(pageIndex * pageSize).Take(pageSize).ToList();

            return (result, totalCount);
        }

        /// <summary>
        /// The to pages list.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual (List<TResult>, int totalCount) ToPagedList<TResult>(int pageIndex, int pageSize, Expression<Func<T, TResult>> selector)
        {
            int totalCount = DbQuery.Count();
            var result = DbQuery.Select(selector).Skip(pageIndex * pageSize).Take(pageSize).ToList();

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
            int totalCount = DbQuery.Count();
            var result = await DbQuery.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();

            return (result, totalCount);
        }

        /// <summary>
        /// the ToPagedListAsync
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual async Task<(List<TResult>, int totalCount)> ToPagedListAsync<TResult>(int pageIndex, int pageSize, Expression<Func<T, TResult>> selector)
        {
            int totalCount = DbQuery.Count();
            var result = await DbQuery.Select(selector).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();

            return (result, totalCount);
        }
        #endregion
    }
}
