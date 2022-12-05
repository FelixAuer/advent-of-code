using System.Collections;
using System.Text.RegularExpressions;

namespace Console.Days;

public class Day05 : IDay
{
    public void Solve()
    {
        var (stacks, instructions) = ParseInput();
        PartOne(instructions, stacks);

        // read again because i'm lazy
        (stacks, instructions) = ParseInput();
        PartTwo(instructions, stacks);
    }

    private static void PartOne(string[] instructions, ArrayList[] stacks)
    {
        foreach (var instruction in instructions)
        {
            var regex = new Regex(@"move (\d+) from (\d+) to (\d+)");
            var groups = regex.Match(instruction).Groups;
            var amount = int.Parse(groups[1].Value);
            var from = int.Parse(groups[2].Value) - 1;
            var to = int.Parse(groups[3].Value) - 1;
            for (var i = 0; i < amount; i++)
            {
                var element = stacks[from][stacks[from].Count - 1];
                stacks[from].RemoveAt(stacks[from].Count - 1);
                stacks[to].Add(element);
            }
        }

        var output = stacks.Aggregate("", (current, stack) => current + stack[^1]);
        System.Console.WriteLine(output);
    }

    private static void PartTwo(string[] instructions, ArrayList[] stacks)
    {
        foreach (var instruction in instructions)
        {
            var regex = new Regex(@"move (\d+) from (\d+) to (\d+)");
            var groups = regex.Match(instruction).Groups;
            var amount = int.Parse(groups[1].Value);
            var from = int.Parse(groups[2].Value) - 1;
            var to = int.Parse(groups[3].Value) - 1;
            var elements = stacks[from].GetRange(stacks[from].Count - amount, amount).ToArray();
            stacks[from].RemoveRange(stacks[from].Count - amount, amount);
            stacks[to].AddRange(elements);
        }

        var output = stacks.Aggregate("", (current, stack) => current + stack[^1]);
        System.Console.WriteLine(output);
    }

    private static (ArrayList[] stacksTransposed, string[] instructions) ParseInput()
    {
        var inputs = AoCHelper.ReadLines("05");

        int splitIndex = -1;
        for (var i = 0; i < inputs.Length; i++)
        {
            if (inputs[i] == "")
            {
                splitIndex = i;
                break;
            }
        }

        var stacksInput = new string[splitIndex - 1];
        Array.Copy(inputs, 0, stacksInput, 0, splitIndex - 1);

        var stacks = new IEnumerable<char>[stacksInput.Length];
        for (var i = 0; i < stacksInput.Length; i++)
        {
            stacks[i] = stacksInput[i].Replace("    ", " [-]").Replace("[", "").Replace("]", "").ToCharArray()
                .Where((item, index) => index % 2 == 0);
        }

        var stacksTransposed = new ArrayList[stacks[0].Count()];
        for (var i = 0; i < stacksTransposed.Length; i++)
        {
            stacksTransposed[i] = new ArrayList();
        }

        foreach (var line in stacks)
        {
            for (var i = 0; i < line.Count(); i++)
            {
                var element = line.ElementAt(i);
                if (!element.Equals('-'))
                {
                    stacksTransposed[i].Add(element);
                }

                line.GetEnumerator().MoveNext();
            }
        }

        foreach (var t in stacksTransposed)
        {
            t.Reverse();
        }

        var instructions = new string[inputs.Length - splitIndex - 1];
        Array.Copy(inputs, splitIndex + 1, instructions, 0, (inputs.Length - splitIndex) - 1);
        return (stacksTransposed, instructions);
    }
}