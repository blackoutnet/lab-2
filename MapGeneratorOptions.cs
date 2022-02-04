namespace DoodleMaps;

public class MapGeneratorOptions
{
    public int Width { get; init; }

    public int Height { get; init; }

    public MapType Type { get; init; }

    public float Noise { get; init; }

    public int Seed { get; init; } = -1;

    public bool AddTraffic { get; set; } = false;
    
    public int TrafficSeed { get; set; }
}

public enum MapType
{
    Maze,
    Grid
}