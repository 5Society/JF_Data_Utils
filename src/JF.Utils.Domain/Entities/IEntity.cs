
namespace JF.Utils.Domain.Entities
{
    /// <summary>
    /// Interfaz base para todas las entidades
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Obtiene el identificador único de la entidad, de tipo object.
        /// </summary>
        object? GetId();
    }

    /// <summary>
    /// Interfaz base para todas las entidades que tienen un identificador único.
    /// </summary>
    /// <typeparam name="TId">El tipo de dato del identificador único.</typeparam>
    public interface IEntity<TId> : IEntity
    {
        /// <summary>
        /// Obtiene o establece el identificador único de la entidad, de tipo <typeparamref name="TId"/>.
        /// </summary>
        TId Id { get; set; }
    }
}
