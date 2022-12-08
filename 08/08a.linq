<Query Kind="Program" />

void Main()
{
	var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");
	var inputs = File.ReadAllLines(inputPath);

	var input = FormatInput(inputs);

	var height = input.GetLength(0);
	var width = input.GetLength(1);
	var visibleTrees = (height * 2) + (width * 2) - 4;

	for (int i = 1; i < width - 1; i++)
	{
		for (int j = 1; j < height - 1; j++)
		{
			if (CheckLeft(input, i, j) || CheckRight(input, i, j, width) || CheckUp(input, i, j) || CheckDown(input, i, j, height))
				visibleTrees++;
		}
	}

	visibleTrees.Dump("Visible Trees");
}

bool CheckDown(int[,] input, int x, int y, int height)
{
	for (var i = x + 1; i < height; i++)
	{
		if (input[x, y] <= input[i, y])
			return false;
	}

	return true;
}

bool CheckUp(int[,] input, int x, int y)
{
	for (var i = x - 1; i >= 0; i--)
	{
		if (input[x, y] <= input[i, y])
			return false;
	}

	return true;
}

bool CheckRight(int[,] input, int x, int y, int width)
{
	for (var i = y + 1; i < width; i++)
	{
		if (input[x, y] <= input[x, i])
			return false;
	}

	return true;
}

bool CheckLeft(int[,] input, int x, int y)
{
	for (var i = y - 1; i >= 0; i--)
	{
		if (input[x, y] <= input[x, i])
			return false;
	}

	return true;
}

int[,] FormatInput(string[] inputs)
{
	var output = new int[inputs[0].Length, inputs.Length];

	for (int i = 0; i < inputs.Length; i++)
	{
		for (int j = 0; j < inputs[i].Length; j++)
		{
			output[i, j] = int.Parse(inputs[i][j].ToString());
		}
	}

	return output;
}

