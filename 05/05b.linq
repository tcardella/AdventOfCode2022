<Query Kind="Statements" />

var stacks = new List<Stack<char>>();

var crates = new[]
{
    new[] { 'W', 'B', 'G', 'Z', 'R', 'D', 'C', 'V' },
    new[] { 'V', 'T', 'S', 'B', 'C', 'F', 'W', 'G' },
    new[] { 'W', 'N', 'S', 'B', 'C' },
    new[] { 'P', 'C', 'V', 'J', 'N', 'M', 'G', 'Q' },
    new[] { 'B', 'H', 'D', 'F', 'L', 'S', 'T' },
    new[] { 'N', 'M', 'W', 'T', 'V', 'J' },
    new[] { 'G', 'T', 'S', 'C', 'L', 'F', 'P' },
    new[] { 'Z', 'D', 'B' },
    new[] { 'W', 'Z', 'N', 'M' }
};

var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");
var inputs = await File.ReadAllLinesAsync(inputPath);

//var crates = new[]{
//	new []{'N', 'Z'},
//	new []{'D', 'C', 'M'},
//	new []{'P'},
//};
//
//var inputs = new[]
//{
//			"    [D]    ",
//			"[N] [C]    ",
//			"[Z] [M] [P]",
//			" 1   2   3 ",
//			"",
//			"move 1 from 2 to 1",
//			"move 3 from 1 to 3",
//			"move 2 from 2 to 1",
//			"move 1 from 1 to 2"
//		};

for (var i = 0; i < crates.Count(); i++)
{
    stacks.Add(new Stack<char>());

    foreach (var crate in crates[i].Reverse()) stacks[i].Push(crate);
}

var movesRegex = new Regex(@"move (?<n>\d+) from (?<source>\d+) to (?<destination>\d+)");

var crane = new Stack<char>();

foreach (var input in inputs.AsEnumerable().Skip(10)) // 5
{
    var match = movesRegex.Match(input);

    var n = int.Parse(match.Groups["n"].Value);
    var source = int.Parse(match.Groups["source"].Value) - 1;
    var destination = int.Parse(match.Groups["destination"].Value) - 1;


    for (var i = 0; i < n; i++)
    {
        var e = stacks[source].Pop();
        crane.Push(e);
    }

    while (crane.TryPop(out var crate))
        stacks[destination].Push(crate);
}

string.Concat(stacks.Select(e => e.Peek())).Dump();