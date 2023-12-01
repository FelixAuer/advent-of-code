namespace Console;

using System.Reflection;

public static class AoCHelper
{
    public static string[] ReadLines(string filename)
    {
        var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            @"inputs\" + filename + ".txt");
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
}