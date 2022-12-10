<Query Kind="Statements" />

var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");
var inputs = File.ReadAllLines(inputPath);

var currentCycle = 0;
var register = 1;
var spriteStart = 0;
var spriteEnd = 2;
var crt = "";

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

        var index = currentCycle % 40 - 1;

        crt += index >= spriteStart && index <= spriteEnd ? "#" : ".";

        if (currentCycle % 40 == 0)
            crt += Environment.NewLine;
    }

    register += increment;
    spriteStart += increment;
    spriteEnd += increment;
}

crt.Dump("CRT");