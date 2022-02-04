namespace DoodleMaps;

public class MapShortestPath
{
    private readonly Map _map;
    private readonly Point _source;
    private readonly Point _destination;
    private readonly bool _addTraffic;

    public MapShortestPath(Map map, Point source, Point destination, bool addTraffic)
    {
        _map = map;
        _source = source;
        _destination = destination;
        _addTraffic = addTraffic;
    }

    public Stack<Point>? Get()
    {
        return _addTraffic ? null : GetShortestPathDijkstra();
    }


    // private Stack<Point>? GetShortestPathAStar()
    // {
    //     var path = new Stack<Point>();
    //     var graph = MapToGraph();
    //     var vertexCount = graph.Keys.Count;
    //     var visitedVertices = new HashSet<Point>(vertexCount);
    //     var distances = new Dictionary<Point, int>(vertexCount);
    //     var origins = new Dictionary<Point, Point>(vertexCount);
    //
    //     visitedVertices.Add(_source);
    //     distances.Add(_destination);
    //     
    //     
    //
    //     return path;
    // }
    
    

    private Stack<Point> GetShortestPathDijkstra()
    {

        var path = new Stack<Point>();
        var graph = MapToGraph();
        var vertexCount = graph.Keys.Count;
        var vertices = new PriorityQueue<Point, int>(vertexCount);
        var visitedVertices = new HashSet<Point>(vertexCount);
        var distances = new Dictionary<Point, int>(vertexCount);
        var origins = new Dictionary<Point, Point>(vertexCount);

        foreach (var vertex in graph.Keys)
        {
            distances[vertex] = int.MaxValue;
        }


        distances[_source] = 0;
        vertices.Enqueue(_source, 0);
        visitedVertices.Add(_source);

        while (vertices.Count != 0)
        {
            var v = vertices.Dequeue();
            if (origins.ContainsKey(_destination) || v == _destination)
            {
                var u = _destination;
                path.Push(u);
                while (origins.ContainsKey(u))
                {
                    u = origins[u];
                    path.Push(u);
                }

                break;
            }

            foreach (var u in graph[v])
            {
                var newDistance = distances[v] + 1;
                if (newDistance < distances[u])
                {
                    distances[u] = newDistance;
                    origins[u] = v;
                    if (!visitedVertices.Contains(u))
                    {
                        vertices.Enqueue(u, newDistance);
                        visitedVertices.Add(u);
                    }
                }
            }
        }

        return path;
    }

    private Dictionary<Point, List<Point>> MapToGraph()
    {
        var maze = _map.Maze;
        var graph = new Dictionary<Point, List<Point>>();
        var neighbors = new List<Point>();
        var nRows = maze.GetLength(0);
        var nColumns = maze.GetLength(1);
        for (var i = 0; i < nRows; i++)
        {
            for (var j = 0; j < nColumns; j++)
            {
                if (maze[i, j] == "█") continue;
                foreach (var neighbor in Enum.GetValues(typeof(VertexNeighbors)))
                {
                    var rowOffset = 0;
                    var columnOffset = 0;
                    switch (neighbor)
                    {
                        case VertexNeighbors.Left:
                            rowOffset = 0;
                            columnOffset = -1;
                            break;
                        case VertexNeighbors.Right:
                            rowOffset = 0;
                            columnOffset = 1;
                            break;
                        case VertexNeighbors.Lower:
                            rowOffset = -1;
                            columnOffset = 0;
                            break;
                        case VertexNeighbors.Upper:
                            rowOffset = 1;
                            columnOffset = 0;
                            break;
                    }

                    var row = i + rowOffset;
                    var column = j + columnOffset;
                    if (row < nRows && row >= 0 && column < nColumns && column >= 0 && maze[row, column] != "█")
                    {
                        neighbors.Add(new Point(column, row));
                    }
                }

                var vertex = new Point(j, i);
                graph[vertex] = new List<Point>(neighbors);
                neighbors.Clear();
            }
        }

        return graph;
    }
}