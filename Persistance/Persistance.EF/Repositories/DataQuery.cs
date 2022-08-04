using Microsoft.EntityFrameworkCore;
using ProductCatalogue.Application.Common.Interfaces.Persistence;
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
        public DataQuery(DbSet<T> dbSet)
        {
            this.DbSet = dbSet;
            this.DbQuery = DbSet.AsNoTracking().AsQueryable();
            //  ResetQuery();
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
            return this.DbSet.Find(id);
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
            return await this.DbSet.FindAsync(id);
        }

        /// <summary>
        /// Track the eniities of the query
        /// </summary>
        /// <returns></returns>
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

            return result;
        }

        /// <summary>
        /// The fist or default async
        /// </summary>
        /// <returns></returns>
        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null)
        {
            var result = predicate != null ? this.DbQuery.FirstOrDefault(predicate) : this.DbQuery.FirstOrDefault();

            return await Task.FromResult(result);
        }
        /// <summary>
        /// Any
        /// </summary>
        /// <returns></returns>
        public virtual bool Any()
        {
            var result = this.DbQuery.Any();

            return result;
        }

        /// <summary>
        /// AnyAsync
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync()
        {
            var result = await this.DbQuery.AnyAsync();

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

            return result;
        }

        /// <summary>
        /// the Count
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            var result = this.DbQuery.Count();
            //ResetQuery();

            return result;
        }

        /// <summary>
        /// the CountAsync
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> CountAsync()
        {
            var result = await Task.FromResult(this.DbQuery.Count());

            return result;
        }

        /// <summary>
        /// Get Maximum value
        /// </summary>
        /// <returns></returns>
        public virtual T Max()
        {
            var result = this.DbQuery.Max();

            return result;
        }

        /// <summary>
        /// MaxAsync
        /// </summary>
        /// <returns>Maximium value in operation</returns>
        public virtual async Task<T> MaxAsync()
        {
            var result = await Task.FromResult(this.DbQuery.Max());

            return result;
        }

        /// <summary>
        /// Min value
        /// </summary>
        /// <returns></returns>
        public virtual T Min()
        {
            var result = this.DbQuery.Min();

            return result;
        }

        /// <summary>
        /// Min value async 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<T> MinAsync()
        {
            var result = await Task.FromResult(this.DbQuery.Min());

            return result;
        }

        /// <summary>
        /// The to list.
        /// </summary>
        /// <returns>The result.</returns>
        public virtual List<T> ToList()
        {
            var result = this.DbQuery.ToList();

            return result;
        }

        /// <summary>
        /// AsEnumerable
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> AsEnumerable()
        {
            var result = this.DbQuery.AsEnumerable();

            return result;
        }

        /// <summary>
        /// The to list async.
        /// </summary>
        /// <returns>The result.</returns>
        public virtual async Task<List<T>> ToListAsync()
        {
            var result = await Task.FromResult(this.DbQuery.ToList());

            return result;
        }

        /// <summary>
        /// AsAsyncEnumerable
        /// </summary>
        /// <returns></returns>
        public virtual IAsyncEnumerable<T> AsAsyncEnumerable()
        {
            var result = this.DbQuery.AsAsyncEnumerable();

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
            var result = await this.DbQuery.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();

            return (result, totalCount);
        }
        #endregion
    }
}
