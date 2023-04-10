
namespace JF.Utils.Domain.Entities
{
    public interface IEntityBase 
    {
        object? GetId();
    }

    public interface IEntityBase<TId> : IEntityBase
    {
        TId Id { get; set; }
    }
}
