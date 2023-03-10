using System.Threading;
using System.Threading.Tasks;

namespace TesTDD.Domain.Core.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}