using ProductCatalogue.Application.Common.Interfaces.Persistence;
using ProductCatalogue.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogue.Persistence.EF.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public Repository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }
        #endregion

        #region Context

        /// <summary>
        /// Gets the context.
        /// </summary>
        protected internal ApplicationDbContext Context { get; }
        #endregion

        #region DbSet
        /// <summary>
        /// Gets the db set.
        /// </summary>
        protected internal DbSet<T> DbSet { get; }


        #endregion

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
            var addedEntity = DbSet.Add(entity);

            if (Context.Entry(addedEntity.Entity).State == EntityState.Added)
            {
                return addedEntity.Entity;
            }

            return null;
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
            var obj = DbSet.Find(id);
            Context.Entry(obj).CurrentValues.SetValues(entity);
            DbSet.Update(obj);
        }

        /// <summary>
        /// Updated
        /// </summary>
        /// <param name="entityToUpdate"> Updated Object</param>
        public virtual void Update(T updatedEntity)
        {
            //DbSet.Attach(entityToUpdate);
            //Context.Entry(entityToUpdate).State = EntityState.Modified;
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
            var deletedEntity = DbSet.Remove(entity);
            return Context.Entry(deletedEntity.Entity).State == EntityState.Deleted;
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

        #region Read Method
        public IDataQuery<T> GetQuery()
        {
            return new DataQuery<T>(DbSet.AsNoTracking().AsQueryable());
        }
        #endregion

        // #region Read Methods
        // /// <summary>
        // ///  The count.
        // /// </summary>
        // /// <returns>
        // ///The <see cref="long" />.
        // /// </returns>
        // public virtual long Count()
        // {
        //     return DbSet.AsNoTracking().Count();
        // }

        // /// <summary>
        // /// The count.
        // /// </summary>
        // /// <param name="filter">
        // /// The where expression.
        // /// </param>
        // /// <returns>
        // /// The <see cref="long"/>.
        // /// </returns>
        // public virtual long Count(Expression<Func<T, bool>> filter)
        // {
        //     return DbSet.Count(filter);
        // }

        // /// <summary>
        // ///     The count async.
        // /// </summary>
        // /// <returns>
        // ///     The <see cref="Task" />.
        // /// </returns>
        // public virtual async Task<int> CountAsync()
        // {
        //     return await DbSet.AsNoTracking().CountAsync();
        // }

        // /// <summary>
        // /// The count async.
        // /// </summary>
        // /// <param name="filter">
        // /// The where expression.
        // /// </param>
        // /// <returns>
        // /// The <see cref="Task"/>.
        // /// </returns>
        // public virtual async Task<int> CountAsync(Expression<Func<T, bool>> filter)
        // {
        //     return await DbSet.AsNoTracking().CountAsync(filter);
        // }

        // /// <summary>
        // /// The exists.
        // /// </summary>
        // /// <param name="entity">
        // /// The entity.
        // /// </param>
        // /// <returns>
        // /// The <see cref="bool"/>.
        // /// </returns>
        // public virtual bool Exists(T entity)
        // {
        //     return DbSet.AsNoTracking().Any(e => e == entity);
        // }

        // /// <summary>
        // /// The exists async.
        // /// </summary>
        // /// <param name="entity">
        // /// The entity.
        // /// </param>
        // /// <returns>
        // /// The <see cref="Task"/>.
        // /// </returns>
        // public virtual async Task<bool> ExistsAsync(T entity)
        // {
        //     return await DbSet.AnyAsync(e => e == entity);
        // }

        // public virtual bool Any(Expression<Func<T, bool>> filter)
        // {
        //     return DbSet.Any(filter);
        // }

        // public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        // {
        //     return await DbSet.AsNoTracking().AnyAsync(filter);
        // }

        // /// <summary>
        // /// The exists.
        // /// </summary>
        // /// <param name="id">
        // /// The id.
        // /// </param>
        // /// <returns>
        // /// The <see cref="bool"/>.
        // /// </returns>
        // public virtual bool Exists(object id)
        // {
        //     return DbSet.Find(id) != null;
        // }

        // /// <summary>
        // /// The exists async.
        // /// </summary>
        // /// <param name="id">
        // /// The id.
        // /// </param>
        // /// <returns>
        // /// The <see cref="Task"/>.
        // /// </returns>
        // public virtual async Task<bool> ExistsAsync(object id)
        // {
        //     return await DbSet.FindAsync(id) != null;
        // }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public virtual T GetById(object id)
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

        // /// <summary>
        // /// get first Or default with filter
        // /// </summary>
        // /// <param name="filter"></param>
        // /// <param name="orderBy"></param>
        // /// <returns></returns>
        // public virtual T GetFirst(Expression<Func<T, bool>> filter = null,
        //Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        // {
        //     IQueryable<T> query = DbSet.AsNoTracking();

        //     if (filter != null)
        //     {
        //         query = query.Where(filter);
        //     }

        //     if (orderBy != null)
        //     {
        //         query = orderBy(query);
        //     }

        //     return query.FirstOrDefault();
        // }

        // /// <summary>
        // /// get first Or default with filter async
        // /// </summary>
        // /// <param name="filter"> expression filter</param>
        // /// <param name="orderBy"> order by expression</param>
        // /// <returns></returns>
        // public virtual async Task<T> GetFirstAsync(Expression<Func<T, bool>> filter = null,
        //     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        // {
        //     IQueryable<T> query = DbSet.AsNoTracking();

        //     if (filter != null)
        //     {
        //         query = query.Where(filter);
        //     }

        //     if (orderBy != null)
        //     {
        //         query = orderBy(query);
        //     }

        //     return await query.FirstOrDefaultAsync();
        // }

        // /// <summary>
        // /// get Top
        // /// </summary>
        // /// <param name="length"></param>
        // /// <param name="filter"> expression filter</param>
        // /// <param name="orderBy"> oreder by expression</param>
        // /// <returns></returns>
        // public virtual IEnumerable<T> GetTop(int length, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        // {
        //     IQueryable<T> query = DbSet.AsNoTracking();

        //     if (filter != null)
        //     {
        //         query = query.Where(filter);
        //     }

        //     if (orderBy != null)
        //     {
        //         return orderBy(query).Take(length).ToList();
        //     }

        //     return query.Take(length).ToList();
        // }

        // /// <summary>
        // /// get Top async
        // /// </summary>
        // /// <param name="length"></param>
        // /// <param name="filter"> expression filter</param>
        // /// <param name="orderBy"> oreder by expression</param>
        // /// <returns></returns>
        // public async virtual Task<IEnumerable<T>> GetTopAsync(int length, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        // {
        //     IQueryable<T> query = DbSet.AsNoTracking();

        //     if (filter != null)
        //     {
        //         query = query.Where(filter);
        //     }

        //     if (orderBy != null)
        //     {
        //         return await orderBy(query).Take(length).ToListAsync();
        //     }

        //     return await query.Take(length).ToListAsync();
        // }

        // /// <summary>
        // /// Get
        // /// </summary>
        // /// <param name="filter"> expression filter</param>
        // /// <param name="orderBy"> oreder by expression</param>
        // /// <param name="includeProperties"> list of Includes properties</param>
        // /// <returns></returns>
        // public virtual IEnumerable<T> Get(
        // Expression<Func<T, bool>> filter = null,
        // Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        // params Expression<Func<T, object>>[] includeProperties)
        // {
        //     IQueryable<T> query = DbSet.AsNoTracking();

        //     if (filter != null)
        //     {
        //         query = query.Where(filter);
        //     }

        //     if (includeProperties != null && includeProperties.Any())
        //     {
        //         query = query.IncludeMultiple(includeProperties);
        //     }

        //     return orderBy != null ? orderBy(query).ToList() : query.ToList();
        // }

        // /// <summary>
        // /// Get Async
        // /// </summary>
        // /// <param name="filter"> expression filter</param>
        // /// <param name="orderBy"> oreder by expression</param>
        // /// <param name="includeProperties"> list of Includes properties</param>
        // /// <returns></returns>
        // public virtual Task<List<T>> GetAsync(
        // Expression<Func<T, bool>> filter = null,
        // Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        // params Expression<Func<T, object>>[] includeProperties)
        // {

        //     IQueryable<T> query = DbSet.AsNoTracking();

        //     if (filter != null)
        //     {
        //         query = query.Where(filter);
        //     }

        //     if (includeProperties != null && includeProperties.Any())
        //     {
        //         query = query.IncludeMultiple(includeProperties);
        //     }

        //     if (orderBy != null)
        //     {
        //         return orderBy(query).ToListAsync();
        //     }

        //     return query.ToListAsync();
        // }

        // /// <summary>
        // /// Get By filters
        // /// </summary>
        // /// <param name="filter">List of expression filter</param>
        // /// <param name="orderBy"> oreder by expression</param>
        // /// <param name="includeProperties"> list of Includes properties</param>
        // /// <returns></returns>
        // public virtual IEnumerable<T> GetByFilters(
        // IEnumerable<Expression<Func<T, bool>>> filters,
        // Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        // params Expression<Func<T, object>>[] includeProperties)
        // {
        //     IQueryable<T> query = DbSet.AsNoTracking();

        //     if (filters != null && filters.Count() > 0)
        //     {
        //         foreach (var filter in filters)
        //         {
        //             query = query.Where(filter);
        //         }
        //     }

        //     if (includeProperties != null && includeProperties.Any())
        //     {
        //         query = query.IncludeMultiple(includeProperties);
        //     }

        //     if (orderBy != null)
        //     {
        //         return orderBy(query).ToList();
        //     }

        //     return query.ToList();
        // }

        // /// <summary>
        // /// Get By filters async
        // /// </summary>
        // /// <param name="filter">List of expression filter</param>
        // /// <param name="orderBy"> oreder by expression</param>
        // /// <param name="includeProperties"> list of Includes properties</param>
        // public virtual async Task<IEnumerable<T>> GetByFiltersAsync(
        // IEnumerable<Expression<Func<T, bool>>> filters,
        // Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        // params Expression<Func<T, object>>[] includeProperties)
        // {
        //     IQueryable<T> query = DbSet.AsNoTracking();

        //     if (filters != null && filters.Count() > 0)
        //     {
        //         foreach (var filter in filters)
        //         {
        //             query = query.Where(filter);
        //         }
        //     }

        //     if (includeProperties != null && includeProperties.Any())
        //     {
        //         query = query.IncludeMultiple(includeProperties);
        //     }

        //     if (orderBy != null)
        //     {
        //         return await orderBy(query).ToListAsync();
        //     }

        //     return await query.ToListAsync();
        // }

        // /// <summary>
        // /// Get Paged result
        // /// </summary>
        // /// <param name="pageIndex"></param>
        // /// <param name="pageSize"></param>
        // /// <param name="totalCount"></param>
        // /// <param name="filter"> expression filter</param>
        // /// <param name="orderBy"> oreder by expression</param>
        // /// <param name="includeProperties"> list of Includes properties</param>
        // /// <returns></returns>
        // public virtual IEnumerable<T> GetPaged(int pageIndex, int pageSize, out int totalCount,
        // Expression<Func<T, bool>> filter = null,
        // Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        // params Expression<Func<T, object>>[] includeProperties)
        // {
        //     IQueryable<T> query = DbSet.AsNoTracking();

        //     if (filter != null)
        //     {
        //         query = query.Where(filter);
        //     }
        //     totalCount = query.Count();

        //     if (includeProperties != null && includeProperties.Any())
        //     {
        //         query = query.IncludeMultiple(includeProperties);
        //     }

        //     if (orderBy != null)
        //     {
        //         return orderBy(query).Skip(pageIndex * pageSize).Take(pageSize).ToList();
        //     }

        //     return query.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        // }

        // /// <summary>
        // /// Get paged reslut with option include, filter and order async
        // /// </summary>
        // /// <param name="pageIndex"></param>
        // /// <param name="pageSize"></param>
        // /// <param name="totalCount"></param>
        // /// <param name="filter">expression filter</param>
        // /// <param name="orderBy"> oreder by expression</param>
        // /// <param name="includeProperties"> list of Includes properties</param>
        // /// <returns></returns>
        // public virtual async Task<(IEnumerable<T> items, int totalCount)> GetPagedAsync(int pageIndex, int pageSize,
        // Expression<Func<T, bool>> filter = null,
        // Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        // params Expression<Func<T, object>>[] includeProperties)
        // {
        //     IQueryable<T> query = DbSet.AsNoTracking();

        //     if (filter != null)
        //     {
        //         query = query.Where(filter);
        //     }
        //     var totalCount = query.Count();

        //     if (includeProperties != null && includeProperties.Any())
        //     {
        //         query = query.IncludeMultiple(includeProperties);
        //     }

        //     if (orderBy != null)
        //     {
        //         return (await orderBy(query).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(), totalCount);
        //     }

        //     return (await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(), totalCount);
        // }

        // /// <summary>
        // /// GetPagedByFilters
        // /// </summary>
        // /// <param name="pageIndex"></param>
        // /// <param name="pageSize"></param>
        // /// <param name="totalCount"></param>
        // /// <param name="filter">List of expression filter</param>
        // /// <param name="orderBy"> oreder by expression</param>
        // /// <param name="includeProperties"> list of Includes properties</param>
        // /// <returns></returns>
        // public virtual IEnumerable<T> GetPagedByFilters(int pageIndex, int pageSize, out int totalCount,
        // IEnumerable<Expression<Func<T, bool>>> filters,
        // Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        // params Expression<Func<T, object>>[] includeProperties)
        // {
        //     IQueryable<T> query = DbSet.AsNoTracking();

        //     if (filters != null && filters.Count() > 0)
        //     {
        //         foreach (var filter in filters)
        //         {
        //             query = query.Where(filter);
        //         }
        //     }
        //     totalCount = query.Count();

        //     if (includeProperties != null && includeProperties.Any())
        //     {
        //         query = query.IncludeMultiple(includeProperties);
        //     }

        //     if (orderBy != null)
        //     {
        //         return orderBy(query).Skip(pageIndex * pageSize).Take(pageSize).ToList();
        //     }

        //     return query.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        // }

        // /// <summary>
        // /// GetPagedByFiltersAsync
        // /// </summary>
        // /// <param name="pageIndex"></param>
        // /// <param name="pageSize"></param>
        // /// <param name="filters">List of expression filter</param>
        // /// <param name="orderBy"> oreder by expression</param>
        // /// <param name="includeProperties"> list of Includes properties</param>
        // /// <returns></returns>
        // public virtual async Task<(IEnumerable<T> items, int totalCount)> GetPagedByFiltersAsync(int pageIndex, int pageSize,
        // IEnumerable<Expression<Func<T, bool>>> filters,
        // Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        // params Expression<Func<T, object>>[] includeProperties)
        // {
        //     IQueryable<T> query = DbSet.AsNoTracking();

        //     if (filters != null && filters.Count() > 0)
        //     {
        //         foreach (var filter in filters)
        //         {
        //             query = query.Where(filter);
        //         }
        //     }
        //     var totalCount = query.Count();

        //     if (includeProperties != null && includeProperties.Any())
        //     {
        //         query = query.IncludeMultiple(includeProperties);
        //     }

        //     if (orderBy != null)
        //     {
        //         return (await orderBy(query).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(), totalCount);
        //     }

        //     return (await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(), totalCount);
        // }

        // #endregion

        //public virtual int Count()
        //{
        //    int count = this.Query.Count();
        //    this.Query = null;
        //    return count;
        //}
    }

}

