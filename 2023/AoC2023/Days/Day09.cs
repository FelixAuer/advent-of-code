namespace Console.Days;

public class Day09 : IDay
{
    public void Solve()
    {
        var input = AoCHelper.ReadLines("09");
        Part1(input);
        Part2(input);
    }

    private void Part1(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var history = new List<List<int>>();
            var lineNumbers = line.Split(" ").Select(int.Parse).ToList();
            history.Add(lineNumbers);
            while (history.Last().Any(i => i != 0))
            {
                lineNumbers = GetDifferences(lineNumbers);
                history.Add(lineNumbers);
            }

            history.Last().Add(0);
            for (var i = history.Count - 1; i >= 1; i--)
            {
                history[i - 1].Add(history[i - 1].Last() + history[i].Last());
            }

            sum += history.First().Last();
        }

        System.Console.WriteLine(sum);
    }
    
    private void Part2(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var history = new List<List<int>>();
            var lineNumbers = line.Split(" ").Select(int.Parse).ToList();
            history.Add(lineNumbers);
            while (history.Last().Any(i => i != 0))
            {
                lineNumbers = GetDifferences(lineNumbers);
                history.Add(lineNumbers);
            }

            foreach (var h in history)
            {
                h.Reverse();
            }
            history.Last().Add(0);
            for (var i = history.Count - 1; i >= 1; i--)
            {
                history[i - 1].Add(history[i - 1].Last() - history[i].Last());
            }

            sum += history.First().Last();
        }

        System.Console.WriteLine(sum);
    }

    public List<int> GetDifferences(List<int> list)
    {
        var differences = new List<int>();
        for (var i = 1; i < list.Count; i++)
        {
            differences.Add(list[i] - list[i - 1]);
        }

        return differences;
    }
}