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
}