using Microsoft.EntityFrameworkCore;
using TesTDD.Domain.Core.Entity;
using System.Linq;

namespace TesTDD.Infra.Repository
{
    public class GenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly RepositoryContext _Context;
        protected readonly DbSet<TEntity> _DbSet;
        protected readonly IQueryable<TEntity> _QueryableReadOnly;

        public GenericRepository(RepositoryContext context)
        {
            _Context = context;
            _DbSet = _Context.Set<TEntity>();
            _QueryableReadOnly = _DbSet.AsNoTracking();
        }
    }
}