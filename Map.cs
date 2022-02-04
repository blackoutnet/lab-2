namespace DoodleMaps;

public class Map
{
    public string[,] Maze { get; init; }
    public string Wall { get; init; }
    public string Space { get; init; }

    public string this[Point p]
    {
        get => Maze[p.Row, p.Column];
        set => Maze[p.Row, p.Column] = value;
    }
}