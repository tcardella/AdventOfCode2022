<Query Kind="Program" />

void Main()
{
	var dumpContainer = new DumpContainer();
	dumpContainer.Dump("Monkees");

	var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");
	var inputs = File.ReadAllLines(inputPath);

	var monkees = new List<Monkey>();
	var monkeeIndex = 0;
	Monkey currentMonkee = null;

	foreach (var input in inputs)
	{
		if (input.StartsWith("Monkey "))
		{
			currentMonkee = new Monkey(monkeeIndex++);
			monkees.Add(currentMonkee);
		}

		if (input.StartsWith("  Starting items: "))
		{
			var startingItems = input.Split(':')
						.Skip(1)
						.First()
						.Split(',')
						.Select(e => int.Parse(e));

			foreach (var e in startingItems)
				currentMonkee.Items.Enqueue(e);
		}

		if (input.StartsWith("  Operation: new = "))
		{
			var a = input.Split('=')
			.Skip(1)
			.First()
			.Trim();

			currentMonkee.Operation = a;
		}

		if (input.StartsWith("  Test: divisible by "))
		{
			var x = input.LastIndexOf(' ');
			var y = input.Substring(x);
			currentMonkee.Divisior = int.Parse(y);
		}

		if (input.StartsWith("    If true: throw to monkey "))
		{
			var x = input.LastIndexOf(' ');
			var y = input.Substring(x);
			currentMonkee.ToMonkeyIfTrue = int.Parse(y);
		}

		if (input.StartsWith("    If false: throw to monkey "))
		{
			var x = input.LastIndexOf(' ');
			var y = input.Substring(x);
			currentMonkee.ToMonkeyIfFalse = int.Parse(y);
		}
	}

	dumpContainer.UpdateContent(monkees);

	var lcm = monkees.Select(e => e.Divisior).Aggregate((x, y) => x * y);

	for (int i = 0; i < 10_000; i++)
	{
		foreach (var monkee in monkees)
		{
			while (monkee.Items.TryDequeue(out double item))
			{
				monkee.InspectionCount += 1;

				var regex = new Regex(@"(?<a>old|\d+) (?<op>[*+]) (?<b>old|\d+)");

				var match = regex.Match(monkee.Operation);

				var a = match.Groups["a"].Value == "old" ? item : int.Parse(match.Groups["a"].Value);
				var b = match.Groups["b"].Value == "old" ? item : int.Parse(match.Groups["b"].Value);

				var newItem = match.Groups["op"].Value switch
				{
					"*" => a * b,
					"+" => a + b,
					_ => throw new ArgumentOutOfRangeException()
				};

				newItem = newItem % lcm;

				var toMonkee = newItem % monkee.Divisior == 0
				? monkee.ToMonkeyIfTrue
				: monkee.ToMonkeyIfFalse;

				monkees[toMonkee].Items.Enqueue(newItem);

				//dumpContainer.UpdateContent(monkees);
			}
		}
	}

	dumpContainer.UpdateContent(monkees);

	var temp = monkees.OrderByDescending(m => m.InspectionCount);

	temp.Dump();

	temp.Select(m => m.InspectionCount)
	.Take(2)
	.Select(e => (long)e)
	.Aggregate((x, y) => x * y)
	.Dump("Monkey Business");
}

public class Monkey
{
	public Monkey(int index)
	{
		Index = index;
	}

	public int Index { get; set; }
	public Queue<double> Items { get; set; } = new Queue<double>();
	public string Operation { get; set; }
	public int Divisior { get; set; }
	public int ToMonkeyIfTrue { get; set; }
	public int ToMonkeyIfFalse { get; set; }
	public int InspectionCount { get; set; }
}