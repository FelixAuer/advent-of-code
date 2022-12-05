// See https://aka.ms/new-console-template for more information


using System.Reflection;
using Console.Days;

var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
    @"inputs");
var inputFiles = Directory.GetFiles(path, "*.txt")
    .Select(Path.GetFileNameWithoutExtension)
    .ToArray();
Array.Sort(inputFiles);
var newest = inputFiles.Reverse().First();

System.Console.WriteLine("Day " + newest);

var className = "Console.Days.Day" + newest;
var type = Type.GetType(className);
var day = (IDay)Activator.CreateInstance(type);
day.Solve();