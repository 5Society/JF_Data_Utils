using JF.Utils.Data;
using JF.Utils.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace API_JF_Data_Utils_Example.Core.Models
{
    public class Course : EntitySoftDelete
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Summary { get; set; } = "";
    }
}
