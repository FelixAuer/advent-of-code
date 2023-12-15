namespace Console.Days;

public class Day15 : IDay
{
    public void Solve()
    {
        var input = AoCHelper.ReadLines("15");
        var inputs = input[0].Split(",");
        System.Console.WriteLine(inputs.Sum(Hash));

        var boxes = new Box[256].Select(h => new Box()).ToArray();
        ;
        foreach (var instruction in inputs)
        {
            if (instruction.Contains('-'))
            {
                var label = instruction.Split("-")[0];
                var boxNumber = Hash(label);
                boxes[boxNumber].Lenses.RemoveAll(lens => lens.Label == label);
            }
            else
            {
                var split = instruction.Split("=");
                var label = split[0];
                var focalLength = int.Parse(split[1]);
                var boxNumber = Hash(label);
                var box = boxes[boxNumber];
                if (box.Lenses.Any(lens => lens.Label == label))
                {
                    foreach (var lens in box.Lenses.Where(lens => lens.Label == label))
                    {
                        lens.FocalLength = focalLength;
                    }
                }
                else
                {
                    box.Lenses.Add(new Lens
                    {
                        Label = label,
                        FocalLength = focalLength
                    });
                }
            }
        }

        var sum = 0;
        for (var i = 0; i < boxes.Length; i++)
        {
            for (var j = 0; j < boxes[i].Lenses.Count; j++)
            {
                sum += (i + 1) * (j + 1) * boxes[i].Lenses[j].FocalLength;
            }
        }

        System.Console.WriteLine(sum);
    }

    private int Hash(string input)
    {
        var sum = 0;
        foreach (var c in input.ToCharArray())
        {
            var s = (int)c;
            sum += s;
            sum *= 17;
            sum %= 256;
        }

        return sum;
    }

    private class Box
    {
        public List<Lens> Lenses { get; set; } = new();
    }

    private class Lens
    {
        public string Label { get; set; }
        public int FocalLength { get; set; }
    }
}