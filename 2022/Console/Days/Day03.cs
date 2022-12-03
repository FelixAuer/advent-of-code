namespace Console.Days;

public class Day03 : IDay
{
    public void Solve()
    {
        var inputs = AoCHelper.ReadLines("03");
        PartOne(inputs);
        PartTwo(inputs);
    }

    private void PartOne(string[] inputs)
    {
        var sum = 0;
        foreach (string rucksackContent in inputs)
        {
            var leftComp = rucksackContent.Substring(0, rucksackContent.Length / 2).ToCharArray();
            var rightComp = rucksackContent.Substring(rucksackContent.Length / 2).ToCharArray();
            var intersection = leftComp.Intersect(rightComp);

            sum += GetPriority(intersection.First());
        }

        System.Console.WriteLine(sum);
    }

    private void PartTwo(string[] inputs)
    {
        var sum = 0;
        for (var i = 0; i < inputs.Length / 3; i++)
        {
            var k = 3 * i;
            var intersection = inputs[k].ToCharArray().Intersect(inputs[k + 1].ToCharArray())
                .Intersect(inputs[k + 2].ToCharArray());

            sum += GetPriority(intersection.First());
        }

        System.Console.WriteLine(sum);
    }

    private int GetPriority(char item)
    {
        if (char.IsLower(item))
        {
            return item - 96;
        }

        return item - 38;
    }
}