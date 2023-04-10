using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace JF.Utils.Domain.Entities
{
    public class EntityAuditableSoftDelete : EntityAuditableSoftDelete<DefaultIdType>
    {

    }
    public class EntityAuditableSoftDelete<TId> : EntityAuditable<TId>, IEntitySoftDelete
    {
        public DateTime? DeletedDate { get; set; }

        [Column("DeletedBy", TypeName = "varchar")]
        [MaxLength(100)]
        public string? DeletedBy { get; set; }
        [NotMapped]
        public bool IsDeleted => DeletedDate != null;
    }
}
