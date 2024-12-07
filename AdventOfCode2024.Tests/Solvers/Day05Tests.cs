using AdventOfCode2024.Solvers;
using FluentAssertions;

namespace AdventOfCode2024.Tests.Solvers;
public class Day05Tests
{
    private readonly Day05 day05 = new Day05();

    [Theory]
    [InlineData("23|54", new int[] { 23, 54 })]
    [InlineData("54|23", new int[] { 54, 23 })]
    public void ParsePrintingRule_ShouldReturnRuleArray(string input, int[] expectedPages)
    {
        //Act
        var result = Day05.ParsePrintingRule(input);

        //Assert
        result.Should().BeEquivalentTo(expectedPages);
    }

    [Theory]
    [InlineData("12,45,63,12", new int[] { 12, 45, 63, 12 })]
    [InlineData("51", new int[] { 54 })]
    public void ParsePages_ShouldReturnPages(string input, int[] expectedPages)
    {
        //Act
        var result = Day05.ParsePrintingRule(input);

        //Assert
        result.Should().BeEquivalentTo(expectedPages);

    }

    [Fact]
    public void PrintingRule_Parse_ShouldReturnPrintingRule()
    {
        //Arrange
        var pages = new int[] { 23, 54 };

        //Act
        var printingRule = Day05.PrintingRule.Parse(pages);

        //Assert
        printingRule.PrintedPage.Should().Be(pages[1]);
        printingRule.PagesBefore.Should().Contain(pages[0]);
    }

    [Fact]
    public void IsPagesUpdateCorrect_shouldReturnTrue_WhenUpdateIsCorrect()
    {
        //Arrange
        var pages = new int[] { 75, 47, 61 };
        var printingRules = new Dictionary<int, Day05.PrintingRule>();

        Day05.PrintingRule rule = new Day05.PrintingRule { PrintedPage = 47 };
        rule.PagesBefore.Add(75);
        printingRules.Add(47, rule);

        Day05.PrintingRule rule1 = new Day05.PrintingRule { PrintedPage = 61 };
        rule1.PagesBefore.Add(47);
        printingRules.Add(61, rule1);

        //Act
        var result = Day05.IsPagesUpdateCorrect(pages, printingRules);

        //Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsPagesUpdateCorrect_ShouldReturnFalse_WhenUpdateIsNotCorrect()
    {
        //Arrange
        var pages = new int[] { 47, 61, 75 };
        var printingRules = new Dictionary<int, Day05.PrintingRule>();

        Day05.PrintingRule rule = new Day05.PrintingRule { PrintedPage = 47 };
        rule.PagesBefore.Add(75);
        printingRules.Add(47, rule);

        Day05.PrintingRule rule1 = new Day05.PrintingRule { PrintedPage = 61 };
        rule1.PagesBefore.Add(47);
        printingRules.Add(61, rule1);

        //Act
        var result = Day05.IsPagesUpdateCorrect(pages, printingRules);

        //Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GetFixedUpdateMiddlePage_ShouldReturnValidPage()
    {
        //Arrange
        var pages = new int[] { 47, 61, 75 };
        var printingRules = new Dictionary<int, Day05.PrintingRule>();

        Day05.PrintingRule rule = new Day05.PrintingRule { PrintedPage = 47 };
        rule.PagesBefore.Add(75);
        printingRules.Add(47, rule);

        Day05.PrintingRule rule1 = new Day05.PrintingRule { PrintedPage = 61 };
        rule1.PagesBefore.Add(47);
        printingRules.Add(61, rule1);

        //Act
        var result = Day05.GetFixedUpdateMiddlePage(pages, printingRules);

        //Assert
        result.Should().Be(47);
    }
}
