namespace AdventOfCode2024
{
    public class DayOne
    {
        private static PriorityQueue<int, int> _firstListNumbersQueue = new();
        private static PriorityQueue<int, int> _secondListNumbersQueue = new();

        private static List<int> _firstListNumbers = new();
        private static List<int> _secondListNumbers = new();

        public static void DayOneSolution()
        {
            var dataPath = CreatePath.GetDataPath(nameof(DayOne));
            var data = File.ReadAllLines(dataPath);

            foreach (var pair in data)
            {
                var numbers = pair.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var first = int.Parse(numbers[0]);
                var second = int.Parse(numbers[1]);

                _firstListNumbersQueue.Enqueue(first, first);
                _secondListNumbersQueue.Enqueue(second, second);

                _firstListNumbers.Add(first);
                _secondListNumbers.Add(second);
            }

            FindTotalDistance(data);
            FindSimilarity();
        }

        // PartOne
        private static void FindTotalDistance(string[] data)
        {
            var firstList = new PriorityQueue<int, int>();
            var secondList = new PriorityQueue<int, int>();

            _firstListNumbersQueue = firstList;
            _secondListNumbersQueue = secondList;

            var result = 0;

            for (var i = 0; i < data.Length; i++)
            {
                result += Math.Abs(_firstListNumbersQueue.Dequeue() - _secondListNumbersQueue.Dequeue());
            }

            Console.WriteLine(result);
        }

        // Part Two.
        private static void FindSimilarity()
        {
            var firstListNumbers = new Dictionary<int, (int firstListCount, int secondListCount)>();

            foreach (var number in _firstListNumbers)
            {
                if (!firstListNumbers.TryGetValue(number, out var value))
                {
                    firstListNumbers[number] = (1, 0);
                    continue;
                }

                var firstListNumber = value;
                firstListNumber.firstListCount++;
                firstListNumbers[number] = firstListNumber;
            }

            foreach (var number in _secondListNumbers)
            {
                if (!firstListNumbers.TryGetValue(number, out var value))
                {
                    continue;
                }

                var firstListNumber = value;
                firstListNumber.secondListCount++;
                firstListNumbers[number] = firstListNumber;
            }

            var result = 0;

            foreach (var (number, count) in firstListNumbers)
            {
                if (count.secondListCount > 0)
                {
                    result += number * count.firstListCount * count.secondListCount;
                }
            }

            Console.WriteLine(result);
        }
    }
}