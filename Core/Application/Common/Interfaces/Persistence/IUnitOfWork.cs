using System.Threading;
using System.Threading.Tasks;

namespace ProductCatalogue.Application.Common.Interfaces.Persistence
{
    public interface IUnitOfWork
    {
        int Save(string userId = null);
        Task<int> SaveAsync(CancellationToken cancellationToken, string userId = null);
    }
}
