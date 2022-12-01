// See https://aka.ms/new-console-template for more information

using System.Reflection;

string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"inputs\01.txt");
string[] inputs = File.ReadAllLines(path);

List<int> calories = new List<int>();
int current = 0;

foreach (string weight in inputs)
{
    if (weight.Equals(""))
    {
        calories.Add(current);
        current = 0;
        continue;
    }
    current += Int32.Parse(weight);
    
}
calories.Sort();
calories.Reverse();
Console.WriteLine(calories[0]);
Console.WriteLine(calories[0]+calories[1]+calories[2]);