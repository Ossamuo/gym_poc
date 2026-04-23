namespace Gym.Domain.Contexts.SharedContext.Entities;
/// <summary>
/// Base Class for all Entities of the application.
/// all this will be a representation of an Entity in the Domain model
/// </summary>
/// <param name="id"></param>
public class Entity(Guid id): IEquatable<Entity>, IEquatable<Guid>
{
    #region Properties

    public Guid Id { get; init; } = id;

    #endregion
    
    #region Equals Methods

    public bool Equals(Guid other) => Id == other;


    public bool Equals(Entity? other)
    {
        if (other is null) return false;
        return ReferenceEquals(this, other) || Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Entity)obj);
    }

   
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    #endregion

    #region Operators

    public static bool operator ==(Entity ? left, Entity? right) => Equals(left, right);
    public static bool operator != (Entity ? left, Entity? right) => !Equals(left, right);

    #endregion
}