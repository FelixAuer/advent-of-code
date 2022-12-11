using System.Numerics;

namespace Console.Days;

public class Day11 : IDay
{
    public void Solve()
    {
        var lines = AoCHelper.ReadLines("11");
        for (var i = 0; i <= lines.Length / 7; i++)
        {
            var startingLine = i * 7;
            var operationLine = lines[startingLine + 2].Split(" ");
            var operation = operationLine[^2];
            var adjustment = operationLine[^1];
            var divisor = int.Parse(lines[startingLine + 3].Split(" ")[^1]);
            var catcherTrue = int.Parse(lines[startingLine + 4].Split(" ")[^1]);
            var catcherFalse = int.Parse(lines[startingLine + 5].Split(" ")[^1]);

            var monkey = new Monkey(divisor, catcherTrue, catcherFalse, operation, adjustment);

            var worryLevels = lines[startingLine + 1].Split(": ")[1].Split(", ");
            foreach (var worryLevel in worryLevels)
            {
                monkey.Catch(new Item(int.Parse(worryLevel)));
            }
        }

        for (var i = 0; i < 10000; i++)
        {
            foreach (var monkey in Monkey.Monkeys)
            {
                monkey.Value.Action();
            }
        }
        
        foreach (var monkey in Monkey.Monkeys)
        {
            System.Console.WriteLine(monkey.Value.inspections);
        }
    }
}

public class Item
{
    public Item(int worryLevel)
    {
        WorryLevel = worryLevel;
    }

    public BigInteger WorryLevel { get; set; }

    public void DecreaseWorry()
    {
        var modulo = 5 * 17 * 2 * 7 * 3 * 11 * 13 * 19;
       // var modulo = 23 * 19 * 13 * 17;
        WorryLevel %= modulo;
    }

    public void AdjustWorry(string operation, string adjustment)
    {
        var value = adjustment == "old" ? WorryLevel : int.Parse(adjustment);
        if (operation == "+")
        {
            WorryLevel += value;
        }
        else
        {
            WorryLevel *= value;
        }
    }
}

public class Monkey
{
    public static readonly Dictionary<int, Monkey> Monkeys = new();
    private static int id;

    public Monkey(int divisor, int catcherIdTrue, int catcherIdFalse, string operation, string adjustment)
    {
        ID = id++;
        this.divisor = divisor;
        this.catcherIdTrue = catcherIdTrue;
        this.catcherIdFalse = catcherIdFalse;
        this.operation = operation;
        this.adjustment = adjustment;
        Monkeys.Add(ID, this);
    }

    private int ID { get; }
    private int divisor;
    private int catcherIdTrue;
    private int catcherIdFalse;
    private string operation;
    private string adjustment;
    private Queue<Item> items = new();
    public int inspections = 0;

    public void Action()
    {
        while (items.Count != 0)
        {
            var item = items.Dequeue();
            item.AdjustWorry(operation, adjustment);
            inspections++;

            item.DecreaseWorry();


            var result = (item.WorryLevel % divisor) == 0;
            throwTo(item, result ? catcherIdTrue : catcherIdFalse);
        }
    }


    private void throwTo(Item item, int id)
    {
        var catcher = Monkeys[id];
        catcher.Catch(item);
    }

    public void Catch(Item item)
    {
        items.Enqueue(item);
    }
}