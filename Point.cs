namespace DoodleMaps;

public readonly struct Point
{
    public int Column { get; }
    public int Row { get; }

    public Point(int column, int row)
    {
        Column = column;
        Row = row;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Column, Row);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }
        var point = (Point) obj;
        return Column == point.Column && Row == point.Row;
    }

    public static bool operator ==(Point left, Point right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Point left, Point right)
    {
        return !(left == right);
    }
}
