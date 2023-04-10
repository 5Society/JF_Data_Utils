
namespace JF.Utils.Domain.Entities
{
    public interface IEntitySoftDelete
    {
        public DateTime? DeletedDate { get; set; }

        public string? DeletedBy { get; set; }

        public bool IsDeleted { get; }
    }
}
