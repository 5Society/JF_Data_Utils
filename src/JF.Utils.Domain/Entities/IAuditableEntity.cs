
namespace JF.Utils.Domain.Entities
{
    /// <summary>
    /// Interfaz para entidades auditables.
    /// </summary>
    public interface IAuditableEntity
    {
        /// <summary>
        /// Obtiene o establece la fecha de creación de la entidad.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del creador de la entidad.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha de la última modificación de la entidad, si ha sido modificada.
        /// </summary>
        public DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del último usuario que modificó la entidad, si ha sido modificada.
        /// </summary>
        public string? LastModifiedBy { get; set; }
    }

}
