namespace TesteMeli.Business.Interfaces;

public interface IEntityBase<TId> where TId : IEquatable<TId>
{
    TId Id { get; }
}
