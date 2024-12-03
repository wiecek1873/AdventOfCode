namespace AdventOfCode2024.Solvers;
public class Day02 : Solver
{
    private const int MinLevelDiffer = 1;
    private const int MaxLevelDiffer = 3;

    public override async Task SolveAsync()
    {
        var inputStream = GetInputStream("02");

        var safeReportsCounter = 0;
        while (!inputStream.EndOfStream)
        {
            var report = await inputStream.ReadLineAsync();

            if (report == null)
                continue;

            if (IsReportSafe(report))
                safeReportsCounter++;
        }

        PrintResult(2, 1, safeReportsCounter);
    }

    public bool IsReportSafe(string report)
    {
        var levelsAsString = report.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        var levels = Array.ConvertAll(levelsAsString, int.Parse);

        var isIncreasing = levels[0] < levels[1];

        if (!AreAllDecreasing(levels) && !AreAllIncreasing(levels))
            return false;

        return AreDifferencesSafe(levels, MinLevelDiffer, MaxLevelDiffer);

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