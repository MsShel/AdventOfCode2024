namespace AdventOfCode2024;

public abstract class Day
{
    protected string[] Data;

    protected void GetData()
    {
        var dataPath = CreatePath.GetDataPath(GetType().Name);
        Data = File.ReadAllLines(dataPath);
    }
}