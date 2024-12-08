using AdventOfCode2024.Commons;

namespace AdventOfCode2024.Solvers;
public class Day07 : Solver
{
    public async override Task SolveAsync()
    {
        var inputStream = GetInputStream("07");

        long possibleEquationsSum = 0;
        while (!inputStream.EndOfStream)
        {
            var line = await inputStream.ReadLineAsync();

            if (line == null)
                break;

            ParseInputLine(line, out long equationResult, out long[] numbers);

            if (IsEquationPossible(numbers, equationResult))
                possibleEquationsSum += equationResult;
        }

        PrintResult(07, 02, possibleEquationsSum);
    }

    public static void ParseInputLine(string inputLine, out long equationResult, out long[] numbers)
    {
        var lineSplit = inputLine.Split(':');

        var equationResultString = lineSplit[0];
        equationResult = long.Parse(equationResultString);

        var numbersString = lineSplit[1];
        var numbersSplit = lineSplit[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        numbers = numbersSplit.ToList().ConvertAll(long.Parse).ToArray();

    }

    public static bool IsEquationPossible(long[] numbers, long equationExpectedResult)
    {
        foreach (var operatorsPermutations in GetAllOperatorsPermutations(numbers))
        {
            if (EvaluateExpression(numbers, operatorsPermutations.ToArray()) == equationExpectedResult)
                return true;
        }

        return false;
    }

    public static List<List<char>> GetAllOperatorsPermutations(long[] numbers)
    {
        if (numbers.Length < 2)
            return [];

        var permutations = new List<List<char>>();

        int maxOperatorCount = numbers.Length - 1;

        permutations.Add(['+']);
        permutations.Add(['*']);
        permutations.Add(['|']);

        int operatorCount = 1;

        var newPermutations = new List<List<char>>();

        while (operatorCount < maxOperatorCount)
        {
            foreach (var permutation in permutations)
            {
                char[] permutationMultiplyCopy = new char[permutation.Count + 1];
                char[] permutationConcatCopy = new char[permutation.Count + 1];
                permutation.CopyTo(permutationMultiplyCopy);
                permutation.CopyTo(permutationConcatCopy);

                permutation.Add('+');
                permutationMultiplyCopy[^1] = '*';
                permutationConcatCopy[^1] = '|';

                newPermutations.AddRange(permutationMultiplyCopy.ToList());
                newPermutations.AddRange(permutationConcatCopy.ToList());
            }

            permutations.AddRange(newPermutations);
            newPermutations.Clear();
            operatorCount++;
        }

        return permutations;
    }

    public static long EvaluateExpression(long[] numbers, char[] operators)
    {
        var result = numbers[0];

        for (int i = 1; i < numbers.Length; i++)
        {
            result = Evaluate([result, numbers[i]], operators[i-1]);
        }

        return result;
    }

    public static long Evaluate(long[] numbers, char operatorCharacter)
    {
        if (numbers.Length != 2)
            throw new ArgumentException($"Provided numbers count is not 2. Numbers count is: {numbers.Length}", nameof(numbers));

        return operatorCharacter switch
        {
            '+' => numbers[0] + numbers[1],
            '*' => numbers[0] * numbers[1],
            '|' => long.Parse(numbers[0].ToString() + numbers[1].ToString()),
            _ => throw new ArgumentException($"Provided unhandled operator: {operatorCharacter}", nameof(operatorCharacter))
        };
    }
}
