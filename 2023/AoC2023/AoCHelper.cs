namespace Console;

using System.Reflection;

public static class AoCHelper
{
    public static string[] ReadLines(string filename, bool test = false)
    {
        var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            (test ? @"tests\" : @"inputs\") + filename + ".txt");
        return File.ReadAllLines(path);
    }

    public static int ToNumber(char c)
    {
        return c - 'a';
    }

    public static string Reverse(string s)
    {
        var charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    public static long GreatestCommonFactor(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    public static long LeastCommonMultiple(long a, long b)
    {
        return (a / GreatestCommonFactor(a, b)) * b;
    }
}