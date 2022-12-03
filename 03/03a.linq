<Query Kind="Statements" />

var priorityMap = Enumerable.Range(97, 26).Select((e, i) => new { character = (char)e, priority = i + 1 })
    .Union(Enumerable.Range(65, 26).Select((e, i) => new { character = (char)e, priority = i + 27 }))
    .ToDictionary(e => e.character, e => e.priority);

var inputPath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");
var inputs = await File.ReadAllLinesAsync(inputPath);

//var inputs = new[]
//{
//    "vJrwpWtwJgWrhcsFMMfFFhFp",
//    "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
//    "PmmdzqPrVvPwwTWBwg",
//    "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
//    "ttgJtRGJQctTZtZT",
//    "CrZsJsPPZsGzwwsLwLmpwMDw"
//};

var prioritySum = 0;

foreach (var input in inputs)
{
    var matches = new HashSet<char>();

    for (var i = 0; i < input.Length / 2; i++)
    for (var j = input.Length / 2; j < input.Length; j++)
        if (input[i] == input[j])
            matches.Add(input[i]);

    var priorities = from m in matches
        join pm in priorityMap on m equals pm.Key
        select pm.Value;

	prioritySum += priorities.Sum();
}

prioritySum.Dump();