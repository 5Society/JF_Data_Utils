

using JF.Utils.Application.Persistence;
using JF.Utils.Domain.Entities;

namespace JF.Utils.Infrastructure.Persistence
{
    public interface IRepository<TEntity> : IReadRepository<TEntity>, IBaseRepository<TEntity> 
        where TEntity : class, IEntity
    {
    }
}
