<Query Kind="Statements" />

            var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");

            var runningScore = 0;

            foreach (var input in File.ReadAllLines(inputPath))
            {
                var players = input.Split(' ');

                var player1 = players[0];
                var player2Outcome = players[1];
                var player2 = "0";

                switch (player2Outcome)
                {
                    // lose
                    case "X" when player1 == "A":
                        player2 = "Z";
                        break;
                    case "X" when player1 == "B":
                        player2 = "X";
                        break;
                    case "X":
                        player2 = "Y";
                        break;
                    // tie
                    case "Y" when player1 == "A":
                        player2 = "X";
                        break;
                    case "Y" when player1 == "B":
                        player2 = "Y";
                        break;
                    case "Y":
                        player2 = "Z";
                        break;
                    // win
                    default:
                    {
                        player2 = player1 switch
                        {
                            "A" => "Y",
							"B" => "Z",
							_ => "X"
						};
				break;
			}
	}

	if (player1 == "A") // rock
	{
		if (player2 == "X") // rock - tie
			runningScore += 3;
		if (player2 == "Y") // paper - win
			runningScore += 6;
	}

	if (player1 == "B") // paper
	{
		if (player2 == "Y") // paper - tie
			runningScore += 3;
		if (player2 == "Z") // scissors - win
			runningScore += 6;
	}

	if (player1 == "C") // scissors
	{
		if (player2 == "X") // rock - win
			runningScore += 6;
		if (player2 == "Z") // scissors - tie
			runningScore += 3;
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