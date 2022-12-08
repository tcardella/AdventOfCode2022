<Query Kind="Program" />

void Main()
{
	var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");
	var inputs = File.ReadAllLines(inputPath);

	var input = FormatInput(inputs);

	var height = input.GetLength(0);
	var width = input.GetLength(1);
	var highestScenicScore = 0;

	for (var i = 0; i < width; i++)
		for (var j = 0; j < height; j++)
		{
			var scenicScore = CountLeft(input, i, j) * CountRight(input, i, j, width) * CountUp(input, i, j) * CountDown(input, i, j, height);

			if (scenicScore > highestScenicScore)
				highestScenicScore = scenicScore;
		}

	highestScenicScore.Dump("Highest Scenic Score");
}

private int CountDown(int[,] input, int x, int y, int height)
{
	var output = 0;

	for (var i = x + 1; i < height; i++)
	{
		if (input[x, y] > input[i, y])
			output++;
		else if (input[x, y] <= input[i, y])
		{
			output++;
			break;
		}
		else
			break;
	}

	return output;
}

private int CountUp(int[,] input, int x, int y)
{
	var output = 0;

	for (var i = x - 1; i >= 0; i--)
		if (input[x, y] > input[i, y])
			output++;
		else if (input[x, y] <= input[i, y])
		{
			output++;
			break;
		}
		else
			break;

	return output;
}

private int CountRight(int[,] input, int x, int y, int width)
{
	var output = 0;

	for (var i = y + 1; i < width; i++)
		if (input[x, y] > input[x, i])
			output++;
		else if (input[x, y] <= input[x, i])
		{
			output++;
			break;
		}
		else
			break;

	return output;
}

private int CountLeft(int[,] input, int x, int y)
{
	var output = 0;

	for (var i = y - 1; i >= 0; i--)
		if (input[x, y] > input[x, i])
			output++;
		else if (input[x, y] <= input[x, i])
		{
			output++;
			break;
		}
		else
			break;

	return output;
}

private int[,] FormatInput(string[] inputs)
{
	var output = new int[inputs[0].Length, inputs.Length];

	for (var i = 0; i < inputs.Length; i++)
		for (var j = 0; j < inputs[i].Length; j++)
			output[i, j] = int.Parse(inputs[i][j].ToString());

	return output;
}