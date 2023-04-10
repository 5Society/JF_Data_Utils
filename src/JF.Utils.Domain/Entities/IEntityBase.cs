
namespace JF.Utils.Domain.Entities
{
    public interface IEntityBase
    {

    }

    public interface IEntityBase<TId> : IEntityBase
    {
        TId Id { get; }
    }
}
