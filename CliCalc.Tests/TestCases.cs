namespace CliCalc.Tests;

internal static class TestCases
{
    public static IEnumerable<TestCaseData> Expressions
    {
        get
        {
            yield return new TestCaseData("1000+1000", "2,000\n");
            yield return new TestCaseData("Fraction(1,3)+Fraction(1,3)", "2\n─\n3\n\n");
            yield return new TestCaseData("Abs(-1)", "1\n");
            yield return new TestCaseData("Sin(90)", "1\n");
            yield return new TestCaseData("Cos(0)", "1\n");
            yield return new TestCaseData("Tan(45)", "0.9999999999999998889777\n");
            yield return new TestCaseData("Log(10)", "2.3025850929940459010936\n");
            yield return new TestCaseData("Log(100,10)", "2\n");
            yield return new TestCaseData("Ceiling(1.25)", "2\n");
            yield return new TestCaseData("Floor(1.75)", "1\n");
            yield return new TestCaseData("Round(1.75)", "2\n");
            yield return new TestCaseData("Round(1.75,1)", "1.8000000000000000444089\n");

        }
    }
}
