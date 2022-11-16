using JF.Utils.Data;
using System.ComponentModel.DataAnnotations;

namespace API_JF_Data_Utils_Example.Core.Models
{
    public class Class : EntityAuditable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
    }
}
