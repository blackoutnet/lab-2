namespace DoodleMaps;

public class MapPrinter
{
    private readonly string[,] _maze;

    public MapPrinter(Map map, Stack<Point>? path = null)
    {
        _maze = (string[,]) map.Maze.Clone();
        if (path != null)
        {
            foreach (var point in path)
            {
                _maze[point.Row, point.Column] = ".";
            }
        }
    }

    public void Print()
    {
        PrintTopLine();
        for (var row = 0; row < _maze.GetLength(0); row++)
        {
            Console.Write($"{row}\t");
            for (var column = 0; column < _maze.GetLength(1); column++)
            {
                Console.Write(_maze[row, column]);
            }

            Console.WriteLine();
        }

        void PrintTopLine()
        {
            Console.Write(" \t");
            for (var i = 0; i < _maze.GetLength(1); i++)
            {
                Console.Write(i % 10 == 0 ? i / 10 : " ");
            }

            Console.Write("\n \t");
            for (var i = 0; i < _maze.GetLength(1); i++)
            {
                Console.Write(i % 10);
            }

            Console.WriteLine("\n");
        }
    }
}