
namespace API_JF_Data_Utils_Example.Core.Models
{
    public class Teacher : Person
    {
        public virtual ICollection<Salon>? Salons { get; set; }
    }
}
