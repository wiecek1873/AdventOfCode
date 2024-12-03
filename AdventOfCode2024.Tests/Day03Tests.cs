using AdventOfCode2024.Solvers;
using FluentAssertions;

namespace AdventOfCode2024.Tests;
public class Day03Tests
{
    private readonly Day03 day03 = new Day03();

    [Theory]
    [InlineData("xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))",
        new string[] { "mul(2,4)", "mul(5,5)", "mul(11,8)", "mul(8,5)" })]
    public void GetValidMulInstructions_ShouldReturnValidMulInstructions(string input, string[] expectedResult)
    {
        //Act
        var mulInstructions = day03.GetValidMulInstructions(input);

        //Assert
        mulInstructions.Should().BeEquivalentTo(expectedResult);
    }

    [Theory]
    [InlineData("mul(1,1)", 1)]
    [InlineData("mul(2,3)", 6)]
    [InlineData("mul(12,2024)", 24288)]
    public void GetMulResult(string mulInstruction, int expectedResult)
    {
        //Act
        var result = day03.GetMulResult(mulInstruction);

        //Assert
        result.Should().Be(expectedResult);
    }
}
