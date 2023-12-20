namespace Console.Days;

public class Day13 : IDay
{
    public void Solve()
    {
        var input = AoCHelper.ReadLines("13", false);
        var patterns = new List<List<string>>();
        patterns.Add(new List<string>());
        foreach (var line in input)
        {
            if (line.Length > 0)
            {
                patterns.Last().Add(line);
            }
            else
            {
                patterns.Add(new List<string>());
            }
        }

        var sum1 = 0;
        var sum2 = 0;

        foreach (var pattern in patterns)
        {
            for (var i = 1; i < pattern[0].Length; i++)
            {
                var errors = 0;
                for (var j = 0; j < Math.Min(i, pattern[0].Length - i); j++)
                {
                    foreach (var line in pattern)
                    {
                        if (line[i - j - 1] != line[i + j])
                        {
                            errors++;
                        }
                    }
                }

                if (errors == 0)
                {
                    sum1 += i;
                }

                if (errors == 1)
                {
                    sum2 += i;
                }
            }

            for (var i = 1; i < pattern.Count; i++)
            {
                var errors = 0;
                for (var j = 0; j < Math.Min(i, pattern.Count - i); j++)
                {
                    for (var k = 0; k < pattern[0].Length; k++)
                    {
                        if (pattern[i - j - 1][k] != pattern[i + j][k])
                        {
                            errors++;
                        }
                    }
                }

                if (errors == 0)
                {
                    sum1 += 100 * i;
                }

                if (errors == 1)
                {
                    sum2 += 100 * i;
                }
            }
        }

        System.Console.WriteLine(sum1);
        System.Console.WriteLine(sum2);
    }
}