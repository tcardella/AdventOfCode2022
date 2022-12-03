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

foreach (var batch in inputs.Chunk(3))
{
    var matches = (from b1 in batch[0]
        join b2 in batch[1] on b1 equals b2
        join b3 in batch[2] on b2 equals b3
        select b1).Distinct();

    var priorities = from m in matches
        join pm in priorityMap on m equals pm.Key
        select pm.Value;

    prioritySum += priorities.Sum();
}

prioritySum.Dump();