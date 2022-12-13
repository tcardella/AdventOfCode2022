<Query Kind="Program" />

// I apologize in advance for checking in this atrocity/war crime of an entry for AoC, but it works.

void Main()
{
    var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");
    var inputs = File.ReadAllLines(inputPath);
    var rows = inputs.Select(e => e.ToArray()).ToList();
    var map = new char[rows.Count(), rows[0].Count()];

    var start = new Tile();
    var end = new Tile();
    var fewestSteps = 0;

    for (var y = 0; y < rows.Count(); y++)
    for (var x = 0; x < rows[y].Count(); x++)
    {
        var temp = rows[y][x];
        map[y, x] = temp;

        if (temp == 'S')
            start = new Tile(x, y);

        if (temp == 'E')
            end = new Tile(x, y);
    }

    start.SetDistance(end.X, end.Y);

    var activeTiles = new List<Tile> { start };
    var visitedTiles = new List<Tile>();

    while (activeTiles.Any())
    {
        var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

        if (checkTile.X == end.X && checkTile.Y == end.Y)
        {
            var tile = checkTile;

            while (true)
            {
                fewestSteps++;

                tile = tile.Parent;

                if (tile == null)
                {
                    (fewestSteps-1).Dump("Fewest Steps"); // HACK: Stupid off by one error
                    return;
                }
            }
        }

        visitedTiles.Add(checkTile);
        activeTiles.Remove(checkTile);

        var walkableTiles = GetWalkableTiles(map, checkTile, end);

        foreach (var walkableTile in walkableTiles)
        {
            if (visitedTiles.Any(e => e.X == walkableTile.X && e.Y == walkableTile.Y))
                continue;

            if (activeTiles.Any(e => e.X == walkableTile.X && e.Y == walkableTile.Y))
            {
                var existingTile = activeTiles.First(e => e.X == walkableTile.X && e.Y == walkableTile.Y);
                if (existingTile.CostDistance > checkTile.CostDistance)
                {
                    activeTiles.Remove(existingTile);
                    activeTiles.Add(walkableTile);
                }
            }
            else
            {
                activeTiles.Add(walkableTile);
            }
        }
    }
}

private static List<Tile> GetWalkableTiles(char[,] map, Tile currentTile, Tile targetTile)
{
    var possibleTiles = new List<Tile>
    {
        new(currentTile.X, currentTile.Y - 1, currentTile, currentTile.Cost + 1),
        new(currentTile.X, currentTile.Y + 1, currentTile, currentTile.Cost + 1),
        new(currentTile.X - 1, currentTile.Y, currentTile, currentTile.Cost + 1),
        new(currentTile.X + 1, currentTile.Y, currentTile, currentTile.Cost + 1)
    };

    possibleTiles.ForEach(t => t.SetDistance(targetTile.X, targetTile.Y));

    var maxX = map.GetLength(1) - 1;
    var maxY = map.GetLength(0) - 1;

    return possibleTiles
        .Where(t => t.X >= 0 && t.X <= maxX)
        .Where(t => t.Y >= 0 && t.Y <= maxY)
        .Where(t =>
        {
            var next = map[t.Y, t.X];

            var elevation = next switch
            {
                'S' => 'a',
                'E' => 'z',
                _ => next
            };

            var currentElevation = map[currentTile.Y, currentTile.X] switch
            {
                'S' => 'a',
                'E' => 'z',
                _ => map[currentTile.Y, currentTile.X]
            };

            return elevation <= currentElevation + 1;
        })
        .ToList();
}

public class Tile
{
    public Tile(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Tile()
    {
    }

    public Tile(int x, int y, Tile parentTile, int cost) : this(x, y)
    {
        X = x;
        Y = y;
        Parent = parentTile;
        Cost = cost;
    }

    public int X { get; }
    public int Y { get; }
    public int Cost { get; }
    public int Distance { get; set; }

    public int CostDistance => Cost + Distance;

    public Tile Parent { get; }

	public void SetDistance(int targetX, int targetY)
	{
		Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
	}
}