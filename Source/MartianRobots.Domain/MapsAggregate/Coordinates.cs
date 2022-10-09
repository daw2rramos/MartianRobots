namespace MartianRobots.Domain.MapsAggregate;

public sealed class Coordinates : IEquatable<Coordinates>
{
    public Coordinates(int xPos, int yPos)
    {
        XPos = xPos;
        YPos = yPos;
    }

    public int XPos { get; private set; }

    public int YPos { get; private set; }

    public bool Equals(Coordinates? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return XPos == other.XPos && YPos == other.YPos;
    }

    public static Coordinates Create(int xPos, int yPos)
    {
        return new Coordinates(xPos, yPos);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj.GetType() == GetType() && Equals((Coordinates)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(XPos, YPos);
    }

    public static bool operator ==(Coordinates? left, Coordinates? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Coordinates? left, Coordinates? right)
    {
        return !Equals(left, right);
    }
}