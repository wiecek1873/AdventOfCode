using AdventOfCode2024.Commons;

namespace AdventOfCode2024.Solvers;

public class Day06 : Solver
{

    public override Task SolveAsync()
    {
        var input = GetInputStream("06");

        var map = new char[1][];

        SimulateGuardPatrol(map);

        throw new NotImplementedException();
    }

    public static void SimulateGuardPatrol(char[][] map)
    {
        Vector2Int? guardPosition = FindGuardStartingPosition(map);

        while (guardPosition != null)
        {
            guardPosition = MoveGuard(map, guardPosition.Value);
        }

        throw new NotImplementedException();
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

    public static Vector2Int MoveGuard(char[][] map, Vector2Int guardPosition)
    {
        var nextAvailablePosition = FindNextAvailablePosition(map, guardPosition);


    }

    public static Vector2Int? FindNextAvailablePosition(char[][] map, Vector2Int guardPosition)
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

            if (y < 0 || map.Length <  y || x < 0 || map[y].Length < x)
                return null;
        }

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
            'v' => new Vector2Int(0, -1),
            '<' => new Vector2Int(-1, 0),
            _ => throw new ArgumentException($"Parameter {nameof(guard)} with value {guard} is not recognized as valid guard"),
        };
    }

    public static char GetDirection(Vector2Int direction)
    {
        if (direction.X !=  0 || direction.Y != 0)
            throw new ArgumentException($"Invalid direction with value {direction}");

        return direction switch
        {
            Vector2Int(0,) => new Vector2Int(0, -1),
            '>' => new Vector2Int(1, 0),
            'v' => new Vector2Int(0, -1),
            '<' => new Vector2Int(-1, 0),
            _ => throw new ArgumentException($"Parameter {nameof(guard)} with value {guard} is not recognized as valid guard"),
        };
    }
}
