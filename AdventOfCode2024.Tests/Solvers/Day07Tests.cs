using AdventOfCode2024.Solvers;
using FluentAssertions;

namespace AdventOfCode2024.Tests.Solvers;
public class Day07Tests
{

    [Theory]
    [InlineData(new long[] { 2, 4 }, '+', 6)]
    [InlineData(new long[] { 2, 4 }, '*', 8)]
    public void Evaluate_ShouldReturnValidResult_ForGivenOperator(long[] numbers, char operatorCharacter, long expectedResult)
    {
        //Act
        var result = Day07.Evaluate(numbers, operatorCharacter);

        //Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void Evaluate_ShouldThrowAgrumentException_ForUnhanledOperator()
    {
        //Arrange
        var numbers = new long[] { 1, 2 };
        var invalidOperator = '/';

        //Act
        Action act = () => Day07.Evaluate(numbers, invalidOperator);

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(new long[] { 1, 2, 3 })]
    [InlineData(new long[] { 1 })]
    [InlineData(new long[] { })]
    public void Evaluate_ShouldThrowArgumentException_WhenInputNumbersIsNotPair(long[] invalidNumbers)
    {
        //Act
        Action act = () => Day07.Evaluate(invalidNumbers, '+');

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(new long[] { }, 0)]
    [InlineData(new long[] { 1 }, 0)]
    [InlineData(new long[] { 1, 2 }, 2)]
    [InlineData(new long[] { 1, 2, 3 }, 4)]
    [InlineData(new long[] { 1, 2, 3, 4 }, 8)]
    [InlineData(new long[] { 1, 2, 3, 4, 5 }, 16)]
    [InlineData(new long[] { 1, 2, 3, 4, 5, 6 }, 32)]
    [InlineData(new long[] { 1, 2, 3, 4, 5, 6, 7 }, 64)]
    [InlineData(new long[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 128)]
    [InlineData(new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 256)]
    [InlineData(new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 512)]
    [InlineData(new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, 1024)]
    [InlineData(new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, 2048)]
    [InlineData(new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 }, 4096)]
    public void GetAllOperatorsPermutations_ShouldReturnValidPermutationsCount(long[] numbers, int count)
    {
        //Act
        var result = Day07.GetAllOperatorsPermutations(numbers);

        //Assert
        result.Count().Should().Be(count);
    }

    [Fact]
    public void GetAllOperatorsPermutations_ShouldReturnAllPermutations()
    {
        //Arrange
        var numbers = new long[] { 1, 2, 3, 4 };
        var expectedResult = new List<char[]>();
        expectedResult.AddRange(['+', '+', '+']);
        expectedResult.AddRange(['*', '+', '+']);
        expectedResult.AddRange(['+', '*', '+']);
        expectedResult.AddRange(['+', '+', '*']);
        expectedResult.AddRange(['*', '+', '*']);
        expectedResult.AddRange(['+', '*', '*']);
        expectedResult.AddRange(['*', '*', '*']);
        expectedResult.AddRange(['+', '*', '*']);

        //Act
        var result = Day07.GetAllOperatorsPermutations(numbers);

        //Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Theory]
    [InlineData(new long[] { 2, 4 }, new char[] { '+' }, 6)]
    [InlineData(new long[] { 2, 4 }, new char[] { '*' }, 8)]
    [InlineData(new long[] { 2, 4, 8 }, new char[] { '+', '*' }, 48)]
    [InlineData(new long[] { 2, 4, 8 }, new char[] { '*', '+' }, 16)]
    public void EvaluateExpression_ShouldReturnValidResult(long[] numbers, char[] operators, long expectedResult)
    {
        //Act
        var result = Day07.EvaluateExpression(numbers, operators);

        //Arrange
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData(new long[] { 10, 19 }, 190)]
    [InlineData(new long[] { 81, 40, 27 }, 3267)]
    [InlineData(new long[] { 11, 6, 16, 20 }, 292)]
    public void IsEquationPossible_ShouldReturnTrue_WhenEquationIsPossible(long[] numbers, long equationExpectedResult)
    {
        //Act
        var result = Day07.IsEquationPossible(numbers, equationExpectedResult);

        //Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(new long[] { 17, 5 }, 83)]
    [InlineData(new long[] { 15, 6 }, 156)]
    [InlineData(new long[] { 16, 10, 13 }, 161011)]
    public void IsEquationPossible_ShouldReturnFalse_WhenEquationIsNotPossible(long[] numbers, long equationExpectedResult)
    {
        //Act
        var result = Day07.IsEquationPossible(numbers, equationExpectedResult);

        //Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("9738: 7 89 52 75 8 1", 9738, new long[] { 7, 89, 52, 75, 8, 1 })]
    public void ParseInputLine_ShouldCorrectlyParseInput_WhenInputIsValid(string inputLine, long expectedEquationResult, long[] expectedNumbers)
    {
        //Act
        Day07.ParseInputLine(inputLine, out var equationResult, out var numbers);

        //Assert
        equationResult.Should().Be(expectedEquationResult);
        numbers.Should().BeEquivalentTo(expectedNumbers);
    }

}
