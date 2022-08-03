using ProductCatalogue.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Common.Extension.Linq.IQueryableExtension;

namespace ProductCatalogue.Application.Common.Interfaces.Persistence
{
    public interface IDataQuery<T>
    {
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
         T Add(T entity);

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
         void Add(IEnumerable<T> entities);

        /// <summary>
        /// The add async.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
          Task<T> AddAsync(T entity);

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
         void AddAsync(IEnumerable<T> entities);

        /// <summary>
        /// Update by Specific Object 
        /// </summary>
        /// <param name="id">Key</param>
        /// <param name="t"> updated Object</param>
          void Update(object id, T entity);

        /// <summary>
        /// Updated
        /// </summary>
        /// <param name="entityToUpdate"> Updated Object</param>
          void Update(T updatedEntity);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
          bool Delete(T entity);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
          void Delete(IEnumerable<T> entities);

        /// <summary>
        /// The delete by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
          bool DeleteById(object id);


        #endregion

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T GetById(object id);

        /// <summary>
        /// The get by id async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<T> GetByIdAsync(object id);
       
        /// <summary>
        /// Track the eniities of the query
        /// </summary>
        /// <returns></returns>
        IDataQuery<T> AsTracking();

        /// <summary>
        /// The where.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The result.</returns>
        IDataQuery<T> Where(Expression<Func<T, bool>> filter);

        /// <summary>
        /// The WhereIf to assert from condition before applying filter
        /// </summary>
        /// <param name="IfCondition">the precondition to apply the filter or not</param>
        /// <param name="filter"></param>
        /// <returns></returns>
        IDataQuery<T> WhereIf(bool IfCondition, Expression<Func<T, bool>> filter);

        /// <summary>
        /// IncludeAll levels
        /// </summary>
        /// <param name="Include"></param>
        /// <returns></returns>
        IDataQuery<T> Include(string Include);

        /// <summary>
        /// The order by.
        /// </summary>
        /// <param name="keySelector">The key selector.</param>
        /// <returns>The result.</returns>
        IDataQuery<T> OrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        /// <summary>
        /// Order by using dynamic sort
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="sortDirection"></param>
        /// <param name="anotherLevel"></param>
        /// <returns></returns>
        IDataQuery<T> OrderBy(string propertyName, SortDirection sortDirection = SortDirection.Ascending, bool anotherLevel = false);
        /// <summary>
        /// The fist or default
        /// </summary>
        /// <returns></returns>
        T FirstOrDefault(Expression<Func<T, bool>> predicate = null);

        /// <summary>
        /// The fist or default async
        /// </summary>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null);
        /// <summary>
        /// AnyAsync
        /// </summary>
        /// <returns></returns>
        Task<bool> AnyAsync();

        /// <summary>
        /// AnyAsync
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Any
        /// </summary>
        /// <returns></returns>
        bool Any();

        /// <summary>
        /// The Top
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        List<T> Top(int count);

        /// <summary>
        /// The Top async
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<List<T>> TopAsync(int count);

        /// <summary>
        /// the GetLast get the last items 
        /// </summary>
        /// <param name="count">count of items</param>
        /// <returns></returns>
        List<T> Last(int count);

        /// <summary>
        ///  the last items 
        /// </summary>
        /// <param name="count">count of items</param>
        /// <returns></returns>
        Task<List<T>> LastAsync(int count);
        /// <summary>
        /// the Count
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// the CountAsync
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// Get Maximum value
        /// </summary>
        /// <returns></returns>
        T Max();

        /// <summary>
        /// MaxAsync
        /// </summary>
        /// <returns>Maximium value in operation</returns>
        Task<T> MaxAsync();

        /// <summary>
        /// Min value
        /// </summary>
        /// <returns></returns>
        T Min();

        /// <summary>
        /// Min value async 
        /// </summary>
        /// <returns></returns>
        Task<T> MinAsync();
        /// <summary>
        /// The to list.
        /// </summary>
        /// <returns>The result.</returns>
        List<T> ToList();

        /// <summary>
        /// AsEnumerable
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> AsEnumerable();

        /// <summary>
        /// The to list async.
        /// </summary>
        /// <returns>The result.</returns>
        Task<List<T>> ToListAsync();

        /// <summary>
        /// AsAsyncEnumerable
        /// </summary>
        /// <returns></returns>
        IAsyncEnumerable<T> AsAsyncEnumerable();
        /// <summary>
        /// The to pages list.
        /// </summary>
        /// <param name="pageIndex">The page index.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The result.</returns>
        (List<T>, int totalCount) ToPagedList(int pageIndex, int pageSize);

        /// <summary>
        /// The to pages list.
        /// </summary>
        /// <param name="pageIndex">The page index.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The result.</returns>
        Task<(List<T>, int totalCount)> ToPagedListAsync(int pageIndex, int pageSize);   
    }
}
