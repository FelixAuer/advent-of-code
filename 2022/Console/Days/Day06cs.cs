using System.Text.RegularExpressions;

namespace Console.Days;

public class Day06 : IDay
{
    public void Solve()
    {
        var inputs = AoCHelper.ReadLines("06")[0];


        CheckSignal(inputs, 4);
        CheckSignal(inputs, 14);
    }

    private static void CheckSignal(string inputs, int length)
    {
        var dict = new Dictionary<char, int>();
        for (var c = 'a'; c <= 'z'; c++)
        {
            dict.Add(c, 0);
        }

        var signals = inputs.ToCharArray();
        for (var i = 0; i < length; i++)
        {
            dict[signals[i]]++;
        }

        for (var right = length; right < signals.Length; right++)
        {
            var left = right - length;
            dict[signals[left]]--;
            dict[signals[right]]++;
            if (dict.Count(count => count.Value > 1) == 0)
            {
                System.Console.WriteLine(right + 1);
                break;
            }
        }
    }
}