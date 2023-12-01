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

        switch (number)
        {
            case "one": return "1";
            case "two": return "2";
            case "three": return "3";
            case "four": return "4";
            case "five": return "5";
            case "six": return "6";
            case "seven": return "7";
            case "eight": return "8";
            case "nine": return "9";
        }

        return "";
    }
}