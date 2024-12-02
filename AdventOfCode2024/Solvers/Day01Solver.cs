namespace AdventOfCode2024.Solvers;

public class Day01Solver
{
    public static StreamReader GetInputStream()
    {
        return new StreamReader("Inputs/01.txt");
    }

    public async Task SolveAsync()
    {
        var inputStream = GetInputStream();

        var line = await inputStream.ReadLineAsync();

        var sum = 0;

        while (line != null)
        {
            var nums = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            sum += int.Parse(nums[0]) - int.Parse(nums[1]);
            line = await inputStream.ReadLineAsync();
        }

        Console.WriteLine($"Day 1, Part 1 answer is: {sum}");
    }

}
