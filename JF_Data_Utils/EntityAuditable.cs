using JF.Utils.Data.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JF.Utils.Data
{
    public class EntityAuditable : IEntityAuditable
    {
        [Required] 
        public DateTime CreatedDate { get; set; }= DateTime.Now;
        
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
