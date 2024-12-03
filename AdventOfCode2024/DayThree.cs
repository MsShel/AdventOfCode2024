using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public class DayThree
{
    public static void DayThreeSolution()
    {
        var dataPath = CreatePath.GetDataPath(nameof(DayThree));
        var data = File.ReadAllLines(dataPath);

        // PartOne(data);
        PartTwo(data);
    }

    private static void PartOne(string[] data)
    {
        long result = 0;

        foreach (var input in data) result += FindMultiplications(input);

        Console.WriteLine(result);
    }

    private static void PartTwo(string[] data)
    {
        var input = data.Aggregate("", (current, input) => current + input);

        var result = FindMatches(input);
        Console.WriteLine(result);
    }

    private static int FindMultiplications(string input)
    {
        var mulPattern = @"mul\((\d+),(\d+)\)";
        var matches = Regex.Matches(input, mulPattern);
        var result = 0;

        foreach (Match match in matches)
        {
            result += GetMult(match.Groups);
           
        }

        return result;
    }

    private static long FindMatches(string input)
    {
        var mulPattern = @"mul\((\d+),(\d+)\)";
        var doPattern = @"do\(\)";
        var dontPattern = @"don't\(\)";

        // Альтернативно можно объеденить паттерны в 1:
        //var pattern = @"(?<do>do\(\))|(?<dont>don't\(\))|(?<mul>mul\((\d+),(\d+)\))";

        var isEnabled = true;
        long totalSum = 0;

        var matches = Regex.Matches(input, $"{mulPattern}|{doPattern}|{dontPattern}");

        foreach (Match match in matches)
        {
            if (match.Value.StartsWith("mul"))
            {
                if (!isEnabled) continue;

               totalSum += GetMult(match.Groups);
            }
            else if (match.Value.StartsWith("do()"))
            {
                isEnabled = true;
            }
            else if (match.Value.StartsWith("don't()"))
            {
                isEnabled = false;
            }
        }

        return totalSum;
    }

    private static int GetMult(GroupCollection matchGroups)
    {
        var n = int.Parse(matchGroups[1].Value);
        var m = int.Parse(matchGroups[2].Value);

        return n * m;
    }
}