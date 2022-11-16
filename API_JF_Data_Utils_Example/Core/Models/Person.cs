using JF.Utils.Data;
using System.ComponentModel.DataAnnotations;

namespace API_JF_Data_Utils_Example.Core.Models
{
    public abstract class Person :  EntityAuditableSoftDelete
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string LastName { get; set; } = "";
    }
}
