using System.Text.RegularExpressions;

namespace Console.Days;

public class Day08 : IDay
{
    public void Solve()
    {
        var input = AoCHelper.ReadLines("08");
        var instructions = input[0];
        var nodes = new Dictionary<string, Node>();
        for (var i = 2; i < input.Length; i++)
        {
            var id = input[i].Split(" ")[0];
            nodes[id] = new Node() { ID = id };
        }

        for (var i = 2; i < input.Length; i++)
        {
            var id = input[i].Split(" ")[0];
            nodes[id] = new Node() { ID = id };
        }

        for (var i = 2; i < input.Length; i++)
        {
            var ids = Regex.Matches(input[i], "(\\w)+");
            var id = ids[0].ToString();
            var idLeft = ids[1].ToString();
            var idRight = ids[2].ToString();
            nodes[id].Left = nodes[idLeft];
            nodes[id].Right = nodes[idRight];
        }

        var current = nodes["AAA"];
        var steps = 0;
        while (current.ID != "ZZZ")
        {
            var instruction = instructions[steps % instructions.Length];
            current = instruction == 'L' ? current.Left : current.Right;

            steps++;
        }

        System.Console.WriteLine(steps);

        var nodesWithA = nodes.Where(pair => pair.Key[2] == 'A');
        var loopLengths = new List<int>();
        foreach (var startNode in nodesWithA)
        {
            loopLengths.Add(GetLoopLength(startNode.Value, instructions));
        }

        var minLoop = loopLengths.Select(i => (long)i).Aggregate(LeastCommonMultiple);
        System.Console.WriteLine(minLoop);
    }

    private int GetLoopLength(Node startNode, string instructions)
    {
        var current = startNode;
        var steps = 0;
        while (current.ID[2] != 'Z')
        {
            var instruction = instructions[steps % instructions.Length];
            current = instruction == 'L' ? current.Left : current.Right;

            steps++;
        }

        return steps;
    }

    private class Node
    {
        public Node? Left { get; set; }
        public Node? Right { get; set; }
        public string ID { get; set; }
    }

    static long GreatestCommonFactor(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    static long LeastCommonMultiple(long a, long b)
    {
        return (a / GreatestCommonFactor(a, b)) * b;
    }
}