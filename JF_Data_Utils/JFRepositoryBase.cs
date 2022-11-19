using JF.Utils.Data.Interfaces;

namespace JF.Utils.Data
{
    public class JFRepositoryBase<TEntity> : JFReadRepositoryBase<TEntity>, IRepositoryBase<TEntity> where TEntity : class
    {
        public JFRepositoryBase(IUnitOfWork context) : base(context)
        {
        }

        public virtual TEntity Add(TEntity entity)
        {
            return _entities.Add(entity).Entity;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public virtual ICollection<TEntity> AddRange(ICollection<TEntity> entities)
        {
            _entities.AddRange(entities);
            return entities;
        }

        public virtual async Task<int> AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _entities.AddRangeAsync(entities, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _entities.Remove(entity);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual void DeleteRange(ICollection<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        public virtual async Task<int> DeleteRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            _entities.RemoveRange(entities);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual void Update(TEntity entity)
        {
            _entities.Update(entity);
        }

        public virtual async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _entities.Update(entity);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
