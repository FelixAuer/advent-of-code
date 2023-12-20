using System.Text;

namespace Console.Days;

public class Day12 : IDay
{
    private Dictionary<(string rest, string blocks), long> _memo = new();
    private int mult = 5;

    public void Solve()
    {
         var input = AoCHelper.ReadLines("12");
       // var input = new[] { "?###???????? 3,2,1" };
        long sum = 0;
        foreach (var s in input)
        {
            var split = s.Split(" ");
            var goal = Repeat(split[0], mult);
            var blocks = Repeat(split[1].Split(",").Select(int.Parse)
                .ToList(), mult);
            var x = Step(blocks, goal);
            System.Console.WriteLine(s + " " + x);
            sum += x;
        }

        System.Console.WriteLine(sum);
    }

    private List<int> Repeat(List<int> list, int repetitions)
    {
        if (repetitions == 1)
        {
            return list;
        }

        return list.Concat(Repeat(list, repetitions - 1)).ToList();
    }

    private string Repeat(string s, int repetitions)
    {
        if (repetitions == 1)
        {
            return s;
        }

        return s + "?" + Repeat(s, repetitions - 1);
    }

    private long Step(List<int> blocks, string goal, string current = "")
    {
        for (var i = 0; i < current.Length; i++)
        {
            if (goal[i] != '?' && goal[i] != current[i])
            {
                return 0;
            }
        }

        if (goal.Length == current.Length && blocks.Count == 0)
        {
            return 1;
        }

        // if current.Length + minimal length for blocks > goal.Length return 0
        if (current.Length + blocks.Sum() + blocks.Count - 1 > goal.Length)
        {
            return 0;
        }

        var rest = goal.Substring(current.Length);
        if (_memo.TryGetValue((rest, string.Join(",", blocks)), out long value))
        {
            return value;
        }

        var v = Step(blocks, goal, current + ".") +
                Step(blocks.Skip(1).ToList(),
                    goal,
                    current + new string('#', blocks[0]) + (blocks.Count > 1
                        ? "."
                        : new string('.', goal.Length - (current.Length + blocks[0])))
                );
        _memo[(rest, string.Join(",", blocks))] = v;
        return v;
    }
}