namespace AdventOfCode2024;

public class DayTwo
{
    public static void DayTwoSolution()
    {
        var dataPath = CreatePath.GetDataPath(nameof(DayTwo));
        var data = File.ReadAllLines(dataPath);

        var result = 0;
        var resultPartTwo = 0;

        foreach (var pair in data)
        {
            var numbers = pair.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            var levels = numbers.Select(int.Parse).ToList();
            result += IsReportSafe(levels);
            resultPartTwo += IsReportSafe2(levels);
        }

        Console.WriteLine(result);
        Console.WriteLine(resultPartTwo);
    }

    // Part One.
    private static int IsReportSafe(List<int> levels)
    {
        return CheckSequence(levels) ? 1 : 0;
    }

    // Part Two.
    private static int IsReportSafe2(List<int> levels)
    {
        if (CheckSequence(levels))
        {
            return 1;
        }

        for (var index = 0; index < levels.Count; index++)
        {
            var temp = new List<int>(levels);
            temp.RemoveAt(index);

            if (CheckSequence(temp))
            {
                return 1;
            }
        }

        return 0;
    }

    private static bool CheckSequence(List<int> levels)
    {
        var increasing = true;
        var decreasing = true;

        for (var i = 1; i < levels.Count; i++)
        {
            var diff = Math.Abs(levels[i] - levels[i - 1]);
            if (diff is > 3 or < 1)
            {
                return false;
            }

            if (levels[i] > levels[i - 1])
            {
                decreasing = false;
            }
            else
            {
                increasing = false;
            }
        }

        return increasing || decreasing;
    }
}