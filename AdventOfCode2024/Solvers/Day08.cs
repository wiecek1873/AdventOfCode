using AdventOfCode2024.Commons;
namespace AdventOfCode2024.Solvers;

public class Day08 : Solver
{
    public class Antena
    {
        public char Frequency { get; set; }
        public Vector2Int Position { get; set; }

        public Antena() { }

        public Antena(char frequency, Vector2Int prosition)
        {
            Frequency = frequency;
            Position = prosition;
        }

        public Antena(char frequency, int x, int y)
        {
            Frequency = frequency;
            Position = new Vector2Int(x, y);
        }

        public override string? ToString()
        {
            return $"Frequency: {Frequency}, position: {Position}";
        }
    }

    public override async Task SolveAsync()
    {
        using var inputStream = GetInputStream("08");

        var city = await inputStream.ToTwoDimensionalArray();
        var antinodeMap = new char[city.Length][];

        for (int i = 0; i <  city.Length; i++)
        {
            antinodeMap[i] = new char[city[i].Length];
        }

        var allAntenas = GetAllAntenaPositions(city);
        var groupedAntenas = allAntenas.GroupBy(x => x.Frequency);

        foreach (var antenaGroup in groupedAntenas)
        {
            foreach (var antena in antenaGroup)
            {
                foreach (var antena1 in antenaGroup)
                {
                    if (antena == antena1)
                        continue;

                    var antinodePositions = CalculateAntinodePosition(antena, antena1);
                    MarkAntinodeOnCity(antinodeMap, antinodePositions.Item1);
                    MarkAntinodeOnCity(antinodeMap, antinodePositions.Item2);
                }
            }
        }

        var antinodeCount = CountAntinodes(antinodeMap);

        PrintResult(08, 01, antinodeCount);
    }

    public static List<Antena> GetAllAntenaPositions(char[][] city)
    {
        var antenas = new List<Antena>();

        for (int y = 0; y < city.Length; y++)
        {
            for (int x = 0; x < city[y].Length; x++)
            {
                var currentNode = city[y][x];

                if (currentNode == '.')
                    continue;

                var antena = new Antena()
                {
                    Frequency = currentNode,
                    Position = new Vector2Int(x, y)
                };

                antenas.Add(antena);
            }
        }

        return antenas;
    }

    public static (Vector2Int, Vector2Int) CalculateAntinodePosition(Antena antena, Antena antena1)
    {
        if (antena.Frequency != antena1.Frequency)
            throw new ArgumentException($"Can not calculate antinode for antenas with different frequencies. Fist antena: {antena} and second antena: {antena1}");

        if (antena.Position == antena1.Position)
            throw new ArgumentException($"Can not calculate antinode for antenas with same positions.  Fist antena: {antena} and second antena: {antena1}");

        var distance = antena.Position - antena1.Position;

        var antinode = antena.Position + distance;
        var antinode1 = antena1.Position - distance;

        return (antinode, antinode1);
    }

    public static void MarkAntinodeOnCity(char[][] city, Vector2Int antinodePosition)
    {
        if (antinodePosition.Y < 0 || city.Length <= antinodePosition.Y ||
            antinodePosition.X < 0 || city[antinodePosition.Y].Length <= antinodePosition.X)
            return;

        city[antinodePosition.Y][antinodePosition.X] = '#';
    }

    public static int CountAntinodes(char[][] city)
    {
        var counter = 0;

        for (int y = 0; y < city.Length; y++)
        {
            for (int x = 0; x < city[y].Length; x++)
            {
                if (city[y][x] == '#')
                    counter++;
            }
        }

        return counter;
    }
}
