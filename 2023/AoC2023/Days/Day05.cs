using System.Text.RegularExpressions;

namespace Console.Days;

public class Day05 : IDay
{
    public void Solve()
    {
        var input = AoCHelper.ReadLines("05");
        var seeds = Regex.Matches(input[0], "(\\d+)").Select(match => long.Parse(match.ToString()));

        input = input.Skip(2).ToArray();
        var maps = input.Aggregate(new List<List<long[]>> { new List<long[]>() },
            (list, value) =>
            {
                if (!value.Contains("map"))
                {
                    list.Last().Add((long[])Regex.Matches(value, "(\\d+)").Select(match => long.Parse(match.ToString()))
                        .ToArray());
                }

                if (value.Length == 0)
                {
                    list.Add(new List<long[]>());
                }

                return list;
            });

        Part1(seeds, maps);
        Part2(seeds.ToList(), maps);
    }

    private void Part2(List<long> seeds, List<List<long[]>> maps)
    {
        var ranges = new List<(long left, long right)>();
        for (var i = 0; i < seeds.Count; i += 2)
        {
            ranges.Add((left: seeds[i], right: seeds[i] + seeds[i + 1] - 1));
        }

        foreach (var map in maps)
        {
            var ordered = map.Where(x => x.Length > 0).OrderBy(x => x[1]).ToList();

            var newRanges = new List<(long left, long right)>();
            foreach (var range in ranges)
            {
                var r = range;
                foreach (var mapping in ordered)
                {
                    if (r.left < mapping[1])
                    {
                        newRanges.Add((r.left, Math.Min(r.right, mapping[1] - 1)));
                        r.left = mapping[1];
                        if (r.left > r.right)
                            break;
                    }

                    if (r.left <= mapping[1] + mapping[2] - 1)
                    {
                        newRanges.Add((r.left + mapping[0] - mapping[1],
                            Math.Min(r.right, mapping[1] + mapping[2] - 1) + mapping[0] - mapping[1]));
                        r.left = mapping[1] + mapping[2] - 1 + 1;
                        if (r.left > r.right)
                            break;
                    }
                }

                if (r.left <= r.right)
                {
                    newRanges.Add(r);
                }
            }

            ranges = newRanges;
        }

        System.Console.WriteLine(ranges.MinBy(x => x.left).left);
    }

    private void Part1(IEnumerable<long> seeds, List<List<long[]>> maps)
    {
        var locations = new List<long>();
        foreach (var seed in seeds)
        {
            var value = seed;
            foreach (var map in maps)
            {
                value = Map(map, value);
            }

            locations.Add(value);
        }

        System.Console.WriteLine(locations.Min());
    }

    private long Map(List<long[]> map, long input)
    {
        for (var i = 0; i < map.Count; i++)
        {
            if (map[i].Length == 0) continue;
            if (input >= map[i][1] && input < map[i][1] + map[i][2])
            {
                return input + (map[i][0] - map[i][1]);
            }
        }

        return input;
    }
}