<Query Kind="Statements" />

var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");

var biggestTotal = 0;
var runningTotal = 0;

foreach (var i in await File.ReadAllLinesAsync(inputPath))
{
	if (string.IsNullOrWhiteSpace(i)){
		runningTotal = 0;
	}else{
		runningTotal+= int.Parse(i);
	}

	if (biggestTotal < runningTotal)
		biggestTotal = runningTotal;
}

biggestTotal.Dump();