using JF.Utils.Data;
using JF.Utils.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_JF_Data_Utils_Example.Core.Models
{
    public class Course : EntitySoftDelete
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Name cannot be less than 2")]
        [MaxLength(250, ErrorMessage = "Name cannot be greater than 250")]
        [Column("Name", TypeName = "varchar")]
        public string Name { get; set; } = "";
        [Required]
        [MinLength(2, ErrorMessage = "Summary cannot be less than 2")]
        [Column("Summary", TypeName = "varchar")]
        public string Summary { get; set; } = "";
    }
}
