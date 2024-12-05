namespace AdventOfCode2024;

public class DayFour
{
    private static char[] _targetWord = ['X', 'M', 'A', 'S'];
    private static readonly char[] _targetWordTwo = ['M', 'A', 'S'];
    private static int[][] _grid;

    private static int _width;
    private static int _heigh;

    private static readonly List<Directions> _masDirections = new()
    {
        Directions.DiagonalLeftUp,
        Directions.DiagonalRightDown,
        Directions.DiagonalLeftDown,
        Directions.DiagonalRightUp
    };

    private Stack<(int i, int j, int position, Directions direction)> _stack;

    enum Directions
    {
        Left,
        Right,
        Up,
        Down,
        DiagonalLeftUp,
        DiagonalLeftDown,
        DiagonalRightUp,
        DiagonalRightDown
    }

    public void DayFourSolution()
    {
        var dataPath = CreatePath.GetDataPath(nameof(DayFour));
        var data = File.ReadAllLines(dataPath);
        _heigh = data.Length;
        _width = data[0].Length;

        _grid = new int[_heigh][];
        for (var index = 0; index < _heigh; index++)
        {
            var @string = data[index];
            _grid[index] = new int[_width];
            for (var j = 0; j < _width; j++)
            {
                _grid[index][j] = @string[j];
            }
        }

        PartOne();
        PartTwo();
    }

    private void PartOne()
    {
        var count = 0;
        for (var i = 0; i < _heigh; i++)
        {
            for (var j = 0; j < _width; j++)
            {
                if (_grid[i][j] == _targetWord.First())
                {
                    count += AggregateNode(i, j);
                }
            }
        }

        Console.WriteLine(count);
    }

    private void PartTwo()
    {
        var count = 0;

        for (var i = 0; i < _heigh; i++)
        {
            for (var j = 0; j < _width; j++)
            {
                if (_grid[i][j] == _targetWordTwo[1])
                {
                    count += AggregateMas(i, j);
                }
            }
        }

        Console.WriteLine(count);
    }

    private int AggregateMas(int i, int j)
    {
        var countM = 0;
        var countS = 0;

        for (var index = 0; index < _masDirections.Count; index++)
        {
            var direction = _masDirections[index];
            if (!GetNetPositionOnGrid(direction, i, j, out var res)) return 0;
            switch (_grid[res.row][res.column])
            {
                case 'M':
                    countM++;
                    break;
                case 'S':
                    countS++;
                    break;
                default:
                    return 0;
            }

            if (index == 1 && (countM == 2 || countS == 2))
            {
                return 0;
            }
        }

        return countM == countS && countM == 2 ? 1 : 0;
    }

    private int AggregateNode(int i, int j)
    {
        var count = 0;
        _stack = new Stack<(int i, int j, int position, Directions direction)>();

        foreach (var direction in Enum.GetValues<Directions>())
        {
            DefineNeighbours(i, j, direction);
        }

        while (_stack.Count > 0)
        {
            var (row, column, position, direction) = _stack.Pop();

            position++;
            if (!GetNetPositionOnGrid(direction, row, column, out var res)) continue;
            if (_grid[res.row][res.column] != _targetWord[position]) continue;
            if (position == _targetWord.Length - 1)
            {
                count++;
                continue;
            }

            _stack.Push((res.row, res.column, position, direction));
        }

        return count;
    }

    private void DefineNeighbours(int i, int j, Directions direction)
    {
        var pos = 1;
        if (!GetNetPositionOnGrid(direction, i, j, out var res)) return;
        if (_grid[res.row][res.column] != _targetWord[pos]) return;
        _stack.Push((res.row, res.column, pos, direction));
    }

    private bool GetNetPositionOnGrid(Directions direction, int i, int j, out (int row, int column) res)
    {
        var row = i;
        var column = j;

        switch (direction)
        {
            case Directions.Left:
                column--;
                break;
            case Directions.Right:
                column++;
                break;
            case Directions.Up:
                row--;
                break;
            case Directions.Down:
                row++;
                break;
            case Directions.DiagonalLeftUp:
                column--;
                row--;
                break;
            case Directions.DiagonalLeftDown:
                column--;
                row++;
                break;
            case Directions.DiagonalRightUp:
                column++;
                row--;
                break;
            case Directions.DiagonalRightDown:
                column++;
                row++;
                break;
        }

        res = (row, column);
        return column >= 0 && column < _width && row >= 0 && row < _heigh;
    }
}