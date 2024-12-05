namespace AdventOfCode2024.Solvers;
public class Day05 : Solver
{
    private static Random rng = new Random();

    public class PrintingRule
    {
        public int PrintedPage;
        public HashSet<int> PagesBefore = new();

        public static PrintingRule Parse(int[] pages)
        {
            var printingRule = new PrintingRule()
            {
                PrintedPage = pages[1]
            };

            printingRule.PagesBefore.Add(pages[0]);

            return printingRule;
        }
    }

    public async override Task SolveAsync()
    {
        var inputStream = GetInputStream("05");

        var printingRules = new Dictionary<int, PrintingRule>();

        var inputLine = await inputStream.ReadLineAsync();
        while (!string.IsNullOrWhiteSpace(inputLine))
        {
            var pages = ParsePrintingRule(inputLine);
            var rule = PrintingRule.Parse(pages);

            if (printingRules.TryGetValue(rule.PrintedPage, out var exsitingRule))
                exsitingRule.PagesBefore.Add(rule.PagesBefore.First());
            else
                printingRules.Add(rule.PrintedPage, rule);

            inputLine = await inputStream.ReadLineAsync();
        }

        var correctUpdatesMiddlePageSum = 0;
        var incorrectUpdatesMiddlePageSum = 0;
        while (!inputStream.EndOfStream)
        {
            inputLine = await inputStream.ReadLineAsync();
            var pages = ParsePages(inputLine);

            if (IsPagesUpdateCorrect(pages, printingRules))
                correctUpdatesMiddlePageSum += pages[pages.Length / 2];
            else
            {
                var correctPagesOrder = pages.ToList();
                do
                {
                    Shuffle(correctPagesOrder); //Bogosort ftw
                }
                while (!IsPagesUpdateCorrect(correctPagesOrder.ToArray(), printingRules));

                incorrectUpdatesMiddlePageSum = correctPagesOrder[correctPagesOrder.Count / 2];
            }
        }

        PrintResult(5, 1, correctUpdatesMiddlePageSum);
    }

    public static int[] ParsePrintingRule(string? inputLine)
    {
        var pagesAsString = inputLine.Split('|', StringSplitOptions.TrimEntries);
        var printingRule = Array.ConvertAll(pagesAsString, int.Parse);

        return printingRule;
    }

    public static int[] ParsePages(string? inputLine)
    {
        var pagesAsString = inputLine.Split(',', StringSplitOptions.TrimEntries);
        var pages = Array.ConvertAll(pagesAsString, int.Parse);

        return pages;
    }

    public static bool IsPagesUpdateCorrect(int[] pages, Dictionary<int, PrintingRule> printingRules)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            var currentPage = pages[i];

            if (!printingRules.ContainsKey(currentPage))
                continue;

            for (int j = i + 1; j < pages.Length - 1; j++)
            {
                var pagesBeforeCurrentPage = printingRules[currentPage].PagesBefore;

                if (pagesBeforeCurrentPage.Contains(pages[j])) //Refactor this xd
                    return false;
            }
        }

        return true;
    }

    public void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
