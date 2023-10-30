
using JF.Utils.Application.Persistence;
using JF.Utils.Domain.Entities;

namespace JF.Utils.Infrastructure.Persistence
{
    public interface IReadRepository<TEntity>: IBaseReadRepository<TEntity> 
        where TEntity : class, IEntity
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
