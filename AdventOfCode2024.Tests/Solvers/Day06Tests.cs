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
        guardPosition!.Value.X.Should().Be(2);
        guardPosition!.Value.Y.Should().Be(1);
        map[guardPosition.Value.Y][guardPosition.Value.X].Should().Be('>');
    }

    [Fact]
    public void MoveGuard_ShouldMarkWithXGuardStartPosition_IfObstacleExist()
    {
        //Arrange
        var map = GetTestMap();

        //Act
        var guardPosition = Day06.MoveGuard(map, GuardPosition);

        //Assert
        guardPosition.Should().NotBeNull();
        map[GuardPosition.Y][GuardPosition.X].Should().Be('X');
    }

    [Fact]
    public void MoveGuard_ShouldMarkWithXGuardStartPosition_IfObstacleDontExist()
    {
        //Arrange
        var map = GetTestMap();
        map[0][2] = '.';

        //Act
        Day06.MoveGuard(map, GuardPosition);

        //Assert
        map[GuardPosition.Y][GuardPosition.X].Should().Be('X');
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
        var invalidPosition = GuardPosition;
        invalidPosition.X += 1;
        invalidPosition.Y += 1;

        //Act
        Action act = () => Day06.MoveGuard(map, invalidPosition);

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
    public void MarkPathWithX_ShouldMarkPathWithX_WhenMovingRight()
    {
        //Arrange
        var map = GetEmptyMap();
        var y = 1;
        var fromX = 1;
        var toX = 4;

        //Act
        Day06.MarkPathWithX(map, new Vector2Int(fromX, y), new Vector2Int(toX, y));

        //Assert
        for (int x = fromX; x <= toX; x++)
        {
            map[y][x].Should().Be('X');
        }
    }

    [Fact]
    public void MarkPathWithX_ShouldMarkPathWithX_WhenMovingLeft()
    {
        //Arrange
        var map = GetEmptyMap();
        var y = 1;
        var fromX = 4;
        var toX = 1;

        //Act
        Day06.MarkPathWithX(map, new Vector2Int(fromX, y), new Vector2Int(toX, y));

        //Assert
        for (int x = fromX; x >= toX; x--)
        {
            map[y][x].Should().Be('X');
        }
    }

    [Fact]
    public void MarkPathWithX_ShouldMarkPathWithX_WhenMovingDown()
    {
        //Arrange
        var map = GetEmptyMap();
        var x = 1;
        var fromY = 1;
        var toY = 4;

        //Act
        Day06.MarkPathWithX(map, new Vector2Int(x, fromY), new Vector2Int(x, toY));

        //Assert
        for (int y = fromY; y <= toY; y++)
        {
            map[y][x].Should().Be('X');
        }
    }

    [Fact]
    public void MarkPathWithX_ShouldMarkPathWithX_WhenMovingUp()
    {
        //Arrange
        var map = GetEmptyMap();
        var x = 1;
        var fromY = 4;
        var toY = 1;

        //Act
        Day06.MarkPathWithX(map, new Vector2Int(x, fromY), new Vector2Int(x, toY));

        //Assert
        for (int y = fromY; y >= toY; y--)
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
    public void FindNextAvailablePosition_ShouldReturnOutOfMapAsTrue_WhenNoObstacleOnPath()
    {
        //Arrange
        var map = GetTestMap();
        map[0][2] = '.';

        //Act
        var result = Day06.FindNextAvailablePosition(map, GuardPosition, out var outOfMap);

        //Assert
        outOfMap.Should().BeTrue();
    }


    [Fact]
    public void FindNextAvailablePosition_ShouldReturnLastAvailablePosition_WhenNoObstacleOnPath()
    {
        //Arrange
        var map = GetTestMap();
        map[0][2] = '.';

        //Act
        var result = Day06.FindNextAvailablePosition(map, GuardPosition, out var outOfMap);

        //Assert
        outOfMap.Should().BeTrue();
        result.Should().BeEquivalentTo(new Vector2Int(2, 0));
    }

    [Fact]
    public void FindNextAvailablePosition_ShouldReturnAvailablePosition_WhenObstacleOnPath()
    {
        //Arrange
        var map = GetTestMap();
        var expectedPosition = new Vector2Int(2, 1);

        //Act
        var result = Day06.FindNextAvailablePosition(map, GuardPosition, out var outOfMap);

        //Assert
        outOfMap.Should().BeFalse();
        result.Should().Be(expectedPosition);
    }

    [Theory]
    [InlineData('^', 0, -1)]
    [InlineData('>', 1, 0)]
    [InlineData('v', 0, 1)]
    [InlineData('<', -1, 0)]
    public void GetDirection_ShouldReturnValidVector2_BasedOnGuardDirection(char guard, int expectedX, int expectedY)
    {
        //Act
        var result = Day06.GetDirection(guard);

        //Assert
        result.Should().Be(new Vector2Int(expectedX, expectedY));
    }

    [Fact(Skip = "Not implemented")]
    public void GetDirection_ShouldThrowArgumentException_WhenGuardIsNotKnownGuard()
    {

    }

    [Theory]
    [InlineData(0, 1, 'v')]
    [InlineData(1, 0, '>')]
    [InlineData(0, -1, '^')]
    [InlineData(-1, 0, '<')]
    public void GetDirection_ShouldReturnValidChar_BasedOnDirection(int x, int y, char expectedResult)
    {
        //Act
        var result = Day06.GetDirection(new Vector2Int(x, y));

        //Assert
        result.Should().Be(expectedResult);
    }

    [Fact(Skip = "Not implemented")]
    public void GetDirection_ShouldThrowArgumentException_WhenDirectionIsDiagonal()
    {

    }

    [Fact(Skip = "Not implemented")]
    public void GetDirection_ShouldThrowArgumentException_WhenDirectionIsZero()
    {

    }

    [Theory]
    [InlineData('v', '<')]
    [InlineData('>', 'v')]
    [InlineData('^', '>')]
    [InlineData('<', '^')]
    public void RotateGuard_ShouldRotateGuard(char guard, char expectedGuard)
    {
        //Act
        var result = Day06.RotateGuard(guard);

        //Assert
        result.Should().Be(expectedGuard);
    }

    [Fact]
    public void RotateGuard_ShouldThrowArgumentException_WhenGuardIsNotKnownGuard()
    {
        //Act
        Action act = () => Day06.RotateGuard('.');

        //Assert
        act.Should().Throw<ArgumentException>();
    }
}
