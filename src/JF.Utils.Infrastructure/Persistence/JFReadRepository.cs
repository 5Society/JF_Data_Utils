using JF.Utils.Application.Persistence;
using JF.Utils.Domain.Entities;
using JF.Utils.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JF.Utils.Infrastructure.Persistence
{
    /// <summary>
    /// Implementación de la interfaz <see cref="IReadRepository{TEntity}"/> que proporciona operaciones de lectura sobre entidades de un conjunto de datos.
    /// </summary>
    /// <typeparam name="TEntity">Tipo de entidad con la que trabaja el repositorio.</typeparam>
    public class JFReadRepository<TEntity> : IReadRepository<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Conjunto de entidades del tipo especificado en el contexto de base de datos.
        /// </summary>
        protected readonly DbSet<TEntity> _entities;

        /// <summary>
        /// Contexto de base de datos que proporciona acceso a las operaciones de lectura y escritura.
        /// </summary>
        protected readonly JFContext _context;

        /// <summary>
        /// Obtiene la instancia de la interfaz de unidad de trabajo asociada al contexto de base de datos.
        /// </summary>
        public IUnitOfWork UnitOfWork { get { return _context; } }

        /// <summary>
        /// Constructor que inicializa una nueva instancia de la clase <see cref="JFReadRepository{TEntity}"/>.
        /// </summary>
        /// <param name="context">Instancia del contexto de base de datos que se utilizará para acceder a las entidades.</param>
        /// <exception cref="ArgumentNullException">Se lanza cuando el argumento 'context' es nulo.</exception>
        public JFReadRepository(IUnitOfWork context)
        {
            _context = (JFContext)(context ?? throw new ArgumentNullException(nameof(context)));
            _entities = _context.Set<TEntity>();
        }

        // Métodos de operaciones de lectura:

        /// <inheritdoc/>
        public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await _entities.AnyAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _entities.AnyAsync(predicate, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _entities.CountAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _entities.Where(predicate).CountAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual IQueryable<TEntity> GetAll(bool asNoTracking = true)
        {
            return asNoTracking ? _entities.AsNoTracking()
                : _entities.AsQueryable();
        }

        /// <inheritdoc/>
        public IList<TEntity> GetAllPaged(int page, int pageSize)
        {
            return GetAll().GetPaged(page, pageSize).GetResults();
        }

        /// <inheritdoc/>
        public virtual IQueryable<TEntity> GetAllBySpec(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true)
        {
            return asNoTracking ? _entities.Where(predicate).AsNoTracking()
                : _entities.Where(predicate).AsQueryable();
        }

        /// <inheritdoc/>
        public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = GetAll();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            return queryable;
        }

        /// <inheritdoc/>
        public virtual async Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            return await _entities.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public virtual TEntity? GetById(object id)
        {
            return _entities.Find(new object[] { id });
        }

        /// <inheritdoc/>
        public virtual async Task<TEntity?> GetBySpecAsync<Spec>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _entities.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<ICollection<TEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await _entities.ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _entities.Where(predicate).ToListAsync(cancellationToken);
        }
    }

}
