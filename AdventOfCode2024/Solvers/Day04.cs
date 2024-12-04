namespace AdventOfCode2024.Solvers;
public class Day04 : Solver
{
    public override Task SolveAsync()
    {
        throw new NotImplementedException();
    }

    public int GetHorizontalXmasCount(int x, int y, char[][] input)
    {
        var counter = 0;

        if (y + 3 < input[x].Length)
            if (input[x][y+1] == 'M' && input[x][y+2] == 'A' && input[x][y+3] == 'S')
                counter++;

        if (y - 3 >= 0)
            if (input[x][y-1] == 'M' && input[x][y-2] == 'A' && input[x][y-3] == 'S')
                counter++;

        return counter;
    }

    public int GetVerticalXmasCount(int x, int y, char[][] input)
    {
        var counter = 0;

        if (x + 3 < input.Length)
            if (input[x+1][y] == 'M' && input[x+2][y] == 'A' && input[x+3][y] == 'S')
                counter++;

        if (x - 3 >= 0)
            if (input[x-1][y] == 'M' && input[x-2][y] == 'A' && input[x-3][y] == 'S')
                counter++;

        return counter;
    }

    public int GetDiagonalXmasCount(int x, int y, char[][] input)
    {
        var counter = 0;

        if (x + 3 < input.Length)
            if (input[x+1][y] == 'M' && input[x+2][y] == 'A' && input[x+3][y] == 'S')
                counter++;

        if (x - 3 >= 0)
            if (input[x-1][y] == 'M' && input[x-2][y] == 'A' && input[x-3][y] == 'S')
                counter++;

        return counter;
    }
}
