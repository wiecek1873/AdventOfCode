using AdventOfCode2024.Commons;
using AdventOfCode2024.Solvers;
using FluentAssertions;
using static AdventOfCode2024.Solvers.Day08;

namespace AdventOfCode2024.Tests.Solvers;
public class Day08Tests
{
    private char[][] GetEmptyCity()
    {
        var map = new char[5][];
        map[0] = ['.', '.', '.', '.', '.'];
        map[1] = ['.', '.', '.', '.', '.'];
        map[2] = ['.', '.', '.', '.', '.'];
        map[3] = ['.', '.', '.', '.', '.'];
        map[4] = ['.', '.', '.', '.', '.'];

        return map;
    }

    private char[][] GetTestCity()
    {
        var map = new char[5][];
        map[0] = ['1', '.', '.', '.', '.'];
        map[1] = ['.', '.', 'B', '.', '.'];
        map[2] = ['.', 'B', 'A', '.', 'A'];
        map[3] = ['.', '.', 'c', '.', '.'];
        map[4] = ['.', '.', '.', '.', '1'];

        return map;
    }

    private List<Antena> GetAllAntenas()
    {
        var antenas = new List<Antena>();
        var map = GetTestCity();

        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] != '.')
                    antenas.Add(new Antena { Frequency = map[y][x], Position = new Vector2Int(x, y) });
            }
        }

        return antenas;
    }

    [Fact]
    public void GetAllAntenaPositions_ShouldReturnAllAntenas()
    {
        //Arrange
        var map = GetTestCity();
        var expectedAntenas = GetAllAntenas();

        //Act
        var result = GetAllAntenaPositions(map);

        //Assert
        result.Count.Should().Be(expectedAntenas.Count);
        result.Should().BeEquivalentTo(expectedAntenas);
    }


    [Fact]
    public void GetAllAntenaPositions_ShouldReturnEmptyList_WhenMapIsEmpty()
    {
        //Arrange
        var map = GetEmptyCity();

        //Act
        var result = GetAllAntenaPositions(map);

        //Assert
        result.Count.Should().Be(0);
    }

    [Theory]
    [InlineData('A', 2, 2, 4, 2, 0, 2, 6, 2)]
    [InlineData('B', 1, 1, 2, 2, 0, 0, 3, 3)]
    public void CalculateAntinodePositions_ShouldReturnValidAntinodesPositions(char frequency, int antenaX, int antenaY, int antena1X, int antena1Y, int expectedX, int expectedY, int expectedX1, int expectedY1)
    {
        //Arrange
        var antena = new Antena(frequency, antenaX, antenaY);
        var antena1 = new Antena(frequency, antena1X, antena1Y);

        //Act
        var result = CalculateAntinodePosition(antena, antena1);

        //Assert
        result.Item1.X.Should().Be(expectedX);
        result.Item1.Y.Should().Be(expectedY);
        result.Item2.X.Should().Be(expectedX1);
        result.Item2.Y.Should().Be(expectedY1);
    }

    [Fact]
    public void CalculateAntinodePositions_ShouldThrowArgumentException_WhenAntenasHaveDifferentFrequency()
    {
        //Arrange
        var antenaA = new Antena('A', 0, 1);
        var antenaB = new Antena('B', 2, 1);

        //Act
        Action act = () => Day08.CalculateAntinodePosition(antenaA, antenaB);

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CalculateAntinodePositions_ShouldThrowArgumentException_WhenAntenasOnSamePosition()
    {
        //Arrange
        var antenaA = new Antena('A', 0, 1);
        var antenaB = new Antena('A', 0, 1);

        //Act
        Action act = () => CalculateAntinodePosition(antenaA, antenaB);

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(5, 5)]
    [InlineData(-1, -1)]
    [InlineData(0, 5)]
    [InlineData(5, 0)]
    public void MarkAntinodeOnCity_ShouldDoNothing_WhenAntinodeOutOfCity(int x, int y)
    {
        //Arrange
        var city = GetEmptyCity();

        //Act
        MarkAntinodeOnCity(city, new Vector2Int(x, y));

        //Assert
        foreach (var row in city)
        {
            row.Should().NotContain('#');
        }
    }

    [Theory]
    [InlineData(4, 4)]
    [InlineData(0, 0)]
    [InlineData(0, 4)]
    [InlineData(4, 0)]
    public void MarkAntinodeOnCity_ShouldMark_WhenAntinodeInCity(int x, int y)
    {
        //Arrange
        var city = GetEmptyCity();

        //Act
        MarkAntinodeOnCity(city, new Vector2Int(x, y));

        //Assert
        city[y][x].Should().Be('#');
    }

    [Fact]
    public void CountAntinodes_ShouldReturnValidCount()
    {
        //Arrange
        var city = GetEmptyCity();
        city[0][1] = '#';
        city[1][1] = '#';
        city[4][1] = '#';
        var expectedResult = 3;

        //Act
        var result = CountAntinodes(city);

        //Assert
        result.Should().Be(expectedResult);
    }
}
