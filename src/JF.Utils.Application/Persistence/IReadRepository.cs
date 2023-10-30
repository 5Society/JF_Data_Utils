using JF.Utils.Domain.Entities;

namespace JF.Utils.Application.Persistence
{
    /// <summary>
    /// Interfaz genérica que representa un repositorio de solo lectura para entidades del tipo <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">El tipo de entidad que se manejará en el repositorio.</typeparam>
    public interface IReadRepository<TEntity> : IBaseReadRepository<TEntity>
        where TEntity : class, IEntity
    {

        /// <summary>
        /// Obtiene la unidad de trabajo asociada al repositorio, que permite administrar transacciones y cambios en la base de datos.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }

}
