namespace ApplicationCore.Entity.BaseEntity;

public abstract class StorableEntity : IEquatable<StorableEntity>
{
    public long Id { get; set; }

    public bool Equals(StorableEntity? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((StorableEntity)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}