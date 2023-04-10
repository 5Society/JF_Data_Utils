

using JF.Utils.Application.Persistence;

namespace JF.Utils.Infrastructure.Persistence
{
    public interface IRepository<TEntity> : IReadRepository<TEntity>, IRepositoryBase<TEntity> 
        where TEntity : class
    {
    }
}
