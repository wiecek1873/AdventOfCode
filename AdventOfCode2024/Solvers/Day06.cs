namespace AdventOfCode2024.Solvers;
public class Day06 : Solver
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public override Task SolveAsync()
    {
        var input = GetInputStream("06");

        throw new NotImplementedException();
    }

    public static void FindGuardPosition(char[][] map, out int guardI, out int guardJ)
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map.Length; j++)
            {
                if (map[i][j] == '^')
                {
                    guardI = i;
                    guardJ = j;
                    return;
                }
            }
        }

        throw new InvalidOperationException("Map does not contain guard");
    }

    public static void MoveGuard(char[][] map, ref int guardI, ref int guardJ)
    {
        throw new NotImplementedException();
    }

    public static int MarkPathWithX(char[][] map, int fromI, int fromJ, int toI, int toJ)
    {
        if (fromI != toI && fromJ != toJ)
            throw new InvalidOperationException($"Can't mark diagonal path. Path start at {fromI},{fromJ} and end at {toI},{toJ}")

        var marksCounter = 0;

        if (fromI < toI)
        {
            for (int i = fromI; i < toI; i++)
            {
                if (TryMarkWithX())
                    marksCounter++;
            }
        }
        else
        {

        }

        if (fromJ < toJ)
        {

        }
        else
        {

        }


    }

    public static bool TryMarkWithX(char[][] map, int i, int j)
    {
        var currentPosition = map[i][j];

        if (currentPosition == 'X')
            return false;

        map[i][j] = 'X';

        return true;
    }

    public static char[][] FillWithGuardPath(char[][] map)
    {
        FindGuardPosition(map, out int guardI, out int guardJ);

        while (guardI != -1 && guardJ != -1)
        {
            MoveGuard(map, ref guardI, ref guardJ);
        }

        throw new NotImplementedException();
    }
}
