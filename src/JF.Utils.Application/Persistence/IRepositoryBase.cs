

namespace JF.Utils.Application.Persistence
{
    public interface IRepositoryBase<TEntity> : IReadRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity?> AddAsync(TEntity entity);
        Task<TEntity?> AddAndSaveAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);
        Task<int> AddRangeAndSaveAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);
        void Delete(TEntity entity);
        void Delete(object id);
        Task<bool> DeleteAndSaveAsync(object id, CancellationToken cancellationToken = default);
        void DeleteRange(ICollection<TEntity> entities);
        Task<int> DeleteRangeAndSaveAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);
        bool Update(object id, TEntity entity);
        Task<int> UpdateAndSaveAsync(object id, TEntity entity, CancellationToken cancellationToken = default);
        //Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        bool ValidateModel(TEntity entity);
    }
}
