namespace Console.Days;

public class Day20 : IDay
{
    public void Solve()
    {
        var input = AoCHelper.ReadLines("20");

        PartOne(input);
        PartTwo(input);
    }

    private static void PartOne(string[] input)
    {
        var modules = SetupModules(input);

        for (var i = 0; i < 1000; i++)
        {
            var pulses = new Queue<Pulse>();
            pulses.Enqueue(new Pulse(modules["broadcaster"], modules["broadcaster"], PulseType.Low));
            while (pulses.Count > 0)
            {
                var pulse = pulses.Dequeue();
                var newPulses = pulse.Receiver.Pulse(pulse);
                foreach (var p in newPulses)
                {
                    pulses.Enqueue(p);
                }
            }
        }

        System.Console.WriteLine(Pulse.Counter[PulseType.Low]);
        System.Console.WriteLine(Pulse.Counter[PulseType.High]);
        System.Console.WriteLine(Pulse.Counter[PulseType.High] * Pulse.Counter[PulseType.Low]);
    }

    private static void PartTwo(string[] input)
    {
        // rx gets a low pulse if all last input pulses xm have been high
        // xm gets pulses from ft, jz, sv & ng
        // so for rx to get a low pulse xm needs to get high pulses from ft, jz, sv & ng
        // each of these has only 1 input
        // so we need to check how many presses we need for each of them to send a high pulse
        var mods = new[] { "ft", "jz", "sv", "ng" };
        var divs = new List<long>();
        foreach (var m in mods)
        {
            var modules = SetupModules(input);
            long i = 0;
            var hits = new HashSet<long>();
            while (hits.Count < 5)
            {
                i++;
                var pulses = new Queue<Pulse>();
                pulses.Enqueue(new Pulse(modules["broadcaster"], modules["broadcaster"], PulseType.Low));
                while (pulses.Count > 0)
                {
                    var pulse = pulses.Dequeue();
                    var newPulses = pulse.Receiver.Pulse(pulse);
                    foreach (var p in newPulses)
                    {
                        pulses.Enqueue(p);
                        if (p.Sender.Name == m && p.Receiver.Name == "xm" && p.Type == PulseType.High)
                        {
                            hits.Add(i);
                        }
                    }
                }
            }

            divs.Add(hits.ToList()[2] - hits.ToList()[1]);
        }


        System.Console.WriteLine(divs.Select(i => (long)i).Aggregate(AoCHelper.LeastCommonMultiple));
    }

    private static Dictionary<string, Module> SetupModules(string[] input)
    {
        var modules = new Dictionary<string, Module>();
        foreach (var line in input)
        {
            var moduleName = line.Split(" ")[0];
            switch (moduleName[0])
            {
                case '%':
                    moduleName = moduleName.Substring(1);
                    modules[moduleName] = new FlipFlopModule(moduleName);
                    break;
                case '&':
                    moduleName = moduleName.Substring(1);
                    modules[moduleName] = new ConjunctionModule(moduleName);
                    break;
                default:
                    modules[moduleName] = new BroadcasterModule(moduleName);
                    break;
            }
        }

        foreach (var line in input)
        {
            var split = line.Split(" -> ");
            var moduleName = split[0];
            switch (moduleName[0])
            {
                case '%':
                case '&':
                    moduleName = moduleName.Substring(1);
                    break;
            }

            var destinations = split[1].Split(", ");
            foreach (var destination in destinations)
            {
                if (modules.TryGetValue(destination, out Module value))
                {
                    modules[moduleName].AddDestination(value);
                }
                else
                {
                    modules[destination] = new OutputModule(destination);
                    modules[moduleName].AddDestination(modules[destination]);
                }
            }
        }

        return modules;
    }

    private class Pulse
    {
        public static readonly Dictionary<PulseType, int> Counter = new Dictionary<PulseType, int>()
        {
            { PulseType.Low, 0 },
            { PulseType.High, 0 }
        };

        public Module Sender;
        public Module Receiver;

        public Pulse(Module sender, Module receiver, PulseType type)
        {
            Sender = sender;
            Receiver = receiver;
            Type = type;
            Counter[type]++;
        }

        public PulseType Type { get; set; }
    }

    private enum PulseType
    {
        Low,
        High
    }

    private abstract class Module
    {
        public string Name { get; set; }
        public abstract List<Pulse> Pulse(Pulse pulse);
        protected readonly List<Module> _destinations = new();
        public readonly Dictionary<string, PulseType> Inputs = new();

        protected Module(string name)
        {
            Name = name;
        }

        protected Module()
        {
            throw new NotImplementedException();
        }

        public void AddDestination(Module module)
        {
            _destinations.Add(module);
            module.AddInput(this);
        }

        private void AddInput(Module module)
        {
            Inputs[module.Name] = PulseType.Low;
        }
    }

    private class BroadcasterModule : Module
    {
        public BroadcasterModule(string name) : base(name)
        {
        }

        public override List<Pulse> Pulse(Pulse pulse)
        {
            return _destinations.Select(destination => new Pulse(this, destination, pulse.Type)).ToList();
        }
    }


    private class FlipFlopModule : Module
    {
        private bool TurnedOn;

        public FlipFlopModule(string name) : base(name)
        {
        }

        public override List<Pulse> Pulse(Pulse pulse)
        {
            if (pulse.Type == PulseType.High)
            {
                return new List<Pulse>();
            }

            TurnedOn = !TurnedOn;

            return _destinations
                .Select(destination => new Pulse(this, destination, TurnedOn ? PulseType.High : PulseType.Low))
                .ToList();
        }
    }

    private class ConjunctionModule : Module
    {
        public ConjunctionModule(string name) : base(name)
        {
        }

        public override List<Pulse> Pulse(Pulse pulse)
        {
            Inputs[pulse.Sender.Name] = pulse.Type;

            var allHigh = Inputs.Values.All(type => type == PulseType.High);
            var output = allHigh ? PulseType.Low : PulseType.High;

            return _destinations
                .Select(destination => new Pulse(this, destination, output))
                .ToList();
        }
    }

    private class OutputModule : Module
    {
        public OutputModule(string name) : base(name)
        {
        }

        public override List<Pulse> Pulse(Pulse pulse)
        {
            return new List<Pulse>();
        }
    }
}