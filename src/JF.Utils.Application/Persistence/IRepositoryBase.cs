

namespace JF.Utils.Application.Persistence
{
    public interface IRepositoryBase<TEntity> : IReadRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity?> AddAsync(TEntity entity);
        Task<TEntity?> AddAndSaveAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);
        Task<int> AddRangeAndSaveAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);
        void Delete(TEntity entity);
        void Delete(int id);
        Task<bool> DeleteAndSaveAsync(int id, CancellationToken cancellationToken = default);
        void DeleteRange(ICollection<TEntity> entities);
        Task<int> DeleteRangeAndSaveAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);
        bool Update(int id, TEntity entity);
        Task<int> UpdateAndSaveAsync(int id, TEntity entity, CancellationToken cancellationToken = default);
        //Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        bool ValidateModel(TEntity entity);
    }
}
