
using JF.Utils.Application.Persistence;
using System.Reflection;
using System.Threading;

namespace JF.Utils.Infrastructure.Persistence
{
    public class JFRepository<TEntity> : JFReadRepository<TEntity>, IRepository<TEntity> where TEntity : class
    {
        public JFRepository(IUnitOfWork context) : base(context)
        {
        }
        public virtual async Task<TEntity?> AddAsync(TEntity entity)
        {
            if (!ValidateModel(entity)) return null;
            await _entities.AddAsync(entity);
            return entity;
        }
        public virtual async Task<TEntity?> AddAndSaveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await UnitOfWork.BeginTransactionAsync();
            await AddAsync(entity);
            return (await UnitOfWork.CommitTransactionAsync() ) ? entity : null;
        }
        public virtual async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
        {
            await _entities.AddRangeAsync(entities);
            return entities;
        }

        public virtual async Task<int> AddRangeAndSaveAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
           
            await AddRangeAsync(entities);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public virtual void Delete(int id)
        {
            TEntity? entity = GetById(id);
            if (entity != null) Delete(entity);

        }
        public virtual async Task<bool> DeleteAndSaveAsync(int id, CancellationToken cancellationToken = default)
        {
            await UnitOfWork.BeginTransactionAsync();
            Delete(id);
            return await UnitOfWork.CommitTransactionAsync(cancellationToken);
        }

        public virtual void DeleteRange(ICollection<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }
        public virtual async Task<int> DeleteRangeAndSaveAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            DeleteRange(entities);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual bool Update(int id, TEntity entity)
        {
            //Validate id entity
            PropertyInfo? fieldId = entity.GetType().GetProperties().FirstOrDefault(f => f.Name == "Id");
            if (fieldId == null) throw new ArgumentException("Property Id cannot exists. You must implement this function");
            if (id != (int)fieldId.GetValue(entity)!) return false;
            //Validate model entity
            if (!ValidateModel(entity)) return false;
            _entities.Update(entity);
            return true;
        }

        public virtual async Task<int> UpdateAndSaveAsync(int id, TEntity entity, CancellationToken cancellationToken = default)
        {
            //Updates entity
            if (!Update(id, entity)) return 0;
            return await _context.SaveChangesAsync(cancellationToken);
        }
        /*
        public virtual async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
           _entities.Update(entity);
            return await _context.SaveChangesAsync(cancellationToken);
        }*/

        public virtual bool ValidateModel(TEntity entity) => true;

    

    }
}
