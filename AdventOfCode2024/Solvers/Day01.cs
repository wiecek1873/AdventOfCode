namespace AdventOfCode2024.Solvers;

public class Day01 : Solver
{
    public override async Task SolveAsync()
    {
        var inputStream = GetInputStream("01");

        var line = await inputStream.ReadLineAsync();

        var leftList = new List<int>();
        var rightList = new List<int>();

        while (line != null)
        {
            var nums = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var leftId = int.Parse(nums[0]);
            var rightId = int.Parse(nums[1]);

            leftList.Add(leftId);
            rightList.Add(rightId);

            line = await inputStream.ReadLineAsync();
        }

        inputStream.Dispose();

        leftList.Sort();
        rightList.Sort();

        var sum = 0;
        for (int i = 0; i < leftList.Count; i++)
        {
            sum += Math.Abs(leftList[i] - rightList[i]);
        }

        var occurrencesInRightList = new Dictionary<int, int>();
        for (int i = 0; i < rightList.Count; i++)
        {
            var rightId = rightList[i];

            if (occurrencesInRightList.ContainsKey(rightId))
                occurrencesInRightList[rightId]++;
            else
                occurrencesInRightList[rightId] = 1;
        }

        long similarityScore = 0;
        for (int i = 0; i < leftList.Count; i++)
        {
            var leftId = leftList[i];

            if (occurrencesInRightList.TryGetValue(leftId, out var rightId))
                similarityScore += leftId * rightId;
        }

        PrintResult(1, 1, sum);
        PrintResult(1, 2, (int)similarityScore);
    }

}
