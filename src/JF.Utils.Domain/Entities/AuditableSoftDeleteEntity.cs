using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace JF.Utils.Domain.Entities
{
    /// <summary>
    /// Clase abstracta que representa una entidad auditada con eliminación lógica (soft delete) y un identificador de tipo DefaultIdType.
    /// </summary>
    public abstract class AuditableSoftDeleteEntity : AuditableSoftDeleteEntity<DefaultIdType>
    {

    }

    /// <summary>
    /// Clase abstracta que representa una entidad auditada con eliminación lógica (soft delete) y un identificador de tipo <typeparamref name="TId"/>.
    /// </summary>
    public abstract class AuditableSoftDeleteEntity<TId> : AuditableEntity<TId>, ISoftDeleteEntity
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
