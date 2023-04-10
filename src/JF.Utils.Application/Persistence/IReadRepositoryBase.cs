using System.Linq.Expressions;


namespace JF.Utils.Application.Persistence
{
    public interface IReadRepositoryBase<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(bool asNoTracking = true);

        IList<TEntity> GetAllPaged(int page, int pagesize);

        IQueryable<TEntity> GetAllBySpec(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true);

        Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default);

        TEntity? GetById(object id);
        Task<TEntity?> GetBySpecAsync<Spec>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        Task<ICollection<TEntity>> ListAsync(CancellationToken cancellationToken = default);

        Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        Task<int> CountAsync(CancellationToken cancellationToken = default);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
