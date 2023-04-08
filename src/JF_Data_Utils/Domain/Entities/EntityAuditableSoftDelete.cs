using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JF.Utils.Data.Domain.Entities
{
    public class EntityAuditableSoftDelete : EntityAuditable, IEntitySoftDelete
    {
        public DateTime? DeletedDate { get; set; }

        [Column("DeletedBy", TypeName = "varchar")]
        [MaxLength(100)]
        public string? DeletedBy { get; set; }
        [NotMapped]
        public bool IsDeleted => DeletedDate != null;
    }
}
