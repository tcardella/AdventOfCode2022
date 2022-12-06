<Query Kind="Statements" />

        var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");
        var inputs = await File.ReadAllLinesAsync(inputPath);

        //var inputs = new[]
        //{
        //    "mjqjpqmgbljsphdztnvjfqwrcgsmlb",
        //    "bvwbjplbgvbhsrlpgdmjqwftvncz",
        //    "nppdvjthqldpwncqszvftbrmjlhg",
        //    "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg",
        //    "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw"
        //};

        foreach (var input in inputs)
        {
            var startOfMessageMarkerQueue = new Queue<char>();

            for (var i = 0; i < input.Length; i++)
            {
                if (i < 13)
		{
			startOfMessageMarkerQueue.Enqueue(input[i]);
			continue;
		}
		else
		{
			if (startOfMessageMarkerQueue.Count() == 14)
				startOfMessageMarkerQueue.Dequeue();

			startOfMessageMarkerQueue.Enqueue(input[i]);

			if (startOfMessageMarkerQueue.Distinct().Count() == 14)
			{
				($"{input} - {i + 1}").Dump();
				break;
			}
		}
	}
}