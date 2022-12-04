using System.Text.RegularExpressions;

namespace Console.Days;

public class Day04 : IDay
{
    public void Solve()
    {
        var inputs = AoCHelper.ReadLines("04");


        PartOne(inputs);
        PartTwo(inputs);
    }

    private static void PartOne(string[] inputs)
    {
        var fullyContained = 0;
        var regex = new Regex(@"(\d+)-(\d+),(\d+)-(\d+)");
        foreach (var line in inputs)
        {
            var borders = regex.Match(line).Groups;

            if ((int.Parse(borders[1].Value) <= int.Parse(borders[3].Value) &&
                 int.Parse(borders[2].Value) >= int.Parse(borders[4].Value)) ||
                (int.Parse(borders[3].Value) <= int.Parse(borders[1].Value) &&
                 int.Parse(borders[4].Value) >= int.Parse(borders[2].Value)))
            {
                fullyContained++;
            }
        }

        System.Console.WriteLine(fullyContained);
    }

    private static void PartTwo(string[] inputs)
    {
        var overlapping = 0;
        var regex = new Regex(@"(\d+)-(\d+),(\d+)-(\d+)");
        foreach (var line in inputs)
        {
            var borders = regex.Match(line).Groups;

            if ((int.Parse(borders[1].Value) <= int.Parse(borders[3].Value) &&
                 int.Parse(borders[2].Value) >= int.Parse(borders[3].Value)) ||
                (int.Parse(borders[3].Value) <= int.Parse(borders[1].Value) &&
                 int.Parse(borders[4].Value) >= int.Parse(borders[1].Value)))
            {
                overlapping++;
            }
        }

        System.Console.WriteLine(overlapping);
    }
}