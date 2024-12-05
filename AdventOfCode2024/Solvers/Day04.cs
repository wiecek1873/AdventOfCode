namespace AdventOfCode2024.Solvers;
public class Day04 : Solver
{
    public async override Task SolveAsync()
    {
        using var inputStream = GetInputStream("04");

        var inputList = new List<char[]>();

        while (!inputStream.EndOfStream)
        {
            var line = await inputStream.ReadLineAsync();

            if (line == null)
                break;

            inputList.Add(line.ToCharArray());
        }


        var input = inputList.ToArray();
        var xmasCounter = 0;
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] != 'X')
                    continue;

                xmasCounter += GetHorizontalXmasCount(i, j, input);
                xmasCounter += GetVerticalXmasCount(i, j, input);
                xmasCounter += GetDiagonalXmasCount(i, j, input);
            }
        }
        PrintResult(4, 1, xmasCounter);

        var xShapedMasCounter = CountXShapedMas(input);
        PrintResult(4, 2, xShapedMasCounter);
    }

    public int GetHorizontalXmasCount(int x, int y, char[][] input)
    {
        var counter = 0;

        //Right
        if (y + 3 < input[x].Length)
            if (input[x][y+1] == 'M' && input[x][y+2] == 'A' && input[x][y+3] == 'S')
                counter++;

        //Left
        if (y - 3 >= 0)
            if (input[x][y-1] == 'M' && input[x][y-2] == 'A' && input[x][y-3] == 'S')
                counter++;

        return counter;
    }

    public int GetVerticalXmasCount(int x, int y, char[][] input)
    {
        var counter = 0;

        //Down
        if (x + 3 < input.Length)
            if (input[x+1][y] == 'M' && input[x+2][y] == 'A' && input[x+3][y] == 'S')
                counter++;

        //Up
        if (x - 3 >= 0)
            if (input[x-1][y] == 'M' && input[x-2][y] == 'A' && input[x-3][y] == 'S')
                counter++;

        return counter;
    }

    public int GetDiagonalXmasCount(int x, int y, char[][] input)
    {
        var counter = 0;

        //Left up
        if (x - 3 >= 0 && y - 3 >= 0)
            if (input[x-1][y-1] == 'M' && input[x-2][y-2] == 'A' && input[x-3][y-3] == 'S')
                counter++;

        //Right up
        if (x - 3 >= 0 && y + 3 < input[x-3].Length)
            if (input[x-1][y+1] == 'M' && input[x-2][y+2] == 'A' && input[x-3][y+3] == 'S')
                counter++;

        //Left down
        if (x + 3 < input.Length && y - 3 >= 0)
            if (input[x+1][y-1] == 'M' && input[x+2][y-2] == 'A' && input[x+3][y-3] == 'S')
                counter++;

        //Right down
        if (x + 3 < input.Length && y + 3 < input[x+3].Length)
            if (input[x+1][y+1] == 'M' && input[x+2][y+2] == 'A' && input[x+3][y+3] == 'S')
                counter++;

        return counter;
    }

    public int CountXShapedMas(char[][] input)
    {
        var counter = 0;

        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == 'A' && IsXShapedMas(i, j, input))
                    counter++;
            }
        }

        return counter;
    }

    public bool IsXShapedMas(int x, int y, char[][] input)
    {
        var counter = 0;

        if (!(0 <= x - 1 && x + 1 < input.Length && 0 <= y - 1 && y + 1 < input[x].Length))
            return false;

        //Left up M
        if (input[x-1][y-1] == 'M' && input[x+1][y+1] == 'S')
            counter++;

        //Right up M
        if (input[x-1][y+1] == 'M' && input[x+1][y-1] == 'S')
            counter++;

        //Left down M
        if (input[x+1][y+1] == 'M' && input[x-1][y-1] == 'S')
            counter++;

        //Right down M
        if (input[x+1][y-1] == 'M' && input[x-1][y+1] == 'S')
            counter++;

        return counter > 1;
    }
}
