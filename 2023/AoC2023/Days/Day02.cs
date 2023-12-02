using System.Text.RegularExpressions;

namespace Console.Days;

public class Day02 : IDay
{
    public void Solve()
    {
        var input = AoCHelper.ReadLines("02");
        var sum1 = 0;
        var sum2 = 0;
        foreach (var line in input)
        {
            var id = Regex.Match(line, "Game (\\d+)").Groups[1].ToString();
            var maxRed = GetMax(line, "red");
            var maxGreen = GetMax(line, "green");
            var maxBlue = GetMax(line, "blue");
            if (maxRed <= 12 && maxGreen <= 13 && maxBlue <= 14)
            {
                sum1 += int.Parse(id);
            }

            sum2 += maxRed * maxGreen * maxBlue;
        }

        System.Console.WriteLine(sum1);
        System.Console.WriteLine(sum2);
    }

    public int GetMax(string line, string color)
    {
        var matches = Regex.Matches(line, $"(\\d+) {color}");
        var max = matches.Select(match => int.Parse(match.Groups[1].ToString())).Max();
        return max;
    }
}