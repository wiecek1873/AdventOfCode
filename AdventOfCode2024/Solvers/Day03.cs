using AdventOfCode2024.Commons;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solvers;
public class Day03 : Solver
{
    public async override Task SolveAsync()
    {
        using var inputStream = GetInputStream("03");

        var input = await inputStream.ReadToEndAsync();

        var mulInstructions = GetValidMulInstructions(input);

        var sum = 0;

        foreach (var mulInstruction in mulInstructions)
        {
            sum += GetMulResult(mulInstruction);
        }

        PrintResult(3, 1, sum);

        var allInstructions = GetInstructions(input);

        var conditionalSum = 0;
        var instrcutionsEnabled = true;
        foreach (var instruction in allInstructions)
        {
            switch (instruction)
            {
                case "do()":
                    instrcutionsEnabled = true;
                    break;
                case "don't()":
                    instrcutionsEnabled = false;
                    break;
                default:
                    if (instrcutionsEnabled)
                        conditionalSum += GetMulResult(instruction);
                    break;
            }
        }

        PrintResult(3, 2, conditionalSum);
    }

    public string[] GetInstructions(string input)
    {
        string pattern = "(mul\\()\\d+,\\d+\\)|(do\\(\\))|(don\\'t\\(\\))";

        var matches = Regex.Matches(input, pattern);

        return matches.Select(m => m.Value).ToArray();
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
