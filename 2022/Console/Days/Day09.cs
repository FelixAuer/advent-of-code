namespace Console.Days;

public class Day09 : IDay
{
    public void Solve()
    {
        var inputs = AoCHelper.ReadLines("09");

        PartOne(inputs);
        PartTwo(inputs);
    }

    private static void PartTwo(string[] inputs)
    {
        var knots = new Position[10];
        for (var i = 0; i < knots.Length; i++)
        {
            knots[i] = new Position();
        }

        var head = knots[0];
        var tail = knots[^1];
        var tailPositions = new HashSet<string>();

        foreach (var line in inputs)
        {
            var split = line.Split(" ");
            var direction = split[0];
            var repetitions = int.Parse(split[1]);

            for (var i = 0; i < repetitions; i++)
            {
                switch (direction)
                {
                    case "U":
                        head.Up();
                        break;
                    case "D":
                        head.Down();
                        break;
                    case "L":
                        head.Left();
                        break;
                    case "R":
                        head.Right();
                        break;
                }

                for (var j = 1; j < knots.Length; j++)
                {
                    knots[j].Approach(knots[j - 1]);
                }

                tailPositions.Add(tail.loggablePosition());
            }
        }

        System.Console.WriteLine(tailPositions.Count);
    }

    private static void PartOne(string[] inputs)
    {
        var head = new Position();
        var tail = new Position();
        var tailPositions = new HashSet<string>();

        foreach (var line in inputs)
        {
            var split = line.Split(" ");
            var direction = split[0];
            var repetitions = int.Parse(split[1]);

            for (var i = 0; i < repetitions; i++)
            {
                switch (direction)
                {
                    case "U":
                        head.Up();
                        break;
                    case "D":
                        head.Down();
                        break;
                    case "L":
                        head.Left();
                        break;
                    case "R":
                        head.Right();
                        break;
                }

                tail.Approach(head);
                tailPositions.Add(tail.loggablePosition());
            }
        }

        System.Console.WriteLine(tailPositions.Count);
    }
}

public class Position
{
    public Position(int posX = 0, int posY = 0)
    {
        PosX = posX;
        PosY = posY;
    }

    public int PosX { get; set; }
    public int PosY { get; set; }

    public void Up()
    {
        PosY++;
    }

    public void Down()
    {
        PosY--;
    }

    public void Left()
    {
        PosX--;
    }

    public void Right()
    {
        PosX++;
    }

    public void Approach(Position position)
    {
        if (Math.Abs(PosX - position.PosX) <= 1 && Math.Abs(PosY - position.PosY) <= 1)
        {
            return;
        }

        PosX += Math.Sign(position.PosX - PosX);
        PosY += Math.Sign(position.PosY - PosY);
    }

    public string loggablePosition()
    {
        return PosX + "X" + PosY;
    }
}