using AdventOfCode2024.Solvers;
using FluentAssertions;

namespace AdventOfCode2024.Tests.Solvers;
public class Day06Tests
{
    private readonly Day06 day06 = new Day06();

    private char[][] GetEmptyMap()
    {
        var map = new char[5][];
        map[0] = ['.', '.', '.', '.', '.'];
        map[1] = ['.', '.', '.', '.', '.'];
        map[2] = ['.', '.', '.', '.', '.'];
        map[3] = ['.', '.', '.', '.', '.'];
        map[4] = ['.', '.', '.', '.', '.'];

        return map;
    }

    private char[][] GetTestMap()
    {
        var map = new char[5][];
        map[0] = ['.', '.', '#', '.', '.'];
        map[1] = ['.', '.', '.', '.', '#'];
        map[2] = ['#', '.', '.', '.', '.'];
        map[3] = ['.', '.', '^', '#', '.'];
        map[4] = ['.', '.', '.', '.', '.'];

        return map;
    }

    private int GuardI = 3;
    private int GuardJ = 2;

    [Fact]
    public void FindGuardPosition_ShouldReturnGuardPosition_IfGuardExist()
    {
        //Arrange
        var map = GetTestMap();
        var expectedGuardI = GuardI;
        var expectedGuardJ = GuardJ;

        //Act
        Day06.FindGuardPosition(map, out var guardI, out var guardJ);

        //Assert
        guardI.Should().Be(expectedGuardI);
        guardJ.Should().Be(expectedGuardJ);
    }

    [Fact]
    public void FindGuardPosition_ShouldThrowInvalidOperationException_WhenGuardDontExist()
    {
        //Arrange
        var map = GetTestMap();
        map[GuardI][GuardJ] = '.';

        //Act
        Action act = () => Day06.FindGuardPosition(map, out var guardI, out var guardJ);

        //Assert
        act.Should().Throw<InvalidOperationException>();
    }


    [Fact]
    public void MoveGuard_ShouldMoveGuardToNextObstable_IfObstacleExist()
    {
        //Arrange
        var map = GetTestMap();
        var guardI = GuardI;
        var guardJ = GuardJ;

        //Act
        Day06.MoveGuard(map, ref guardI, ref guardJ);

        //Assert
        guardI.Should().Be(1);
        guardJ.Should().Be(2);
        map[GuardI][GuardJ].Should().Be('>');
    }

    [Fact]
    public void MoveGuard_ShouldRemoveGuardFromStartPosition_IfObstacleExist()
    {
        //Arrange
        var map = GetTestMap();
        var guardI = GuardI;
        var guardJ = GuardJ;

        //Act
        Day06.MoveGuard(map, ref guardI, ref guardJ);

        //Assert
        map[GuardI][GuardJ].Should().Be('.');
    }

    [Fact]
    public void MoveGuard_ShouldRemoveGuardFromStartPosition_IfObstacleDontExist()
    {
        //Arrange
        var map = GetTestMap();
        map[0][2] = '.';
        var guardI = GuardI;
        var guardJ = GuardJ;

        //Act
        Day06.MoveGuard(map, ref guardI, ref guardJ);

        //Assert
        map[GuardI][GuardJ].Should().Be('.');
    }


    [Fact]
    public void MoveGuard_ShouldMoveGuardOutOfMap_WhenObstacleDontExist()
    {
        //Arrange
        var map = GetTestMap();
        map[0][2] = '.';
        var guardI = GuardI;
        var guardJ = GuardJ;

        //Act
        Day06.MoveGuard(map, ref guardI, ref guardJ);

        //Assert
        foreach (var row in map)
        {
            row.Should().NotContain('>');
        }
    }

    [Fact]
    public void MoveGuard_ShouldThrowInvalidOperationException_WhenGuardIsNotInGivenPosition()
    {
        //Arrange
        var map = GetTestMap();
        var guardI = GuardI;
        var guardJ = GuardJ;

        //Act
        Action act = () => Day06.MoveGuard(map, ref guardI, ref guardJ);

        //Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [InlineData(1, 1, 2, 2)]
    [InlineData(0, 0, 4, 4)]
    [InlineData(4, 4, 0, 0)]
    public void MarkPathWithX_ShouldThrowIvalidOperationException_WhenPathIsDiagonal(int fromI, int fromJ, int toI, int toJ)
    {
        //Arrange
        var map = GetEmptyMap();

        //Act
        Action act = () => Day06.MarkPathWithX(map, fromI, fromJ, toI, toJ);

        //Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void MarkPathWithX_ShouldMarkPathWithX()
    {
        //Arrange
        var map = GetEmptyMap();
        var i = 1;
        var fromJ = 1;
        var toJ = 4;

        //Act
        Day06.MarkPathWithX(map, i, fromJ, i, toJ);

        //Assert
        for (int j = fromJ; j < toJ; j++)
        {
            map[i][j].Should().Be('X');
        }
    }

    [Fact]
    public void MarkPathWithX_ShouldReturnMarkedPositionsCount()
    {
        //Arrange
        var map = GetEmptyMap();
        var i = 1;
        var fromJ = 1;
        var toJ = 4;

        //Act
        var result = Day06.MarkPathWithX(map, i, fromJ, i, toJ);

        //Assert
        result.Should().Be(Math.Abs(toJ - fromJ));
    }

    [Fact]
    public void MarkPathWithX_ShouldNotMarkPath_WhenPathAlreadyMarked()
    {
        //Arrange
        var map = GetEmptyMap();
        var i = 1;
        var fromJ = 1;
        var toJ = 3;

        for (int j = fromJ; j < toJ; j++)
        {
            map[i][j] = 'X';
        }

        //Act
        var result = Day06.MarkPathWithX(map, i, fromJ, i, toJ);

        //Assert
        result.Should().Be(0);
    }
}
