using System.Collections;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Console.Days;

public class Day07 : IDay
{
    public void Solve()
    {
        var filesystem = SetupFilesystem();

        PartOne();
        PartTwo(filesystem);
    }

    private static Filesystem SetupFilesystem()
    {
        var inputs = AoCHelper.ReadLines("07");
        var filesystem = new Filesystem();
        var changeCommand = new Regex(@"^\$ cd");
        var dirOutput = new Regex(@"^dir");
        var fileOutput = new Regex(@"^\d");

        foreach (var line in inputs)
        {
            if (changeCommand.IsMatch(line))
            {
                var targetDirectory = line.Split(" ")[2];
                filesystem.ChangeDir(targetDirectory);
            }
            else if (dirOutput.IsMatch(line))
            {
                var dirName = line.Split(" ")[1];
                var dir = new Directory(dirName);
                filesystem.Add(dir);
            }
            else if (fileOutput.IsMatch(line))
            {
                var split = line.Split(" ");
                var file = new File(split[1], int.Parse(split[0]));
                filesystem.Add(file);
            }
        }

        return filesystem;
    }

    private static void PartOne()
    {
        var size = Directory.Directories.Where(directory => directory.Size <= 100000).Sum(directory => directory.Size);

        System.Console.WriteLine(size);
    }

    private static void PartTwo(Filesystem filesystem)
    {
        var totalDiskSpace = 70000000;
        var neededDiskSpace = 30000000;
        var unusedDiskSpace = totalDiskSpace - filesystem.Size();
        var diskSpaceToDelete = neededDiskSpace - unusedDiskSpace;

        var toDelete = Directory.Directories.Where(directory => directory.Size >= diskSpaceToDelete)
            .MinBy(directory => directory.Size);
        System.Console.WriteLine(toDelete.Size);
    }
}

public interface IListable
{
    int Size { get; }
    string Name { get; }
}

public class File : IListable
{
    public File(string name, int size)
    {
        Name = name;
        Size = size;
    }

    public string Name { get; }
    public int Size { get; }
}

public class Directory : IListable
{
    private readonly List<IListable> _contents = new();
    public static readonly List<Directory> Directories = new();

    public string Name { get; }
    public Directory? Parent { get; private set; }

    public Directory(string name)
    {
        Name = name;

        Directories.Add(this);
    }

    public int Size
    {
        get { return _contents.Count == 0 ? 0 : _contents.Sum(listable => listable.Size); }
    }

    public void Add(IListable listable)
    {
        _contents.Add(listable);
        if (listable.GetType() == typeof(Directory))
        {
            ((Directory)listable).Parent = this;
        }
    }

    public Directory GetDirectory(string name)
    {
        foreach (var listable in _contents.Where(listable =>
                     listable.GetType() == typeof(Directory) && listable.Name == name))
        {
            return (Directory)listable;
        }

        throw new Exception("Nothing found");
    }
}

public class Filesystem
{
    private readonly Directory _root;
    private Directory _current;

    public Filesystem()
    {
        _root = new Directory("/");
        _current = _root;
    }

    public void Add(IListable listable)
    {
        _current.Add(listable);
    }

    public void ChangeDir(string name)
    {
        switch (name)
        {
            case "/":
                _current = _root;
                return;
            case "..":
                _current = _current.Parent;
                break;
            default:
                _current = _current.GetDirectory(name);
                break;
        }
    }

    public int Size()
    {
        return _root.Size;
    }
}