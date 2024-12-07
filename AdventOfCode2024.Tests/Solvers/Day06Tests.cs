using AdventOfCode2024.Commons;
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

    private Vector2Int GuardPosition = new Vector2Int(2, 3);

    [Fact]
    public void FindGuardStartingPosition_ShouldReturnGuardPosition_IfGuardExist()
    {
        //Arrange
        var map = GetTestMap();

        //Act
        var result = Day06.FindGuardStartingPosition(map);

        //Assert
        result.Should().Be(GuardPosition);
    }

    [Fact]
    public void FindGuardStartingPosition_ShouldThrowInvalidOperationException_WhenGuardDontExist()
    {
        //Arrange
        var map = GetTestMap();
        map[GuardPosition.Y][GuardPosition.X] = '.';

        //Act
        Action act = () => Day06.FindGuardStartingPosition(map);

        //Assert
        act.Should().Throw<InvalidOperationException>();
    }


    [Fact]
    public void MoveGuard_ShouldMoveGuardToNextObstable_IfObstacleExist()
    {
        //Arrange
        var map = GetTestMap();

        //Act
        var guardPosition = Day06.MoveGuard(map, GuardPosition);

        //Assert
        guardPosition.Should().NotBeNull();
        guardPosition.X.Should().Be(2);
        guardPosition.Y.Should().Be(1);
        map[GuardPosition.Y][GuardPosition.X].Should().Be('>');
    }

    [Fact]
    public void MoveGuard_ShouldRemoveGuardFromStartPosition_IfObstacleExist()
    {
        //Arrange
        var map = GetTestMap();

        //Act
        var guardPosition = Day06.MoveGuard(map, GuardPosition);

        //Assert
        map[guardPosition.Y][guardPosition.X].Should().Be('.');
    }

    [Fact]
    public void MoveGuard_ShouldRemoveGuardFromStartPosition_IfObstacleDontExist()
    {
        //Arrange
        var map = GetTestMap();
        map[0][2] = '.';

        //Act
        Day06.MoveGuard(map, GuardPosition);

        //Assert
        map[GuardPosition.Y][GuardPosition.X].Should().Be('.');
    }


    [Fact]
    public void MoveGuard_ShouldMoveGuardOutOfMap_WhenObstacleDontExist()
    {
        //Arrange
        var map = GetTestMap();
        map[0][2] = '.';

        //Act
        Day06.MoveGuard(map, GuardPosition);

        //Assert
        foreach (var row in map)
        {
            row.Should().NotContain('^');
            row.Should().NotContain('>');
            row.Should().NotContain('v');
            row.Should().NotContain('<');
        }
    }

    [Fact]
    public void MoveGuard_ShouldThrowInvalidOperationException_WhenGuardIsNotInGivenPosition()
    {
        //Arrange
        var map = GetTestMap();

        //Act
        Action act = () => Day06.MoveGuard(map, GuardPosition);

        //Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [InlineData(1, 1, 2, 2)]
    [InlineData(0, 0, 4, 4)]
    [InlineData(4, 4, 0, 0)]
    public void MarkPathWithX_ShouldThrowIvalidOperationException_WhenPathIsDiagonal(int fromY, int fromX, int toY, int toX)
    {
        //Arrange
        var map = GetEmptyMap();

        //Act
        Action act = () => Day06.MarkPathWithX(map, new Vector2Int(fromX, fromY), new Vector2Int(toX, toY));

        //Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void MarkPathWithX_ShouldMarkPathWithX()
    {
        //Arrange
        var map = GetEmptyMap();
        var y = 1;
        var fromX = 1;
        var toX = 4;

        //Act
        Day06.MarkPathWithX(map, new Vector2Int(fromX, y), new Vector2Int(toX, y));

        //Assert
        for (int x = fromX; x < toX; x++)
        {
            map[y][x].Should().Be('X');
        }
    }

    [Fact]
    public void TryMarkWithX_ShouldSetToX()
    {
        //Arrange
        var map = GetEmptyMap();
        var position = new Vector2Int(0, 0);

        //Act
        Day06.MarkWithX(map, position);

        //Assert
        map[position.Y][position.X].Should().Be('X');
    }

    [Fact]
    public void FindNextAvailablePosition_ShouldReturnNull_WhenNoObstacleOnPath()
    {
        //Arrange
        var map = GetTestMap();
        map[0][2] = '.';

        //Act
        var result = Day06.FindNextAvailablePosition(map, GuardPosition);

        //Assert
        result.Should().BeNull();
    }

    [Fact]
    public void FindNextAvailablePosition_ShouldReturnAvailablePosition_WhenObstacleOnPath()
    {
        //Arrange
        var map = GetTestMap();
        var expectedPosition = new Vector2Int(2, 1);

        //Act
        var result = Day06.FindNextAvailablePosition(map, GuardPosition);

        //Assert
        result.Should().NotBeNull();
        result.Should().Be(expectedPosition);
    }

    //[Theory]
    //[InlineData('^', new Vector2(-1, 0))]
    //[InlineData('>', new Vector2(0, 1))]
    //[InlineData('v', new Vector2(1, 0))]
    //[InlineData('<', new Vector2(0, -1))]
    //public void GetDirection_ShouldReturnValidVector2_BasedOnGuardDirection(char guard, Vector2 expectedDirection)
    //{

    //}
}
