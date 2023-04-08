using JF.Utils.Data.Interfaces;
using System.Reflection;

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

        public virtual bool AddAndSave(TEntity entity)
        {
            if (!ValidateEntityModel(entity)) return false;
            UnitOfWork.BeginTransaction();
            Add(entity);
            return UnitOfWork.CommitTransaction();
        }

        public virtual async Task<TEntity?> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (!ValidateEntityModel(entity)) return null;
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

        public virtual void Delete(int id)
        {
            TEntity? entity = GetById(id);
            if (entity != null) Delete(entity);

        }
        public virtual bool DeleteAndSave(int id)
        {
            UnitOfWork.BeginTransaction();
            Delete(id);
            return UnitOfWork.CommitTransaction();
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

        public virtual bool UpdateAndSave(int id, TEntity entity)
        {
            //Validate id entity
            PropertyInfo? fieldId = entity.GetType().GetProperties().FirstOrDefault(f => f.Name == "Id");
            if (fieldId == null) throw new ArgumentException("Property Id cannot exists. You must implement this function");
            if (id != ((int)fieldId.GetValue(entity)!)) return false;
            //Validate model entity
            if (!ValidateEntityModel(entity)) return false;
            UnitOfWork.BeginTransaction();
            //Updates entity
            Update(entity);
            return UnitOfWork.CommitTransaction();
        }
        public virtual async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _entities.Update(entity);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual bool ValidateEntityModel(TEntity entity)
        {
            return true;
        }
    }
}
