using System.Text.RegularExpressions;

namespace Console.Days;

using System.Reflection;
using System;

public class Day01 : IDay
{
    public void Solve()
    {
        var inputs = AoCHelper.ReadLines("01");
        var sum1 = 0;
        var sum2 = 0;
        foreach (var line in inputs)
        {
            sum1 += GetSumOfFirstAndLast(line);
            sum2 += GetSumOfFirstAndLastWritten(line);
        }

        Console.WriteLine("Sum1: " + sum1);
        Console.WriteLine("Sum2: " + sum2);
    }

    private int GetSumOfFirstAndLast(string line)
    {
        var first = Regex.Match(line, "\\D*(\\d).*").Groups[1].ToString();
        var last = Regex.Match(line, ".*(\\d)\\D*").Groups[1].ToString();
        return int.Parse(first + last);
    }

    private int GetSumOfFirstAndLastWritten(string line)
    {
        var first = Regex.Match(line, "(\\d|one|two|three|four|five|six|seven|eight|nine)+?").Groups[1].ToString();
        var last = AoCHelper.Reverse(Regex
            .Match(AoCHelper.Reverse(line), "(\\d|eno|owt|eerht|ruof|evif|xis|neves|thgie|enin)+?").Groups[1]
            .ToString());
        return int.Parse(Normalize(first) + Normalize(last));
    }

    private string Normalize(string number)
    {
        if (int.TryParse(number, out _))
        {
            return number;
        }

        return number switch
        {
            "one" => "1",
            "two" => "2",
            "three" => "3",
            "four" => "4",
            "five" => "5",
            "six" => "6",
            "seven" => "7",
            "eight" => "8",
            "nine" => "9",
            _ => ""
        };
    }
}