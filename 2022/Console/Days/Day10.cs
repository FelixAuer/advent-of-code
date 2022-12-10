namespace Console.Days;

public class Day10 : IDay
{
    public void Solve()
    {
        var instructions = AoCHelper.ReadLines("10");
        var cycleValues = new List<int> { 1 };
        var xReg = 1;

        foreach (string instruction in instructions)
        {
            if (instruction == "noop")
            {
                cycleValues.Add(xReg);
            }
            else
            {
                cycleValues.Add(xReg);
                cycleValues.Add(xReg);
                var split = instruction.Split();
                var toAdd = int.Parse(split[1]);
                xReg += toAdd;
            }
        }

        cycleValues.Add(xReg);

        System.Console.WriteLine(CycleStrength(cycleValues, 20) + CycleStrength(cycleValues, 60) + CycleStrength(
            cycleValues, 100) + CycleStrength(cycleValues, 140) + CycleStrength(cycleValues, 180) + CycleStrength(
            cycleValues, 220));

        for (var row = 0; row < 6; row++)
        {
            for (var pixel = 0; pixel < 40; pixel++)
            {
                var cycle = row * 40 + pixel + 1;
                var posX = cycleValues[cycle];
                System.Console.Write(Math.Abs(pixel - posX) <= 1 ? "#" : ".");
            }

            System.Console.Write("\n");
        }
    }

    private int CycleStrength(List<int> cycleValues, int number)
    {
        return number * cycleValues[number];
    }
}