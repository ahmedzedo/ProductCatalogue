using ProductCatalogue.Domain.Common;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.Common.Interfaces.Persistence
{
    public interface IReadOnlyRepository<T> where T : Entity
    {
        IDataQuery<T> GetQuery();
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
           }
}
