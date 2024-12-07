namespace CliCalc.Tests;

internal static class TestCases
{
    public static IEnumerable<TestCaseData> Expressions
    {
        get
        {
            yield return new TestCaseData("1000+1000", "2,000\n");
            yield return new TestCaseData("Fraction(1,3)+Fraction(1,3)", "2\n─\n3\n\n");
            yield return new TestCaseData("Abs(-1)", "1");
        }
    }
}
