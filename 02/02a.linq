<Query Kind="Statements" />

var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");

var runningScore = 0;

foreach (var input in File.ReadAllLines(inputPath))
{
	var players = input.Split(' ');

	var player1 = players[0];
	var player2 = players[1];

	switch (player1)
	{
		// rock
		case "A":
			{
				if (player2 == "X") // rock - tie
					runningScore += 3;
				if (player2 == "Y") // paper - win
					runningScore += 6;
				break;
			}
		// paper
		case "B":
			{
				if (player2 == "Y") // paper - tie
					runningScore += 3;
				if (player2 == "Z") // scissors - win
					runningScore += 6;
				break;
			}
		// scissors
		case "C":
			{
				if (player2 == "X") // rock - win
					runningScore += 6;
				if (player2 == "Z") // scissors - tie
					runningScore += 3;
				break;
			}
	}

	runningScore += player2 switch
	{
		"X" => 1,
		"Y" => 2,
		"Z" => 3,
		_ => throw new ArgumentOutOfRangeException()
	};
}

runningScore.Dump();