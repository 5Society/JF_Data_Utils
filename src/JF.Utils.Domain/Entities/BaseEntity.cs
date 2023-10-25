using System.ComponentModel.DataAnnotations;


namespace JF.Utils.Domain.Entities
{
    /// <summary>
    /// Clase base para todas las entidades
    /// </summary>
    public abstract class BaseEntity : BaseEntity<DefaultIdType>
    { 

    }

    /// <summary>
    /// Clase base para todas las entidades que tienen un identificador único.
    /// </summary>
    /// <typeparam name="TId">El tipo de dato del identificador único.</typeparam>
    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        /// <inheritdoc/>
        [Key]
        public TId Id { get; set; } = default!;

        /// <inheritdoc/>
        public object? GetId()
        {
            return Id;
        }
    }
}
