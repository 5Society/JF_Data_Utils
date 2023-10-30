using JF.Utils.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace JF.Utils.Application.Persistence
{
    /// <summary>
    /// Interfaz que representa una unidad de trabajo para administrar transacciones y operaciones de base de datos.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Obtiene la transacción de contexto de base de datos actual, si existe.
        /// </summary>
        /// <returns>La transacción de contexto de base de datos actual o null si no hay ninguna.</returns>
        IDbContextTransaction? GetCurrentTransaction();

        /// <summary>
        /// Indica si hay una transacción activa en la unidad de trabajo.
        /// </summary>
        bool HasActiveTransaction { get; }

        /// <summary>
        /// Inicia una nueva transacción de base de datos de manera asincrónica.
        /// </summary>
        /// <returns>Una tarea que representa la transacción de base de datos recién creada.</returns>
        Task<IDbContextTransaction> BeginTransactionAsync();

        /// <summary>
        /// Confirma la transacción activa de manera asincrónica.
        /// </summary>
        /// <param name="cancellationToken">El token de cancelación para la operación asincrónica.</param>
        /// <returns>Una tarea que representa el resultado de la confirmación de la transacción.</returns>
        Task<int> CommitTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Revierte la transacción activa.
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// Guarda los cambios en la base de datos de manera asincrónica.
        /// </summary>
        /// <param name="cancellationToken">El token de cancelación para la operación asincrónica.</param>
        /// <returns>Una tarea que representa el número de entidades guardadas en la base de datos.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Agrega un repositorio específico para una entidad.
        /// </summary>
        /// <typeparam name="TEntity">El tipo de entidad para la cual se agregará el repositorio.</typeparam>
        /// <param name="repository">El repositorio que se agregará.</param>
        void AddRepository<TEntity>(object repository);

        /// <summary>
        /// Obtiene un repositorio específico para una entidad.
        /// </summary>
        /// <typeparam name="TEntity">El tipo de entidad para la cual se obtendrá el repositorio.</typeparam>
        /// <returns>El repositorio de la entidad especificada o null si no se encuentra.</returns>
        IRepository<TEntity>? Repository<TEntity>() where TEntity : class, IAggregateRoot;

        /// <summary>
        /// Obtiene un repositorio de solo lectura para un agregado raíz específico.
        /// </summary>
        /// <typeparam name="TEntity">El tipo de entidad asociado al agregado raíz.</typeparam>
        /// <returns>El repositorio de solo lectura para el agregado raíz especificado o null si no se encuentra.</returns>
        IReadRepository<TEntity>? ReadRepository<TEntity>() where TEntity : class, IAggregateRoot;

    }
}
