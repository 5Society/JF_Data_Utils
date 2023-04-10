
namespace JF.Utils.Domain.Entities
{
    public interface IEntityAuditable
    {
        /* auditable fields */
        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
