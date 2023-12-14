namespace Console.Days;

public class Day14 : IDay
{
    public void Solve()
    {
        var input = AoCHelper.ReadLines("14", true);
        var height = input.Length + 2;
        var width = input[0].Length + 2;
        Part1(width, height, input);
    }

    private static void Part1(int width, int height, string[] input)
    {
        var grid = SetupGrid(width, height, input);

        Roll(height, width, grid);

        var sum = 0;
        for (var i = 0; i < height; i++)
        {
            var c = 0;
            for (var j = 0; j < width; j++)
            {
                if (grid[i, j].IsStone())
                {
                    c++;
                }
            }

            sum += (height - i - 1) * c;
        }

        System.Console.WriteLine(sum);
    }
    
    private static void Roll(int height, int width, Cell[,] grid)
    {
        var moved = true;
        while (moved)
        {
            moved = false;
            for (var i = 1; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (grid[i, j].IsStone() && grid[i - 1, j].IsEmpty())
                    {
                        grid[i, j].Content = '.';
                        grid[i - 1, j].Content = 'O';
                        moved = true;
                    }
                }
            }
        }
    }

    private static Cell[,] SetupGrid(int width, int height, string[] input)
    {
        var grid = new Cell[width, height];
        for (var j = 0; j < width; j++)
        {
            grid[0, j] = new Cell('#');
        }

        for (var i = 1; i < height - 1; i++)
        {
            grid[i, 0] = new Cell('#');
            for (var j = 1; j < width - 1; j++)
            {
                grid[i, j] = new Cell(
                    input[i - 1]
                        .ToCharArray()[j - 1]
                );
            }

            grid[i, width - 1] = new Cell('#');
        }

        for (var j = 0; j < width; j++)
        {
            grid[height - 1, j] = new Cell('#');
        }

        return grid;
    }

    private class Cell
    {
        public Cell(char content)
        {
            Content = content;
        }

        public char Content { get; set; }

        public override string ToString()
        {
            return Content.ToString();
        }

        public bool IsFixed()
        {
            return Content.Equals('#');
        }

        public bool IsStone()
        {
            return Content.Equals('O');
        }

        public bool IsEmpty()
        {
            return Content.Equals('.');
        }
    }
}