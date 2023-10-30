using JF.Utils.Domain.Entities;
using System.Linq.Expressions;


namespace JF.Utils.Application.Persistence
{
    /// <summary>
    /// Interfaz que define operaciones de lectura básicas para entidades de tipo <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">El tipo de entidad con el que trabaja el repositorio.</typeparam>
    public interface IBaseReadRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Obtiene una consulta que representa todas las entidades del tipo <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="asNoTracking">Indica si se debe realizar un seguimiento de entidades (tracking) o no.</param>
        /// <returns>Una consulta que representa todas las entidades.</returns>
        IQueryable<TEntity> GetAll(bool asNoTracking = true);

        /// <summary>
        /// Obtiene una lista paginada de entidades del tipo <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="page">Número de página.</param>
        /// <param name="pagesize">Tamaño de página.</param>
        /// <returns>Una lista paginada de entidades.</returns>
        IList<TEntity> GetAllPaged(int page, int pagesize);

        /// <summary>
        /// Obtiene una consulta que representa todas las entidades que satisfacen un predicado.
        /// </summary>
        /// <param name="predicate">Predicado de filtro.</param>
        /// <param name="asNoTracking">Indica si se debe realizar un seguimiento de entidades (tracking) o no.</param>
        /// <returns>Una consulta que representa las entidades que cumplen el predicado.</returns>
        IQueryable<TEntity> GetAllBySpec(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true);

        /// <summary>
        /// Obtiene de manera asincrónica una entidad por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la entidad.</param>
        /// <param name="cancellationToken">Token de cancelación opcional.</param>
        /// <returns>Una tarea que representa la entidad obtenida o null si no se encuentra.</returns>
        Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene una entidad por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la entidad.</param>
        /// <returns>La entidad obtenida o null si no se encuentra.</returns>
        TEntity? GetById(object id);

        /// <summary>
        /// Obtiene de manera asincrónica una entidad que cumple con un predicado.
        /// </summary>
        /// <typeparam name="Spec">El tipo del predicado especificado.</typeparam>
        /// <param name="predicate">Predicado de filtro.</param>
        /// <param name="cancellationToken">Token de cancelación opcional.</param>
        /// <returns>Una tarea que representa la entidad obtenida o null si no se encuentra.</returns>
        Task<TEntity?> GetBySpecAsync<Spec>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene de manera asincrónica una lista de todas las entidades del tipo <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación opcional.</param>
        /// <returns>Una tarea que representa una colección de entidades.</returns>
        Task<ICollection<TEntity>> ListAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene de manera asincrónica una lista de entidades que cumplen con un predicado.
        /// </summary>
        /// <param name="predicate">Predicado de filtro.</param>
        /// <param name="cancellationToken">Token de cancelación opcional.</param>
        /// <returns>Una tarea que representa una colección de entidades que cumplen el predicado.</returns>
        Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene de manera asincrónica el número total de entidades del tipo <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación opcional.</param>
        /// <returns>Una tarea que representa el número total de entidades.</returns>
        Task<int> CountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene de manera asincrónica el número total de entidades que cumplen con un predicado.
        /// </summary>
        /// <param name="predicate">Predicado de filtro.</param>
        /// <param name="cancellationToken">Token de cancelación opcional.</param>
        /// <returns>Una tarea que representa el número total de entidades que cumplen el predicado.</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene de manera asincrónica un valor que indica si existen entidades del tipo <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación opcional.</param>
        /// <returns>Una tarea que representa si existen entidades.</returns>
        Task<bool> AnyAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene de manera asincrónica un valor que indica si existen entidades que cumplen con un predicado.
        /// </summary>
        /// <param name="predicate">Predicado de filtro.</param>
        /// <param name="cancellationToken">Token de cancelación opcional.</param>
        /// <returns>Una tarea que representa si existen entidades que cumplen el predicado.</returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene una consulta que incluye las propiedades especificadas en la consulta.
        /// </summary>
        /// <param name="includeProperties">Propiedades de navegación a incluir.</param>
        /// <returns>Una consulta que incluye las propiedades especificadas.</returns>
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
    }

}
