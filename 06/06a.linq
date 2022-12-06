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
    var startOfPacketMarkerQueue = new Queue<char>();

    for (var i = 0; i < input.Length; i++)
    {
        if (i < 3)
        {
            startOfPacketMarkerQueue.Enqueue(input[i]);
            continue;
        }
        else
		{
			if (startOfPacketMarkerQueue.Count() == 4)
				startOfPacketMarkerQueue.Dequeue();

			startOfPacketMarkerQueue.Enqueue(input[i]);

			if (startOfPacketMarkerQueue.Distinct().Count() == 4)
			{
				($"{input} - {i + 1}").Dump();
				break;
			}
		}
	}
}