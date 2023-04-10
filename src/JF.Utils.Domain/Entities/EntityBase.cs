using System.ComponentModel.DataAnnotations;


namespace JF.Utils.Domain.Entities
{
    public abstract class EntityBase : EntityBase<DefaultIdType>
    { 

    }
    public abstract class EntityBase<TId> : IEntityBase<TId>
    {
        [Key]
        public TId Id { get; protected set; } = default!;
    }
}
