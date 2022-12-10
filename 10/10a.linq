<Query Kind="Statements" />

var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");
var inputs = File.ReadAllLines(inputPath);

var currentCycle = 0;
var register = 1;
var sampleCycles = new[] { 20, 60, 100, 140, 180, 220 };
var signalStrengths = 0;

foreach (var input in inputs)
{
    var cycles = 0;
    var increment = 0;

    if (input == "noop")
    {
        cycles = 1;
        increment = 0;
    }
    else if (input.StartsWith("addx"))
    {
        cycles = 2;
        increment = int.Parse(input.Split(' ')[1]);
    }

    for (var i = 0; i < cycles; i++)
    {
        currentCycle++;

        if (sampleCycles.Contains(currentCycle)) signalStrengths += currentCycle * register;
    }

    register += increment;
}

signalStrengths.Dump("Signal Strength Sum");