using TesTDD.Domain.Core.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TesTDD.Domain.Core.Interfaces.Service
{
    public interface IServiceCrud<TKey, TEntity> where TKey : struct where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> AddAsync(TEntity obj);
        Task<TEntity> DeleteAsync(TKey id);
        Task<TEntity> GetAsync(TKey id);
        IEnumerable<TEntity> GetAll();
        Task<TEntity> UpdateAsync(TEntity obj);
    }
}
