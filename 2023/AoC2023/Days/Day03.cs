using System.Text.RegularExpressions;

namespace Console.Days;

public class Day03 : IDay
{
    public void Solve()
    {
        var input = AoCHelper.ReadLines("03");
        var height = input.Length + 2;
        var width = input[0].Length + 2;
        var grid = SetupGrid(width, height, input);

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                if (grid[i, j].IsSymbol())
                {
                    SetAllAdjacent(grid, i, j);
                }
            }
        }

        var outString = GridToString(height, width, grid);

        var matches = Regex.Matches(outString, "(\\d+)");
        var sum = matches.Select(match => int.Parse(match.ToString())).Sum();
        System.Console.WriteLine(sum);

        var gearSum = 0;
        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                if (grid[i, j].Content.Equals("*"))
                {
                    var stringAround =
                        grid[i - 1, j - 1].Content +
                        grid[i - 1, j].Content +
                        grid[i - 1, j + 1].Content +
                        "." +
                        grid[i, j - 1].Content +
                        "*" +
                        grid[i, j + 1].Content +
                        "." +
                        grid[i + 1, j - 1].Content +
                        grid[i + 1, j].Content +
                        grid[i + 1, j + 1].Content;
                    var matchesGears = Regex.Matches(stringAround, "(\\d)+");
                    if (matchesGears.Count == 2)
                    {
                        var gearGrid = SetupGrid(width, height, input);
                        SetAllAdjacent(gearGrid, i, j);

                        var outGearString = GridToString(height, width, gearGrid);

                        var matchesGearsOut = Regex.Matches(outGearString, "(\\d+)");
                        gearSum += int.Parse(matchesGearsOut[0].ToString()) * int.Parse(matchesGearsOut[1].ToString());
                    }
                }
            }
        }

        System.Console.Out.WriteLine(gearSum);
    }

    private static string GridToString(int height, int width, Cell[,] grid)
    {
        var outString = "";
        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var cell = grid[i, j];
                if (cell.IsNumber() && cell.HasSymbolAdjacent)
                {
                    outString += cell.Content;
                }
                else
                {
                    outString += ".";
                }
            }
        }

        return outString;
    }

    private void SetAllAdjacent(Cell[,] grid, int i, int j)
    {
        SetAdjacent(grid, i - 1, j - 1);
        SetAdjacent(grid, i - 1, j);
        SetAdjacent(grid, i - 1, j + 1);
        SetAdjacent(grid, i, j - 1);
        SetAdjacent(grid, i, j + 1);
        SetAdjacent(grid, i + 1, j - 1);
        SetAdjacent(grid, i + 1, j);
        SetAdjacent(grid, i + 1, j + 1);
    }

    private static Cell[,] SetupGrid(int width, int height, string[] input)
    {
        var grid = new Cell[width, height];
        for (var j = 0; j < width; j++)
        {
            grid[0, j] = new Cell(".");
        }

        for (var i = 1; i < height - 1; i++)
        {
            grid[i, 0] = new Cell(".");
            for (var j = 1; j < width - 1; j++)
            {
                grid[i, j] = new Cell(
                    input[i - 1]
                        .ToCharArray()[j - 1]
                        .ToString()
                );
            }

            grid[i, width - 1] = new Cell(".");
        }

        for (var j = 0; j < width; j++)
        {
            grid[height - 1, j] = new Cell(".");
        }

        return grid;
    }

    private void SetAdjacent(Cell[,] grid, int i, int j)
    {
        var cell = grid[i, j];
        if (cell.HasSymbolAdjacent || !cell.IsNumber())
        {
            return;
        }

        cell.HasSymbolAdjacent = true;
        SetAdjacent(grid, i, j - 1);
        SetAdjacent(grid, i, j + 1);
    }

    private class Cell
    {
        public Cell(string content)
        {
            Content = content;
        }

        public string Content { get; set; }
        public bool HasSymbolAdjacent { get; set; }

        public override string ToString()
        {
            return Content;
        }

        public bool IsSymbol()
        {
            return !Content.Equals(".") && !IsNumber();
        }

        public bool IsNumber()
        {
            return int.TryParse(Content, out _);
        }
    }
}