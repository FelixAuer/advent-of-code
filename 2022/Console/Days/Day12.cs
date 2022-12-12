namespace Console.Days;

public class Day12 : IDay
{
    public void Solve()
    {
        var input = AoCHelper.ReadLines("12");
        PartOne(input);
        PartTwo(input);
    }

    private static void PartOne(string[] input)
    {
        var heightmap = new Area[input.Length, input[0].Length];
        Area start = null;
        Area end = null;
        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[0].Length; j++)
            {
                var elevation = input[i][j];
                Area area;
                switch (elevation)
                {
                    case 'S':
                        area = new Area(0, i, j);
                        start = area;
                        break;
                    case 'E':
                        area = new Area(25, i, j);
                        end = area;
                        break;
                    default:
                        area = new Area(AoCHelper.ToNumber(elevation), i, j);
                        break;
                }

                heightmap[i, j] = area;
            }
        }

        var nextAreas = new Queue<Area>();

        start.Visited = true;
        nextAreas.Enqueue(start);

        while (nextAreas.Count != 0)
        {
            var currentArea = nextAreas.Dequeue();
            var neighbours = UnvisitedReachableNeighbours(heightmap, currentArea);
            foreach (var neighbour in neighbours)
            {
                neighbour.Visit(currentArea.Steps + 1);
                nextAreas.Enqueue(neighbour);
            }
        }


        System.Console.WriteLine(end.Steps);
    }

    private static void PartTwo(string[] input)
    {
        var heightmap = new Area[input.Length, input[0].Length];
        Area end = null;
        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[0].Length; j++)
            {
                var elevation = input[i][j];
                Area area;
                switch (elevation)
                {
                    case 'S':
                        area = new Area(0, i, j);
                        break;
                    case 'E':
                        area = new Area(25, i, j);
                        end = area;
                        break;
                    default:
                        area = new Area(AoCHelper.ToNumber(elevation), i, j);
                        break;
                }

                heightmap[i, j] = area;
            }
        }

        var nextAreas = new Queue<Area>();

        end.Visited = true;
        nextAreas.Enqueue(end);

        while (nextAreas.Count != 0)
        {
            var currentArea = nextAreas.Dequeue();
            var neighbours = UnvisitedReachableNeighbours2(heightmap, currentArea);
            foreach (var neighbour in neighbours)
            {
                neighbour.Visit(currentArea.Steps + 1);
                if (neighbour.Elevation == 0)
                {
                    System.Console.WriteLine(neighbour.Steps);
                    return;
                }

                nextAreas.Enqueue(neighbour);
            }
        }
    }

    private static IEnumerable<Area> UnvisitedReachableNeighbours(Area[,] heightmap, Area area)
    {
        var neighbours = Neighbours(heightmap, area);

        return neighbours.Where(a => !a.Visited && a.Elevation <= (area.Elevation + 1));
    }

    private static IEnumerable<Area> UnvisitedReachableNeighbours2(Area[,] heightmap, Area area)
    {
        var neighbours = Neighbours(heightmap, area);

        return neighbours.Where(a => !a.Visited && a.Elevation >= (area.Elevation - 1));
    }

    private static List<Area> Neighbours(Area[,] heightmap, Area area)
    {
        var posY = area.PosY;
        var posX = area.PosX;
        var neighbours = new List<Area>();
        if (posY > 0)
        {
            neighbours.Add(heightmap[posY - 1, posX]);
        }

        if (posY < heightmap.GetLength(0) - 1)
        {
            neighbours.Add(heightmap[posY + 1, posX]);
        }

        if (posX > 0)
        {
            neighbours.Add(heightmap[posY, posX - 1]);
        }

        if (posX < heightmap.GetLength(1) - 1)
        {
            neighbours.Add(heightmap[posY, posX + 1]);
        }

        return neighbours;
    }
}

class Area
{
    public int Elevation { get; }
    public bool Visited { get; set; }

    public int Steps { get; set; }

    public int PosY { get; }
    public int PosX { get; }

    public Area(int elevation, int posY, int posX)
    {
        Elevation = elevation;
        PosY = posY;
        PosX = posX;
    }

    public void Visit(int steps)
    {
        Steps = steps;
        Visited = true;
    }
}