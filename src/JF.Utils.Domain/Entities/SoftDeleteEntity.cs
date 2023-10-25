using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace JF.Utils.Domain.Entities
{
    /// <summary>
    /// Clase que representa una entidad con eliminación lógica (soft delete) y un identificador de tipo DefaultIdType.
    /// </summary>
    public abstract class SoftDeleteEntity : SoftDeleteEntity<DefaultIdType>
    {

    }

    /// <summary>
    /// Clase que representa una entidad con eliminación lógica (soft delete) y un identificador de tipo <typeparamref name="TId"/>.
    /// </summary>
    public abstract class SoftDeleteEntity<TId> : BaseEntity<TId>, ISoftDeleteEntity
    {
        /// <inheritdoc/>
        public DateTime? DeletedDate { get; set; }

        /// <inheritdoc/>
        [Column("DeletedBy", TypeName = "varchar")]
        [MaxLength(100)]
        public string? DeletedBy { get; set; }

        /// <inheritdoc/>
        [NotMapped]
        public bool IsDeleted => DeletedDate != null;
    }
}
