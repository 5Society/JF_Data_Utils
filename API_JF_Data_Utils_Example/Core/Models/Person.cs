using JF.Utils.Data.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_JF_Data_Utils_Example.Core.Models
{
    public abstract class Person :  EntityAuditableSoftDelete
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Name cannot be less than 2")]
        [MaxLength(250, ErrorMessage = "Name cannot be greater than 250")]
        [Column("Name", TypeName = "varchar")]
        public string Name { get; set; } = "";
        [Required]
        [MinLength(2, ErrorMessage = "Last name cannot be less than 2")]
        [MaxLength(250, ErrorMessage = "Last name cannot be greater than 250")]
        [Column("LastName", TypeName = "varchar")]
        public string LastName { get; set; } = "";
    }
}
