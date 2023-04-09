using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JF.Utils.Domain.Entities
{
    public class EntityAuditable : IEntityAuditable
    {
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        [Column("CreatedBy", TypeName = "varchar")]
        [MaxLength(100)]
        public string CreatedBy { get; set; } = "";

        public DateTime? LastModifiedDate { get; set; }

        [Column("LastModifiedBy", TypeName = "varchar")]
        [MaxLength(100)]
        public string? LastModifiedBy { get; set; }
    }
}
