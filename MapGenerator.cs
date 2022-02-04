namespace DoodleMaps;

using System;
using System.Collections.Generic;

public class MapGenerator
{
    private const string Wall = "█";

    private const string Space = " ";

    private readonly MapGeneratorOptions _options;

    private readonly Random _random;

    private readonly string[,] _maze;

    public MapGenerator(MapGeneratorOptions options)
    {
        _options = options;
        _random = new Random((int) (options.Seed == -1 ? DateTime.UtcNow.Ticks : options.Seed));
        _maze = new string[_options.Height, _options.Width];
    }

    public Map? Generate()
    {
        return _options.Type == MapType.Maze ? GenerateMaze() : null;
    }

    private Map GenerateMaze()
    {
        for (var x = 0; x < _maze.GetLength(0); x++)
        {
            for (var y = 0; y < _maze.GetLength(1); y++)
            {
                _maze[x, y] = y % 2 == 1 || x % 2 == 1 ? Wall : Space;
            }
        }

        ExpandFrom(new Point(0, 0), new List<Point>());
        RemoveWalls(_maze, _options.Noise);
        return new Map
        {
            Wall = Wall,
            Space = Space,
            Maze = _maze
        };

        void ExpandFrom(Point point, ICollection<Point> visited)
        {
            visited.Add(point);
            var neighbours = GetNeighbours(point.Row, point.Column).ToArray();
            Shuffle(_random, neighbours);
            foreach (var neighbour in neighbours)
            {
                if (!visited.Contains(neighbour))
                {
                    RemoveWallBetween(point, neighbour);
                    ExpandFrom(neighbour, visited);
                }
            }
        }

        void RemoveWallBetween(Point a, Point b)
        {
            _maze[(a.Row + b.Row) / 2, (a.Column + b.Column) / 2] = Space;
        }

        void Shuffle(Random rng, IList<Point> array)
        {
            var n = array.Count;
            while (n > 1)
            {
                var k = rng.Next(n--);
                (array[n], array[k]) = (array[k], array[n]);
            }
        }
    }

    private void RemoveWalls(string[,] maze, float chance)
    {
        for (var x = 0; x < maze.GetLength(0); x++)
        {
            for (var y = 0; y < maze.GetLength(1); y++)
            {
                if (_random.NextDouble() < chance && maze[x, y] == Wall)
                {
                    maze[x, y] = Space;
                }
            }
        }
    }

    private List<Point> GetNeighbours(int x, int y)
    {
        var result = new List<Point>();
        TryAddWithOffset(result, x, y, 2, 0);
        TryAddWithOffset(result, x, y, -2, 0);
        TryAddWithOffset(result, x, y, 0, 2);
        TryAddWithOffset(result, x, y, 0, -2);

        return result;
    }

    private void TryAddWithOffset(List<Point> points, int x, int y, int offsetX, int offsetY)
    {
        var newX = x + offsetX;
        var newY = y + offsetY;
        if (newX >= 0 && newY >= 0 && newX < _maze.GetLength(0) && newY < _maze.GetLength(1))
        {
            points.Add(new Point(newY, newX));
        }
    }
}