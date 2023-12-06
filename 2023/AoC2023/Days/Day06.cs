namespace Console.Days;

public class Day06 : IDay
{
    public void Solve()
    {
        List<(int time, int maxDistance)> races = new()
        {
            (42, 308),
            (89, 1170),
            (91, 1291),
            (89, 1467)
        };
        var sum1 = races.Select(CountWins).Aggregate((a, x) => a * x);
        System.Console.WriteLine(sum1);

        // Part 2: Simple Maths
    }

    public int CountWins((int time, int maxDistance) race)
    {
        var count = 0;
        for (var i = 0; i < race.time; i++)
        {
            var travelTime = race.time - i;
            if ((travelTime * i) > race.maxDistance)
            {
                count++;
            }
        }

        return count;
    }
}