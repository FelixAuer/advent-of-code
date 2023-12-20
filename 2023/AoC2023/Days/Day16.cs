namespace Console.Days;

public class Day16 : IDay
{
    public void Solve()
    {
        var input = AoCHelper.ReadLines("16");
        var grid = new Grid(input);
        Part1(grid);
        Part2(grid);
    }

    private static void Part1(Grid grid)
    {
        var initialBeam = new Beam(Direction.Right, (0, 0));
        var energized = GetEnergized(initialBeam, grid);

        System.Console.WriteLine(energized);
    }

    private static void Part2(Grid grid)
    {
        var maxEnergy = 0;
        for (var i = 0; i < grid.Height; i++)
        {
            maxEnergy = Math.Max(maxEnergy, GetEnergized(new Beam(Direction.Right, (i, 0)), grid));
            maxEnergy = Math.Max(maxEnergy, GetEnergized(new Beam(Direction.Left, (i, grid.Width - 1)), grid));
        }

        for (var i = 0; i < grid.Width; i++)
        {
            maxEnergy = Math.Max(maxEnergy, GetEnergized(new Beam(Direction.Down, (0, i)), grid));
            maxEnergy = Math.Max(maxEnergy, GetEnergized(new Beam(Direction.Up, (grid.Height - 1, i)), grid));
        }

        System.Console.WriteLine(maxEnergy);
    }

    private static int GetEnergized(Beam initialBeam, Grid grid)
    {
        Beam._energized = new HashSet<(int x, int y)>();
        Beam._energized2 = new HashSet<(int x, int y, Direction d)>();
        var beams = new Stack<Beam>();
        beams.Push(new Beam(initialBeam.Direction, initialBeam.Position));
        while (beams.Count > 0)
        {
            var beam = beams.Pop();
            var newBeams = grid.Run(beam);
            foreach (var b in newBeams)
            {
                beams.Push(b);
            }
        }

        var energized = Beam._energized.Count(position =>
            position.x >= 0 && position.y >= 0 && position.x < grid.Height && position.y < grid.Width);
        return energized;
    }

    private class Beam
    {
        public static HashSet<(int x, int y)> _energized = new();
        public static HashSet<(int x, int y, Direction d)> _energized2 = new();
        public (int x, int y) Position { get; set; }
        public Direction Direction { get; set; } = Direction.Right;

        public Beam(Direction direction, (int x, int y) position)
        {
            Direction = direction;
            Position = position;
            if (_energized2.Contains((position.x, position.y, direction)))
            {
                Position = (-1, -1);
            }

            _energized.Add(position);
            _energized2.Add((position.x, position.y, direction));
        }

        public (int x, int y) Move(Direction direction)
        {
            return direction switch
            {
                Direction.Right => Position with { y = Position.y + 1 },
                Direction.Down => Position with { x = Position.x + 1 },
                Direction.Left => Position with { y = Position.y - 1 },
                Direction.Up => Position with { x = Position.x - 1 },
            };
        }

        public (int x, int y) Move()
        {
            return Move(Direction);
        }
    }

    private enum Direction
    {
        Right,
        Down,
        Left,
        Up
    }

    private class Grid
    {
        private char[,] Cells { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public Grid(string[] input)
        {
            Height = input.Length;
            Width = input[0].Length;
            Cells = new char[input.Length, input[0].Length];
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    Cells[i, j] = input[i][j];
                }
            }
        }

        public List<Beam> Run(Beam beam)
        {
            if (beam.Position.x < 0 || beam.Position.y < 0 || beam.Position.x >= Height || beam.Position.y >= Width)
            {
                return new List<Beam>();
            }

            var cell = Cells[beam.Position.x, beam.Position.y];
            if (cell == '.')
            {
                return new List<Beam>()
                {
                    new Beam(beam.Direction, beam.Move())
                };
            }

            if (cell == '/')
            {
                switch (beam.Direction)
                {
                    case Direction.Right:
                        return new List<Beam>()
                        {
                            new Beam(Direction.Up, beam.Move(Direction.Up))
                        };
                    case Direction.Down:
                        return new List<Beam>()
                        {
                            new Beam(Direction.Left, beam.Move(Direction.Left))
                        };
                    case Direction.Left:
                        return new List<Beam>()
                        {
                            new Beam(Direction.Down, beam.Move(Direction.Down))
                        };
                    case Direction.Up:
                        return new List<Beam>()
                        {
                            new Beam(Direction.Right, beam.Move(Direction.Right))
                        };
                }
            }

            if (cell == '\\')
            {
                switch (beam.Direction)
                {
                    case Direction.Right:
                        return new List<Beam>()
                        {
                            new Beam(Direction.Down, beam.Move(Direction.Down))
                        };
                    case Direction.Down:
                        return new List<Beam>()
                        {
                            new Beam(Direction.Right, beam.Move(Direction.Right))
                        };
                    case Direction.Left:
                        return new List<Beam>()
                        {
                            new Beam(Direction.Up, beam.Move(Direction.Up))
                        };
                    case Direction.Up:
                        return new List<Beam>()
                        {
                            new Beam(Direction.Left, beam.Move(Direction.Left))
                        };
                }
            }

            if (cell == '-')
            {
                switch (beam.Direction)
                {
                    case Direction.Right:
                    case Direction.Left:
                        return new List<Beam>()
                            {
                                new Beam(beam.Direction, beam.Move())
                            }
                            ;
                    case Direction.Down:
                    case Direction.Up:
                        return new List<Beam>()
                        {
                            new Beam(Direction.Right, beam.Move(Direction.Right)),
                            new Beam(Direction.Left, beam.Move(Direction.Left))
                        };
                }
            }

            if (cell == '|')
            {
                switch (beam.Direction)
                {
                    case Direction.Up:
                    case Direction.Down:
                        return new List<Beam>()
                            {
                                new Beam(beam.Direction, beam.Move())
                            }
                            ;
                    case Direction.Left:
                    case Direction.Right:
                        return new List<Beam>()
                        {
                            new Beam(Direction.Up, beam.Move(Direction.Up)),
                            new Beam(Direction.Down, beam.Move(Direction.Down))
                        };
                }
            }

            return new List<Beam>();
        }
    }
}