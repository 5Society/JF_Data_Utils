
using JF.Utils.Application.Persistence;
using JF.Utils.Domain.Entities;

namespace JF.Utils.Infrastructure.Persistence
{
    /// <summary>
    /// Implementación de la interfaz <see cref="IRepository{TEntity}"/> que proporciona operaciones de lectura y escritura sobre entidades de un conjunto de datos.
    /// </summary>
    /// <typeparam name="TEntity">Tipo de entidad con la que trabaja el repositorio.</typeparam>
    public class JFRepository<TEntity> : JFReadRepository<TEntity>, IRepository<TEntity>
        where TEntity : class, IAggregateRoot
    {
        /// <summary>
        /// Constructor que inicializa una nueva instancia de la clase <see cref="JFRepository{TEntity}"/>.
        /// </summary>
        /// <param name="context">Instancia del contexto de base de datos que se utilizará para acceder a las entidades.</param>
        public JFRepository(IUnitOfWork context) : base(context)
        {
        }

        // Métodos de operaciones de escritura:

        /// <inheritdoc/>
        public virtual async Task<TEntity?> AddAsync(TEntity entity)
        {
            if (!ValidateModel(entity)) return null;
            await _entities.AddAsync(entity);
            return entity;
        }

        /// <inheritdoc/>
        public virtual async Task<TEntity?> AddAndSaveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await UnitOfWork.BeginTransactionAsync();
            await AddAsync(entity);
            await UnitOfWork.CommitTransactionAsync(cancellationToken);
            return GetById(entity.GetId()!);
        }

        /// <inheritdoc/>
        public virtual async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
        {
            await _entities.AddRangeAsync(entities);
            return entities;
        }

        /// <inheritdoc/>
        public virtual async Task<int> AddRangeAndSaveAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await AddRangeAsync(entities);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }

        /// <inheritdoc/>
        public virtual void Delete(object id)
        {
            TEntity? entity = GetById(id);
            if (entity != null) Delete(entity);
        }

        /// <inheritdoc/>
        public virtual async Task<bool> DeleteAndSaveAsync(object id, CancellationToken cancellationToken = default)
        {
            await UnitOfWork.BeginTransactionAsync();
            Delete(id);
            return (await UnitOfWork.CommitTransactionAsync(cancellationToken)) > 0;
        }

        /// <inheritdoc/>
        public virtual void DeleteRange(ICollection<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        /// <inheritdoc/>
        public virtual async Task<int> DeleteRangeAndSaveAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            DeleteRange(entities);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual bool Update(object id, TEntity entity)
        {
            // Valida si el id de la entidad coincide con el id proporcionado
            if (!id.Equals(entity.GetId())) return false;
            // Valida el modelo de la entidad
            if (!ValidateModel(entity)) return false;
            _entities.Update(entity);
            return true;
        }

        /// <inheritdoc/>
        public virtual async Task<int> UpdateAndSaveAsync(object id, TEntity entity, CancellationToken cancellationToken = default)
        {
            // Actualiza la entidad y guarda los cambios
            if (!Update(id, entity)) return 0;
            return await _context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual bool ValidateModel(TEntity entity) => true;
    }

}
