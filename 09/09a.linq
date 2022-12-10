<Query Kind="Program" />

DumpContainer dumpContainer = new DumpContainer();
Point[] knots = Enumerable.Range(0, 2).Select(e => new Point(0, 0)).ToArray();

void Main()
{
    var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");
    var inputs = File.ReadAllLines(inputPath);

    dumpContainer.Dump("Knots");

    var tailVisits = new HashSet<string>();
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
                "R" => new Vector(1, 0),
                "U" => new Vector(0, 1),
                "L" => new Vector(-1, 0),
                "D" => new Vector(0, -1),
                _ => throw new ArgumentOutOfRangeException()
            };

            knots[0] = knots[0] + vector;

            DumpKnots();

            for (int j = 1; j < knots.Length; j++)
            {
                var directionalVector = knots[j - 1].ToDirectionalVector(knots[j]);
                
                if (Math.Abs(directionalVector.X) == 2 || Math.Abs(directionalVector.Y) == 2)
                {
                    var correctionVector = new Vector();

                    if (direction is "L" or "R")
                    {
                        if (knots[j - 1].Y != knots[j].Y)
                        {
                            correctionVector = new Vector(0, knots[j - 1].Y - knots[j].Y);
                        }
                    }
                    else
                    {
                        if (knots[j - 1].X != knots[j].X)
                        {
                            correctionVector = new Vector(knots[j - 1].X - knots[j].X, 0);
                        }
                    }
                    
                    knots[j] += vector + correctionVector;

                    if (j == knots.Length - 1)
                        tailVisits.Add(knots[j].ToString());
                }

                DumpKnots();
            }
        }
    }

    tailVisits.Count().Dump("Tail Visits");
}

public void DumpKnots()
{
    var output = new object[knots.Length];
    output[0] = new { Knot = knots[0].ToString(), DirectionalVector = new Vector().ToString() };
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
	
	public Point():this(0,0){}

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
	public static Vector ToDirectionalVector(this Point initialPoint, Point terminalPoint)
	{
		return new Vector(initialPoint.X - terminalPoint.X, initialPoint.Y - terminalPoint.Y);
	}
}