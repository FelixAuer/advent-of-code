namespace Console.Days;

public class Day10 : IDay
{
    private char[] _connectsDown = { '|', '7', 'F' };
    private char[] _connectsUp = { '|', 'L', 'J' };
    private char[] _connectsLeft = { '-', '7', 'J' };
    private char[] _connectsRight = { '-', 'L', 'F' };

    public void Solve()
    {
        var input = AoCHelper.ReadLines("10", false);
        var grid = new Cell[input.Length, input[0].Length];
        (int x, int y) start = (0, 0);
        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[0].Length; j++)
            {
                grid[i, j] = new Cell
                {
                    Shape = input[i][j],
                    X = i,
                    Y = j,
                };
                if (input[i][j] == 'S')
                {
                    start = (i, j);
                }
            }
        }

        var visited = new List<Cell> { grid[start.x, start.y] };
        if (_connectsDown.Contains(grid[start.x - 1, start.y].Shape))
        {
            visited.Add(grid[start.x - 1, start.y]);
        }
        else if (_connectsUp.Contains(grid[start.x + 1, start.y].Shape))
        {
            visited.Add(grid[start.x + 1, start.y]);
        }
        else if (_connectsRight.Contains(grid[start.x, start.y - 1].Shape))
        {
            visited.Add(grid[start.x, start.y - 1]);
        }
        else if (_connectsLeft.Contains(grid[start.x, start.y + 1].Shape))
        {
            visited.Add(grid[start.x, start.y + 1]);
        }

        var added = true;
        while (added)
        {
            added = false;
            var cell = visited.Last();
            if (_connectsDown.Contains(cell.Shape) && !visited.Contains(grid[cell.X + 1, cell.Y]))
            {
                added = true;
                visited.Add(grid[cell.X + 1, cell.Y]);
            }

            if (_connectsUp.Contains(cell.Shape) && !visited.Contains(grid[cell.X - 1, cell.Y]))
            {
                added = true;
                visited.Add(grid[cell.X - 1, cell.Y]);
            }

            if (_connectsLeft.Contains(cell.Shape) && !visited.Contains(grid[cell.X, cell.Y - 1]))
            {
                added = true;
                visited.Add(grid[cell.X, cell.Y - 1]);
            }

            if (_connectsRight.Contains(cell.Shape) && !visited.Contains(grid[cell.X, cell.Y + 1]))
            {
                added = true;
                visited.Add(grid[cell.X, cell.Y + 1]);
            }
        }

        System.Console.WriteLine(Math.Floor((decimal)(visited.Count / 2)));

        foreach (var cell in visited)
        {
            cell.PartOfLoop = true;
        }


        for (var i = 0; i < input.Length; i++)
        {
            var isInside = false;
            for (var j = 0; j < input[0].Length; j++)
            {
                var cell = grid[i, j];
                if (!cell.PartOfLoop)
                {
                    cell.InsideOfLoop = isInside;
                }
                else
                {
                    if (isInside)
                    {
                        switch (cell.Shape)
                        {
                            case '|':
                            case 'F':
                            case '7':
                                isInside = false;
                                break;
                        }
                    }
                    else
                    {
                        switch (cell.Shape)
                        {
                            case '|':
                            case 'F':
                            case '7':
                                isInside = true;
                                break;
                        }
                    }
                }
            }
        }

        var count = 0;
        for (var i = 0; i < input.Length; i++)
        {
            System.Console.Write("\n");
            for (var j = 0; j < input[0].Length; j++)
            {
                var cell = grid[i, j];
                if (cell.InsideOfLoop)
                {
                    System.Console.Write("I");
                    count++;
                }
                else
                {
                    System.Console.Write(cell.PartOfLoop ? cell.Shape : ".");
                }
            }
        }

        System.Console.Write("\n");

        System.Console.WriteLine(count);
    }

    private class Cell
    {
        public bool PartOfLoop { get; set; }
        public bool InsideOfLoop { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public char Shape { get; set; }
    }
}