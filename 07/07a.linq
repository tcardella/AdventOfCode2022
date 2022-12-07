<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

    private void Main()
    {
        var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");
        var inputs = File.ReadAllLines(inputPath);

        var nodes = new List<Node>();
        Node currentNode = null;
        var fileRegex = new Regex(@"(?<size>\d+) .+");

        foreach (var input in inputs)
            if (input is "$ cd.." or "$ cd ..")
            {
                var size = currentNode.Size;
                currentNode = currentNode.Parent;
                currentNode.Size += size;
            }
            else if (input.StartsWith("$ cd "))
            {
                if (currentNode == null)
                {
                    currentNode = new Node(input.Substring(5));
                    nodes.Add(currentNode);
                }
                else
                {
                    var temp = new Node(currentNode, input.Substring(5));
                    currentNode.Children.Add(temp);
                    currentNode = temp;
                }
            }
            else if (fileRegex.IsMatch(input))
            {
                var size = int.Parse(fileRegex.Match(input).Groups["size"].Value);
                currentNode.Size += size;
            }

        while (currentNode.Parent != null)
        {
            var size = currentNode.Size;
            currentNode = currentNode.Parent;
            currentNode.Size += size;
        }

        ExtractDirectorySizes(nodes);

        directorySizes.Where(e => e <= 100_000)
            .Sum()
            .Dump();
    }

    public void ExtractDirectorySizes(List<Node> nodes)
    {
        foreach (var node in nodes.Where(node => node.Size > 0))
	{
		ExtractDirectorySizes(node.Children);
		directorySizes.Add(node.Size);
	}
}

private readonly List<int> directorySizes = new();

public class Node
{
	public Node(string name)
	{
		Name = name;
	}

	public Node(Node parent, string name) : this(name)
	{
		Parent = parent;
	}

	public Node Parent { get; set; }
	public string Name { get; set; }
	public int Size { get; set; }
	public List<Node> Children { get; set; } = new();
}