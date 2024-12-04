using AdventOfCode2024.Solvers;
using FluentAssertions;

namespace AdventOfCode2024.Tests.Solvers;
public class Day04Tests
{
    private readonly Day04 _day04 = new Day04();

    [Fact]
    public void GetHorizontalXmasCount_ShouldReturnValidWordCount()
    {
        //Arrange
        var input = new char[1][];
        input[0] = ['S', 'A', 'M', 'X', 'M', 'A', 'S'];

        var expectedCount = 2;

        //Act
        var count = _day04.GetHorizontalXmasCount(0, 3, input);

        //Assert
        count.Should().Be(expectedCount);
    }

    [Theory]
    [InlineData(new char[] { 'S', 'A', 'M', 'X', 'M', 'A' }, 3, 1)]
    [InlineData(new char[] { 'A', 'M', 'X', 'M', 'A', 'S' }, 2, 1)]
    public void GetHorizontalXmasCount_ShouldReturnValidWordCount_WhenWordCollideWithArrayBounds(char[] inputRow, int xPositon, int expectedCount)
    {
        //Arrange
        var input = new char[1][];
        input[0] = inputRow;

        //Act
        var count = _day04.GetHorizontalXmasCount(0, xPositon, input);

        //Assert
        count.Should().Be(expectedCount);
    }

    [Fact]
    public void GetVerticalXmasCount_ShouldReturnValidWordCount()
    {
        //Arrange
        var input = new char[7][];
        input[0] = ['S'];
        input[1] = ['A'];
        input[2] = ['M'];
        input[3] = ['X'];
        input[4] = ['M'];
        input[5] = ['A'];
        input[6] = ['S'];

        var expectedCount = 2;

        //Act
        var count = _day04.GetVerticalXmasCount(3, 0, input);

        //Assert
        count.Should().Be(expectedCount);
    }

    [Fact]
    public void GetVerticalXmasCount_ShouldReturnValidWordCount_WhenWordCollideWithLowerBound()
    {
        //Arrange
        var input = new char[6][];
        input[0] = ['A'];
        input[1] = ['M'];
        input[2] = ['X'];
        input[3] = ['M'];
        input[4] = ['A'];
        input[5] = ['S'];

        var expectedCount = 1;

        //Act
        var count = _day04.GetVerticalXmasCount(2, 0, input);

        //Assert
        count.Should().Be(expectedCount);
    }

    [Fact]
    public void GetVerticalXmasCount_ShouldReturnValidWordCount_WhenWordCollideWithUpperBound()
    {
        //Arrange
        var input = new char[6][];
        input[0] = ['S'];
        input[1] = ['A'];
        input[2] = ['M'];
        input[3] = ['X'];
        input[4] = ['M'];
        input[5] = ['A'];

        var expectedCount = 1;

        //Act
        var count = _day04.GetVerticalXmasCount(3, 0, input);

        //Assert
        count.Should().Be(expectedCount);
    }

    [Fact]
    public void GetDiagonalXmasCount_ShouldReturnValidWordCount()
    {
        //Arrange
        var input = new char[7][];
        input[0] = ['S', '.', '.', '.', '.', '.', 'S'];
        input[1] = ['.', 'A', '.', '.', '.', 'A', '.'];
        input[2] = ['.', '.', 'M', '.', 'M', '.', '.'];
        input[3] = ['.', '.', '.', 'X', '.', '.', '.'];
        input[4] = ['.', '.', 'M', '.', 'M', '.', '.'];
        input[5] = ['.', 'A', '.', '.', '.', 'A', '.'];
        input[6] = ['S', '.', '.', '.', '.', '.', 'S'];

        var expectedCount = 4;

        //Act
        var count = _day04.GetDiagonalXmasCount(3, 3, input);

        //Assert
        count.Should().Be(expectedCount);
    }

    [Fact]
    public void GetDiagonalXmasCount_ShouldReturnValidWordCount_WhenWordCollideWithLeftUpBound()
    {
        //Arrange
        var input = new char[6][];
        input[0] = ['A', '.', '.', '.', '.', '.'];
        input[1] = ['.', 'M', '.', '.', '.', '.'];
        input[2] = ['.', '.', 'X', '.', '.', '.'];
        input[3] = ['.', '.', '.', 'M', '.', '.'];
        input[4] = ['.', '.', '.', '.', 'A', '.'];
        input[5] = ['.', '.', '.', '.', '.', 'S'];

        var expectedCount = 1;

        //Act
        var count = _day04.GetDiagonalXmasCount(2, 2, input);

        //Assert
        count.Should().Be(expectedCount);
    }

    [Fact]
    public void GetDiagonalXmasCount_ShouldReturnValidWordCount_WhenWordCollideWithRightUpBound()
    {
        //Arrange
        var input = new char[6][];
        input[0] = ['.', '.', '.', '.', '.', 'A'];
        input[1] = ['.', '.', '.', '.', 'M', '.'];
        input[2] = ['.', '.', '.', 'X', '.', '.'];
        input[3] = ['.', '.', 'M', '.', '.', '.'];
        input[4] = ['.', 'A', '.', '.', '.', '.'];
        input[5] = ['S', '.', '.', '.', '.', '.'];

        var expectedCount = 1;

        //Act
        var count = _day04.GetDiagonalXmasCount(2, 4, input);

        //Assert
        count.Should().Be(expectedCount);
    }
    [Fact]
    public void GetDiagonalXmasCount_ShouldReturnValidWordCount_WhenWordCollideWithLeftDownBound()
    {
        //Arrange
        var input = new char[6][];
        input[0] = ['.', '.', '.', '.', '.', 'S'];
        input[1] = ['.', '.', '.', '.', 'A', '.'];
        input[2] = ['.', '.', '.', 'M', '.', '.'];
        input[3] = ['.', '.', 'X', '.', '.', '.'];
        input[4] = ['.', 'A', '.', '.', '.', '.'];
        input[5] = ['S', '.', '.', '.', '.', '.'];

        var expectedCount = 1;

        //Act
        var count = _day04.GetDiagonalXmasCount(4, 2, input);

        //Assert
        count.Should().Be(expectedCount);
    }

    [Fact]
    public void GetDiagonalXmasCount_ShouldReturnValidWordCount_WhenWordCollideWithRightDownBound()
    {
        //Arrange
        var input = new char[6][];
        input[0] = ['S', '.', '.', '.', '.', '.'];
        input[1] = ['.', 'A', '.', '.', '.', '.'];
        input[2] = ['.', '.', 'M', '.', '.', '.'];
        input[3] = ['.', '.', '.', 'X', '.', '.'];
        input[4] = ['.', '.', '.', '.', 'M', '.'];
        input[5] = ['.', '.', '.', '.', '.', 'A'];

        var expectedCount = 1;

        //Act
        var count = _day04.GetDiagonalXmasCount(4, 3, input);

        //Assert
        count.Should().Be(expectedCount);
    }
}
