using System.ComponentModel.DataAnnotations;


namespace JF.Utils.Domain.Entities
{
    public class EntityKey : IEntityKey
    {
        [Key]
        public int Id { get; set; }
    }
}
