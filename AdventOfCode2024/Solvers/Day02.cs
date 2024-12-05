namespace AdventOfCode2024.Solvers;
public class Day02 : Solver
{
    private const int MinLevelDiffer = 1;
    private const int MaxLevelDiffer = 3;

    public override async Task SolveAsync()
    {
        using var inputStream = GetInputStream("02");

        var safeReportsCounter = 0;
        var safeReportsDampenedCounter = 0;
        while (!inputStream.EndOfStream)
        {
            var report = await inputStream.ReadLineAsync();

            if (report == null)
                continue;

            var levels = ParseToIntArray(report);

            if (IsReportSafe(levels))
                safeReportsCounter++;

            for (int i = 0; i < levels.Length; i++)
            {
                var slicedLevels = levels.ToList();
                slicedLevels.RemoveAt(i);

                if (IsReportSafe(slicedLevels.ToArray()))
                {
                    safeReportsDampenedCounter++;
                    break;
                }
            }
        }

        PrintResult(2, 1, safeReportsCounter);
        PrintResult(2, 2, safeReportsDampenedCounter);
    }

    public bool IsReportSafe(int[] levels)
    {
        var isIncreasing = levels[0] < levels[1];

        if (!AreAllDecreasing(levels) && !AreAllIncreasing(levels))
            return false;

        return AreDifferencesSafe(levels, MinLevelDiffer, MaxLevelDiffer);
    }

    private static int[] ParseToIntArray(string report)
    {
        var levelsAsString = report.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        return Array.ConvertAll(levelsAsString, int.Parse);
    }

    public bool AreAllIncreasing(int[] levels)
    {
        for (int i = 1; i < levels.Length; i++)
        {
            if (levels[i-1] >= levels[i])
                return false;
        }

        return true;
    }

    public bool AreAllDecreasing(int[] levels)
    {
        for (int i = 1; i < levels.Length; i++)
        {
            if (levels[i-1] <= levels[i])
                return false;
        }

        return true;
    }

    public bool AreDifferencesSafe(int[] levels, int minDifferInclusive, int maxDifferInclusive)
    {
        for (int i = 1; i <  levels.Length; i++)
        {
            var difference = Math.Abs(levels[i] - levels[i-1]);

            if (difference < minDifferInclusive || maxDifferInclusive < difference)
                return false;
        }

        return true;
    }
}