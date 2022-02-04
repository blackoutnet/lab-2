using DoodleMaps;


var generator = new MapGenerator(new MapGeneratorOptions
{
    Type = MapType.Maze,
    Height = 35,
    Width = 90,
    Seed = 0,
    AddTraffic = false,
    TrafficSeed = 1234
});

var map = generator.Generate();
if (map != null)
{
    var shortestPathGenerator = new MapShortestPath(
        map,
        new Point(18, 0),
        new Point(88, 34),
        false
    );
    var shortestPath = shortestPathGenerator.Get();
    var printer = new MapPrinter(map, shortestPath);
    if (shortestPath == null)
    {
        Console.WriteLine("At least one of the points is a wall!");
    }
    else if (shortestPath.Count == 0)
    {
        Console.WriteLine("source and destination vertices are in different connected components!");
    }

    printer.Print();
}