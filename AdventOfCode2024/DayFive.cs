namespace AdventOfCode2024;

public class DayFive : Day
{
    private readonly Dictionary<int, HashSet<int>> _rules = new();

    public void DayFiveSolution()
    {
        GetData();
        var correctlyOrdered = 0;
        var incorrectlyOrderd = 0;
        var rulesPrepared = false;

        foreach (var @string in Data)
        {
            if (@string.Length == 0)
            {
                rulesPrepared = true;
                continue;
            }

            if (rulesPrepared)
            {
                var list = ParseInput(@string, ',');
                correctlyOrdered += CheckUpdates(list);
                incorrectlyOrderd += CheckAndFixUpdates(list);
                continue;
            }

            ParseRules(@string);
        }

        Console.WriteLine(correctlyOrdered);
        Console.WriteLine(incorrectlyOrderd);
    }

    private int CheckUpdates(List<int> list)
    {
        for (var i = list.Count - 1; i >= 0; i--)
        {
            var num = list[i];
            if (!_rules.TryGetValue(num, out var set))
            {
                continue;
            }

            for (var j = 0; j < i; j++)
            {
                if (set.Contains(list[j]) || num == list[j])
                {
                    return 0;
                }
            }
        }

        return list[list.Count / 2];
    }

    private int CheckAndFixUpdates(List<int> list)
    {
        var wrongList = false;

        for (var i = list.Count - 1; i >= 0; i--)
        {
            var num = list[i];
            if (!_rules.TryGetValue(num, out var set))
            {
                continue;
            }

            for (var j = 0; j <= i; j++)
            {
                if (!set.Contains(list[j])) continue;
                list.RemoveAt(i);
                list.Insert(j, num);
                wrongList = true;
                i++;
                break;
            }
        }

        return wrongList ? list[list.Count / 2] : 0;
    }

    private void ParseRules(string input)
    {
        var list = ParseInput(input, '|');

        if (!_rules.TryGetValue(list[0], out var neighbours))
        {
            _rules[list[0]] = new();
        }

        _rules[list[0]].Add(list[1]);
    }

    private List<int> ParseInput(string input, char splitter)
    {
        var resultList = input.Split(splitter).ToList().Select(int.Parse).ToList();
        return resultList;
    }
}