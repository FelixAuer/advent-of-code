namespace Console.Days;

public class Day02 : IDay
{
    public void Solve()
    {
        var inputs = AoCHelper.ReadLines("02");

        partOne(inputs);
        partTwo(inputs);
    }

    private static void partOne(IEnumerable<string> inputs)
    {
        var scores = new Dictionary<string, int>
        {
            { "A X", 4 }, // rock vs rock
            { "A Y", 8 }, // rock vs paper
            { "A Z", 3 }, // rock vs scissors
            { "B X", 1 }, // paper vs rock
            { "B Y", 5 }, // paper vs paper
            { "B Z", 9 }, // paper vs scissors
            { "C X", 7 }, // scissors vs rock
            { "C Y", 2 }, // scissors vs paper
            { "C Z", 6 } // scissors vs scissors
        };

        var score = inputs.Sum(match => scores[match]);
        System.Console.WriteLine(score);
    }

    private static void partTwo(IEnumerable<string> inputs)
    {
        var scores = new Dictionary<string, int>
        {
            { "A X", 3 }, // lose vs rock => scissors
            { "A Y", 4 }, // draw vs rock => rock
            { "A Z", 8 }, // win vs rock => paper
            { "B X", 1 }, // lose vs paper => rock
            { "B Y", 5 }, // draw vs paper => paper
            { "B Z", 9 }, // win vs paper => scissors
            { "C X", 2 }, // lose vs scissors => rock
            { "C Y", 6 }, // draw vs scissors => scissors
            { "C Z", 7 } // win vs scissors => rock
        };

        var score = inputs.Sum(match => scores[match]);
        System.Console.WriteLine(score);
    }
}