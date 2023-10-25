using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JF.Utils.Domain.Entities
{
    /// <summary>
    /// Clase abstracta que representa una entidad auditable con un identificador de tipo DefaultIdType.
    /// </summary>
    public abstract class AuditableEntity : AuditableEntity<DefaultIdType>
    { 
    }

    /// <summary>
    /// Clase abstracta que representa una entidad auditable con un identificador de tipo <typeparamref name="TId"/>.
    /// </summary>
    public abstract class AuditableEntity<TId> : BaseEntity<TId>, IAuditableEntity
    {
        /// <inheritdoc/>
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <inheritdoc/>
        [Required]
        [Column("CreatedBy", TypeName = "varchar")]
        [MaxLength(100)]
        public string CreatedBy { get; set; } = "";

        /// <inheritdoc/>
        public DateTime? LastModifiedDate { get; set; }

        /// <inheritdoc/>
        [Column("LastModifiedBy", TypeName = "varchar")]
        [MaxLength(100)]
        public string? LastModifiedBy { get; set; }
    }
}
