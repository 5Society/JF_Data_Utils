using JF.Utils.Domain.Entities;

namespace JF.Utils.Application.Persistence
{
    /// <summary>
    /// Interfaz que representa un repositorio para operaciones de lectura y escritura de entidades del tipo <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">El tipo de entidad que se manejará en el repositorio.</typeparam>
    public interface IRepository<TEntity> : IReadRepository<TEntity>, IBaseRepository<TEntity>
        where TEntity : class, IEntity
    {
    }
}
