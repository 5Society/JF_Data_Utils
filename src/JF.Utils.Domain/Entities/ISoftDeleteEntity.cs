
namespace JF.Utils.Domain.Entities
{
    /// <summary>
    /// Interfaz para entidades que admiten eliminación lógica (soft delete).
    /// </summary>
    public interface ISoftDeleteEntity
    {
        /// <summary>
        /// Obtiene o establece la fecha de eliminación de la entidad, si ha sido eliminada de forma lógica.
        /// </summary>
        public DateTime? DeletedDate { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del usuario que realizó la eliminación lógica de la entidad, si ha sido eliminada.
        /// </summary>
        public string? DeletedBy { get; set; }

        /// <summary>
        /// Obtiene un valor que indica si la entidad ha sido eliminada de forma lógica.
        /// </summary>
        public bool IsDeleted { get; }
    }

}
