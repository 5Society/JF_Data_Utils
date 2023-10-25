

namespace JF.Utils.Application.Persistence
{
    /// <summary>
    /// Interfaz que define operaciones básicas de repositorio para entidades de tipo <typeparamref name="TEntity"/>.
    /// Hereda de la interfaz <see cref="IReadRepositoryBase{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity">El tipo de entidad con el que trabaja el repositorio.</typeparam>
    public interface IRepositoryBase<TEntity> : IReadRepositoryBase<TEntity> where TEntity : class
    {
        /// <summary>
        /// Agrega una entidad de tipo <typeparamref name="TEntity"/> de manera asincrónica.
        /// </summary>
        /// <param name="entity">La entidad a agregar.</param>
        /// <returns>Una tarea que representa la entidad agregada o null si no se pudo agregar.</returns>
        Task<TEntity?> AddAsync(TEntity entity);

        /// <summary>
        /// Agrega una entidad de tipo <typeparamref name="TEntity"/> de manera asincrónica y guarda los cambios.
        /// </summary>
        /// <param name="entity">La entidad a agregar.</param>
        /// <param name="cancellationToken">Token de cancelación opcional.</param>
        /// <returns>Una tarea que representa la entidad agregada o null si no se pudo agregar.</returns>
        Task<TEntity?> AddAndSaveAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Agrega una colección de entidades de tipo <typeparamref name="TEntity"/> de manera asincrónica.
        /// </summary>
        /// <param name="entities">La colección de entidades a agregar.</param>
        /// <returns>Una tarea que representa una colección de entidades agregadas.</returns>
        Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);

        /// <summary>
        /// Agrega una colección de entidades de tipo <typeparamref name="TEntity"/> de manera asincrónica y guarda los cambios.
        /// </summary>
        /// <param name="entities">La colección de entidades a agregar.</param>
        /// <param name="cancellationToken">Token de cancelación opcional.</param>
        /// <returns>Una tarea que representa el número de entidades agregadas.</returns>
        Task<int> AddRangeAndSaveAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Elimina una entidad de tipo <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="entity">La entidad a eliminar.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Elimina una entidad de tipo <typeparamref name="TEntity"/> por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la entidad a eliminar.</param>
        void Delete(object id);

        /// <summary>
        /// Elimina una entidad de tipo <typeparamref name="TEntity"/> por su identificador de manera asincrónica y guarda los cambios.
        /// </summary>
        /// <param name="id">Identificador de la entidad a eliminar.</param>
        /// <param name="cancellationToken">Token de cancelación opcional.</param>
        /// <returns>Una tarea que representa si la entidad se eliminó correctamente.</returns>
        Task<bool> DeleteAndSaveAsync(object id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Elimina una colección de entidades de tipo <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="entities">La colección de entidades a eliminar.</param>
        void DeleteRange(ICollection<TEntity> entities);

        /// <summary>
        /// Elimina una colección de entidades de tipo <typeparamref name="TEntity"/> de manera asincrónica y guarda los cambios.
        /// </summary>
        /// <param name="entities">La colección de entidades a eliminar.</param>
        /// <param name="cancellationToken">Token de cancelación opcional.</param>
        /// <returns>Una tarea que representa el número de entidades eliminadas.</returns>
        Task<int> DeleteRangeAndSaveAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Actualiza una entidad de tipo <typeparamref name="TEntity"/> por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la entidad a actualizar.</param>
        /// <param name="entity">La entidad con los nuevos datos.</param>
        /// <returns>Un valor booleano que indica si la actualización tuvo éxito.</returns>
        bool Update(object id, TEntity entity);

        /// <summary>
        /// Actualiza una entidad de tipo <typeparamref name="TEntity"/> por su identificador de manera asincrónica y guarda los cambios.
        /// </summary>
        /// <param name="id">Identificador de la entidad a actualizar.</param>
        /// <param name="entity">La entidad con los nuevos datos.</param>
        /// <param name="cancellationToken">Token de cancelación opcional.</param>
        /// <returns>Una tarea que representa el número de entidades actualizadas.</returns>
        Task<int> UpdateAndSaveAsync(object id, TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Valida una entidad de tipo <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="entity">La entidad a validar.</param>
        /// <returns>Un valor booleano que indica si la entidad es válida.</returns>
        bool ValidateModel(TEntity entity);
    }

}
