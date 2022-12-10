<Query Kind="Program">
  <Namespace>System.Numerics</Namespace>
</Query>

DumpContainer dumpContainer = new DumpContainer();
Vector2[] knots = Enumerable.Range(0, 10).Select(e => new Vector2(0, 0)).ToArray();
HashSet<string> tailVisits = new HashSet<string>();

void Main()
{
	var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");
	var inputs = File.ReadAllLines(inputPath);

	dumpContainer.Dump("Knots");

	tailVisits.Add(knots.Last().ToString());

	foreach (var input in inputs)
	{
		var parts = input.Split(' ');
		var direction = parts[0];
		var amount = int.Parse(parts[1]);

		for (int i = 0; i < amount; i++)
		{
			var vector = direction switch
			{
				"R" => new Vector2(1, 0),
				"U" => new Vector2(0, 1),
				"L" => new Vector2(-1, 0),
				"D" => new Vector2(0, -1),
				_ => throw new ArgumentOutOfRangeException()
			};

			knots[0] = Vector2.Add(knots[0], vector);

			DumpKnots();

			for (int j = 1; j < knots.Length; j++)
			{
				MoveKnot(j);

				DumpKnots();
			}

			DumpKnots();

			var l = 0;
		}
	}

	tailVisits.Count().Dump("Tail Visits");
}

void MoveKnot(int j)
{
	var previous = knots[j - 1];
	var current = knots[j];

	var distance = Math.Abs(previous.X - current.X) + Math.Abs(previous.Y - current.Y);
	var sameColumnOrRow = previous.X == current.X || previous.Y == current.Y;

	if (distance == 2 && sameColumnOrRow)
	{
		knots[j] += Vector2.Normalize(previous - current);
	}

	if (distance == 1 && !sameColumnOrRow)
		knots[j] += Vector2.Clamp(previous - current, new(-1, -1), new(1, 1));

	if (j == knots.Length - 1)
		tailVisits.Add(knots[j].ToString());
}

void DumpKnots()
{
	var output = new object[knots.Length];
	output[0] = new { Knot = knots[0].ToString(), DirectionalVector = new Vector2().ToString() };
	for (int i = 1; i < knots.Length; i++)
	{
		var directionalVector = knots[i - 1].ToDirectionalVector(knots[i]);
		output[i] = new { Knot = knots[i].ToString(), DirectionalVector = directionalVector.ToString() };
	}

	dumpContainer.UpdateContent(output);
}

public class Point
{
	public double X { get; set; }
	public double Y { get; set; }

	public Point(double x, double y)
	{
		X = x;
		Y = y;
	}

	public Point() : this(0, 0) { }

	public override string ToString()
	{
		return $"{X}, {Y}";
	}

	public static Point operator +(Point point, Vector vector)
	{
		return new Point(point.X + vector.X, point.Y + vector.Y);
	}
}

public class Vector
{
	public double X { get; set; }
	public double Y { get; set; }

	public Vector(double x, double y)
	{
		X = x;
		Y = y;
	}

	public Vector() : this(0, 0)
	{
	}

	public override string ToString()
	{
		return $"{X}, {Y}";
	}

	public static Vector operator +(Vector v1, Vector v2)
	{
		return new Vector(v1.X + v2.X, v1.Y + v2.Y);
	}
}

public static class Extensions
{
	public static Vector2 ToDirectionalVector(this Vector2 initialPoint, Vector2 terminalPoint)
	{
		return new Vector2(initialPoint.X - terminalPoint.X, initialPoint.Y - terminalPoint.Y);
	}
}