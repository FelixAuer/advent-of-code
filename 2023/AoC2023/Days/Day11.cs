namespace Console.Days;

public class Day11 : IDay
{
    public void Solve()
    {
        var input = AoCHelper.ReadLines("11", false);
        var galaxies = new List<(int x, int y)>();
        var rowsWithGalaxies = new HashSet<int>();
        var colsWithGalaxies = new HashSet<int>();
        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[0].Length; j++)
            {
                if (input[i][j] == '#')
                {
                    galaxies.Add((i, j));
                    rowsWithGalaxies.Add(i);
                    colsWithGalaxies.Add(j);
                }
            }
        }

        long sum = 0;
        for (var i = 0; i < galaxies.Count - 1; i++)
        {
            var startGalaxy = galaxies[i];
            for (var j = i + 1; j < galaxies.Count; j++)
            {
                long diff = 0;
                var targetGalaxy = galaxies[j];
                for (var x = startGalaxy.x; x != targetGalaxy.x; x += Math.Sign(targetGalaxy.x - startGalaxy.x))
                {
                    if (rowsWithGalaxies.Contains(x))
                    {
                        diff++;
                    }
                    else
                    {
                        //diff += 2;
                        diff += 1000000;
                    }
                }

                for (var y = startGalaxy.y; y != targetGalaxy.y; y += Math.Sign(targetGalaxy.y - startGalaxy.y))
                {
                    if (colsWithGalaxies.Contains(y))
                    {
                        diff++;
                    }
                    else
                    {
                        //diff += 2;
                        diff += 1000000;
                    }
                }

                sum += diff;
            }
        }

        System.Console.WriteLine(sum);
    }
}