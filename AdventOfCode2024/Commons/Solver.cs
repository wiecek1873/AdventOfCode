namespace AdventOfCode2024.Commons;
public abstract class Solver
{
    public abstract Task SolveAsync();

    protected static StreamReader GetInputStream(string day)
    {
        return new StreamReader($"Inputs/{day}.txt");
    }

    protected void PrintResult(int day, int part, int result)
    {
        Console.WriteLine($"Day {day}, Part {part} answer is: {result}");
    }
}
