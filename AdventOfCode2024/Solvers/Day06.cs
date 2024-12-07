using AdventOfCode2024.Commons;

namespace AdventOfCode2024.Solvers;

public class Day06 : Solver
{
    public static HashSet<char> GuardDirections = ['^', '>', 'v', '<'];


    public override async Task SolveAsync()
    {
        var input = GetInputStream("06");
        var inputList = new List<char[]>();

        while (!input.EndOfStream)
        {
            var inputLine = await input.ReadLineAsync();

            if (inputLine == null)
                break;

            var row = inputLine.ToCharArray();
            inputList.Add(row);
        }

        var map = inputList.ToArray();

        SimulateGuardPatrol(map);
        var distinctGuardPositions = CountCharacter(map, 'X');

        PrintResult(06, 01, distinctGuardPositions);
    }

    public static void SimulateGuardPatrol(char[][] map)
    {
        Vector2Int? guardPosition = FindGuardStartingPosition(map);

        while (guardPosition != null)
        {
            guardPosition = MoveGuard(map, guardPosition.Value);
        }
    }

    public static int CountCharacter(char[][] map, char character)
    {
        var counter = 0;

        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                Console.Write(map[i][j]);
                if (map[i][j] == character)
                    counter++;
            }
            Console.Write("\n");
        }

        return counter;
    }

    public static Vector2Int FindGuardStartingPosition(char[][] map)
    {
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map.Length; x++)
            {
                if (map[y][x] == '^')
                {
                    return new Vector2Int(x, y);
                }
            }
        }

        throw new InvalidOperationException("Map does not contain guard");
    }

    public static Vector2Int? MoveGuard(char[][] map, Vector2Int guardPosition)
    {
        var guard = map[guardPosition.Y][guardPosition.X];

        if (!GuardDirections.Contains(guard))
            throw new InvalidOperationException($"Guard position with value: {guardPosition} is invalid. Guard does not exist in this position");

        var nextAvailablePosition = FindNextAvailablePosition(map, guardPosition, out var outOfMap);


        MarkPathWithX(map, guardPosition, nextAvailablePosition);

        if (outOfMap)
            return null;

        map[nextAvailablePosition.Y][nextAvailablePosition.X] = RotateGuard(guard);

        return nextAvailablePosition;
    }

    public static Vector2Int FindNextAvailablePosition(char[][] map, Vector2Int guardPosition, out bool outOfMap)
    {
        var direction = GetDirection(map[guardPosition.Y][guardPosition.X]);

        var previousPosition = new Vector2Int(guardPosition.Y, guardPosition.X);

        var y = guardPosition.Y;
        var x = guardPosition.X;
        while (map[y][x] != '#')
        {
            previousPosition = new Vector2Int(x, y);

            y += direction.Y;
            x += direction.X;

            if (y < 0 || map.Length <=  y || x < 0 || map[y].Length <= x)
            {
                outOfMap = true;
                return previousPosition;
            }
        }

        outOfMap = false;
        return previousPosition;
    }

    public static void MarkPathWithX(char[][] map, Vector2Int from, Vector2Int to)
    {
        if (from.Equals(to))
            return;

        if (from.Y != to.Y && from.X != to.X)
            throw new InvalidOperationException($"Can't mark diagonal path. Path start at {from.Y},{from.X} and end at {to.Y},{to.X}");

        if (from.Y < to.Y)
        {
            for (int y = from.Y; y <= to.Y; y++)
            {
                MarkWithX(map, new Vector2Int(from.X, y));
            }
        }
        else if (from.Y > to.Y)
        {
            for (int y = from.Y; y >= to.Y; y--)
            {
                MarkWithX(map, new Vector2Int(from.X, y));
            }
        }

        if (from.X < to.X)
        {
            for (int x = from.X; x <= to.X; x++)
            {
                MarkWithX(map, new Vector2Int(x, from.Y));
            }
        }
        else if (from.X > to.X)
        {
            for (int x = from.X; x >= to.X; x--)
            {
                MarkWithX(map, new Vector2Int(x, from.Y));
            }
        }
    }

    public static void MarkWithX(char[][] map, Vector2Int position)
    {
        map[position.Y][position.X] = 'X';
    }

    public static Vector2Int GetDirection(char guard)
    {
        return guard switch
        {
            '^' => new Vector2Int(0, -1),
            '>' => new Vector2Int(1, 0),
            'v' => new Vector2Int(0, 1),
            '<' => new Vector2Int(-1, 0),
            _ => throw new ArgumentException($"Parameter {nameof(guard)} with value {guard} is not recognized as valid guard"),
        };
    }

    public static char GetDirection(Vector2Int direction)
    {
        if (direction.X !=  0 && direction.Y != 0)
            throw new ArgumentException($"Invalid direction with value {direction}");

        if (direction.X > 0)
            return '>';
        else if (direction.X < 0)
            return '<';
        else if (direction.Y > 0)
            return 'v';
        else if (direction.Y < 0)
            return '^';

        throw new ArgumentException($"Vector does not point to any direction. Vector value: {direction}");
    }

    public static char RotateGuard(char guard)
    {
        return guard switch
        {
            '^' => '>',
            '>' => 'v',
            'v' => '<',
            '<' => '^',
            _ => throw new ArgumentException($"Parameter {nameof(guard)} with value {guard} is not recognized as valid guard"),
        };
    }
}
