using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JF.Utils.Data.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JF.Utils.Data
{
    public class EntitySoftDelete : IEntitySoftDelete
    {
        public DateTime? DeletedDate { get; set; }

        [Column("DeletedBy", TypeName = "varchar")]
        [MaxLength(100)]
        public string? DeletedBy { get; set; }
    }
}
