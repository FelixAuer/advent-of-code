namespace Console.Days;

using System.Reflection;
using System;

public class Day01 : IDay
{
    public void Solve()
    {
        var inputs = AoCHelper.ReadLines("01");

        var calories = new List<int>();
        var current = 0;

        foreach (var weight in inputs)
        {
            if (weight.Equals(""))
            {
                calories.Add(current);
                current = 0;
                continue;
            }

            current += int.Parse(weight);
        }

        calories.Sort();
        calories.Reverse();
        Console.WriteLine(calories[0]);
        Console.WriteLine(calories[0] + calories[1] + calories[2]);
    }
}