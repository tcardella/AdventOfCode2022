<Query Kind="Statements" />

var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");

List<int> caloriesPerElf = new List<int>();
var runningTotal = 0;

foreach (var i in await File.ReadAllLinesAsync(inputPath))
{
	if (string.IsNullOrWhiteSpace(i))
	{
		caloriesPerElf.Add(runningTotal);
		runningTotal = 0;
	}
	else
	{
		runningTotal += int.Parse(i);
	}
}

caloriesPerElf.Add(runningTotal);

caloriesPerElf.OrderByDescending(e => e)
	.Take(3)
	.Sum()
	.Dump();
