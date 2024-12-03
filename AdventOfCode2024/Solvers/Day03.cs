using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solvers;
public class Day03 : Solver
{
    public async override Task SolveAsync()
    {
        var inputStream = GetInputStream("03");

        var input = await inputStream.ReadToEndAsync();

        var mulInstructions = GetValidMulInstructions(input);

        var sum = 0;

        foreach (var mulInstruction in mulInstructions)
        {
            sum += GetMulResult(mulInstruction);
        }

        PrintResult(3, 1, sum);
    }

    public string[] GetValidMulInstructions(string input)
    {
        string pattern = "(mul\\()\\d+,\\d+\\)";

        var matches = Regex.Matches(input, pattern);

        return matches.Select(match => match.Value).ToArray();
    }

    public int GetMulResult(string mulInstruction)
    {
        string pattern = "\\d+";

        var numbersMatches = Regex.Matches(mulInstruction, pattern);
        var numbersAsString = numbersMatches.Select(match => match.Value).ToArray();

        var numbers = Array.ConvertAll(numbersAsString, int.Parse);

        return numbers[0] * numbers[1];
    }
}
