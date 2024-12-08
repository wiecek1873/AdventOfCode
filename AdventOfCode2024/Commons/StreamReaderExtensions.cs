namespace AdventOfCode2024.Commons;
public static class StreamReaderExtensions
{
    public static async Task<char[][]> ToTwoDimensionalArray(this StreamReader streamReader)
    {
        var streamReaderList = new List<char[]>();

        while (!streamReader.EndOfStream)
        {
            var streamReaderLine = await streamReader.ReadLineAsync();

            if (streamReaderLine == null)
                break;

            var row = streamReaderLine.ToCharArray();
            streamReaderList.Add(row);
        }

        var twoDimensionalArray = streamReaderList.ToArray();

        return twoDimensionalArray;
    }
}
