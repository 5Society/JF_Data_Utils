using JF.Utils.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_JF_Data_Utils_Example.Core.Models
{
    public class Salon : EntitySoftDelete<int>, IAggregateRoot
    {
        [Required]
        [MinLength(2, ErrorMessage = "Name cannot be less than 2")]
        [MaxLength(250, ErrorMessage = "Name cannot be greater than 250")]
        [Column("Name", TypeName = "varchar")]
        public string Name { get; set; } = "";

        [Required]
        public int CourseId { get; set; }
        [Required]
        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

        public int? TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public virtual Teacher? Teacher { get; set; }
    }
}
