namespace Console.Days;

public class Day08 : IDay
{
    public void Solve()
    {
        var lines = AoCHelper.ReadLines("08");
        var lengthY = lines.Length;
        var lengthX = lines[0].Length;
        var trees = new Tree[lines.Length, lines[0].Length];

        for (var i = 0; i < lines.Length; i++)
        {
            var heights = lines[i].ToCharArray();
            for (var j = 0; j < heights.Length; j++)
            {
                trees[i, j] = new Tree((int)char.GetNumericValue(heights[j]));
            }
        }

        PartOne(lengthY, lengthX, trees);
        PartTwo(lengthY, lengthX, trees);
    }

    private static void PartTwo(int lengthY, int lengthX, Tree[,] trees)
    {
        for (var i = 0; i < lengthY; i++)
        {
            for (var j = 0; j < lengthX; j++)
            {
                var tree = trees[i, j];
                var height = tree.Height;
                var visibleUp = 0;
                for (var k = i - 1; k >= 0; k--)
                {
                    visibleUp++;
                    if (trees[k, j].Height >= height)
                    {
                        break;
                    }
                }

                var visibleDown = 0;
                for (var k = i + 1; k < lengthY; k++)
                {
                    visibleDown++;
                    if (trees[k, j].Height >= height)
                    {
                        break;
                    }
                }

                var visibleLeft = 0;
                for (var k = j - 1; k >= 0; k--)
                {
                    visibleLeft++;
                    if (trees[i, k].Height >= height)
                    {
                        break;
                    }
                }

                var visibleRight = 0;
                for (var k = j + 1; k < lengthX; k++)
                {
                    visibleRight++;
                    if (trees[i, k].Height >= height)
                    {
                        break;
                    }
                }

                tree.ScenicScore = visibleUp * visibleDown * visibleLeft * visibleRight;
            }
        }

        System.Console.WriteLine(Tree.Trees.Max(tree => tree.ScenicScore));
    }

    private static void PartOne(int lengthY, int lengthX, Tree[,] trees)
    {
        // check rows of trees first
        for (var i = 0; i < lengthY; i++)
        {
            // sweep row from left to right
            var maxHeight = -1;
            for (var j = 0; j < lengthX; j++)
            {
                var tree = trees[i, j];
                var height = tree.Height;
                if (height > maxHeight)
                {
                    tree.Visible = true;
                    maxHeight = height;
                }
            }

            // sweep row from right to left
            maxHeight = -1;
            for (var j = lengthX - 1; j >= 0; j--)
            {
                var tree = trees[i, j];
                var height = tree.Height;
                if (height > maxHeight)
                {
                    tree.Visible = true;
                    maxHeight = height;
                }
            }
        }

        // check columns
        for (var j = 0; j < lengthX; j++)
        {
            // sweep line from left to right
            var maxHeight = -1;
            for (var i = 0; i < lengthY; i++)
            {
                var tree = trees[i, j];
                var height = tree.Height;
                if (height > maxHeight)
                {
                    tree.Visible = true;
                    maxHeight = height;
                }
            }

            maxHeight = -1;
            for (var i = lengthY - 1; i >= 0; i--)
            {
                var tree = trees[i, j];
                var height = tree.Height;
                if (height > maxHeight)
                {
                    tree.Visible = true;
                    maxHeight = height;
                }
            }
        }

        // draw forest
        for (var i = 0; i < lengthY; i++)
        {
            for (var j = 0; j < lengthX; j++)
            {
                var tree = trees[i, j];
                System.Console.Write(tree.Visible ? "X" : ".");
            }

            System.Console.Write("\n");
        }


        System.Console.WriteLine(Tree.Trees.Count(tree => tree.Visible));
    }
}

public class Tree
{
    public static readonly List<Tree> Trees = new();

    public Tree(int height)
    {
        Height = height;
        Visible = false;
        Trees.Add(this);
    }

    public bool Visible { get; set; }
    public int Height { get; }

    public int ScenicScore { get; set; }
}