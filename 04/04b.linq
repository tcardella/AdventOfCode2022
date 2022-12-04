<Query Kind="Statements" />

var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");
var inputs = await File.ReadAllLinesAsync(inputPath);

//var inputs = new[]
//{
//    "2-4,6-8",
//    "2-3,4-5",
//    "5-7,7-9",
//    "2-8,3-7",
//    "6-6,4-6",
//    "2-6,4-8"
//};

var regex = new Regex(@"(?<l1>\d+)-(?<r1>\d+),(?<l2>\d+)-(?<r2>\d+)");

var overlappingCount = 0;

foreach (var input in inputs)
{
	var matches = regex.Match(input);

	var l1 = int.Parse(matches.Groups["l1"].Value);
	var r1 = int.Parse(matches.Groups["r1"].Value);
	var l2 = int.Parse(matches.Groups["l2"].Value);
	var r2 = int.Parse(matches.Groups["r2"].Value);
	
	if ((l1 >= l2 && l1 <= r2) || (r1 >= l2 && r1 <= r2) || (l2 >= l1 && l2 <= r1) || (r2 >= l1 && r2 <= r1))
		overlappingCount++;
}

overlappingCount.Dump();