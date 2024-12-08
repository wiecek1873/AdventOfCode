namespace AdventOfCode2024.Commons;
public struct Vector2Int
{
    public int X;
    public int Y;

    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object? obj)
    {
        return obj is Vector2Int vector&&
               X==vector.X&&
               Y==vector.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static bool operator ==(Vector2Int left, Vector2Int right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Vector2Int left, Vector2Int right)
    {
        return !(left==right);
    }

    public static Vector2Int operator -(Vector2Int left, Vector2Int right)
    {
        return new Vector2Int(left.X - right.X, left.Y - right.Y);
    }
    public static Vector2Int operator +(Vector2Int left, Vector2Int right)
    {
        return new Vector2Int(left.X + right.X, left.Y + right.Y);
    }

    public override string ToString()
    {
        return $"({X},{Y})";
    }
}
