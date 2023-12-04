using System.Text.RegularExpressions;

namespace Console.Days;

public class Day04 : IDay
{
    public void Solve()
    {
        var input = AoCHelper.ReadLines("04");
        var sum = 0;
        var winningCountPerCard = new List<int>();
        foreach (var line in input)
        {
            var split = line.Split(": ");
            var splitNumbers = split[1].Split(" | ");
            var winning = Regex.Matches(splitNumbers[0], "(\\d+)").Select(match => match.ToString());
            var drawn = Regex.Matches(splitNumbers[1], "(\\d+)").Select(match => match.ToString());
            var winningDrawn = winning.Intersect(drawn);
            var winningCount = winningDrawn.Count();
            winningCountPerCard.Add(winningCount);
            if (winningCount > 0)
            {
                sum += (int)(1 * Math.Pow(2, winningCount - 1));
            }
        }

        for (var i = winningCountPerCard.Count() - 1; i >= 0; i--)
        {
            var j = winningCountPerCard[i];
            while (j > 0)
            {
                winningCountPerCard[i] += winningCountPerCard[i + j];
                j--;
            }
        }

        System.Console.WriteLine(sum);
        System.Console.WriteLine(winningCountPerCard.Sum() + input.Length);
    }
}