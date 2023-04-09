
using JF.Utils.Application.Persistence;

namespace JF.Utils.Infrastructure.Persistence
{
    public interface IReadRepository<TEntity>: IReadRepositoryBase<TEntity> 
        where TEntity : class
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
