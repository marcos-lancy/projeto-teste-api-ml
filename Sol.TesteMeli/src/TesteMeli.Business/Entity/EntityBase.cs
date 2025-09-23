using TesteMeli.Business.Interfaces;

namespace TesteMeli.Business.Entity;

public abstract class EntityBase<TId> : IEntityBase<TId> where TId : IEquatable<TId>
{
    public TId Id { get; protected set; }

    protected EntityBase(TId id)
    {
        Id = id;
    }

    // Igualdade baseada no ID
    public override bool Equals(object obj)
    {
        if (obj is not EntityBase<TId> other) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id);
    }

    public override int GetHashCode() => Id.GetHashCode();
}
