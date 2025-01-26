namespace CliCalc.Tests;

internal static class TestCases
{
    public static IEnumerable<TestCaseData> Expressions
    {
        get
        {
            yield return new TestCaseData("Fraction(1,3)", "1\n─\n3\n\n");
            yield return new TestCaseData("1000+1000", "2,000\n");
            yield return new TestCaseData("Fraction(1,3)+Fraction(1,3)", "2\n─\n3\n\n");
            yield return new TestCaseData("Abs(-1)", "1\n");
            yield return new TestCaseData("Sin(90)", "1\n");
            yield return new TestCaseData("Cos(0)", "1\n");
            yield return new TestCaseData("Tan(45)", "1\n");
            yield return new TestCaseData("Log(10)", "2.30258509299404590109\n");
            yield return new TestCaseData("Log(100,10)", "2\n");
            yield return new TestCaseData("Ceiling(1.25)", "2\n");
            yield return new TestCaseData("Floor(1.75)", "1\n");
            yield return new TestCaseData("Round(1.75)", "2\n");
            yield return new TestCaseData("Round(1.75,1)", "1.8\n");
            yield return new TestCaseData("Milli*26", "0.026\n");
            yield return new TestCaseData("Micro*26", "0.000026\n");
            yield return new TestCaseData("Nano*26", "0.000000026\n");
            yield return new TestCaseData("Pico*26", "0.000000000026\n");
            yield return new TestCaseData("Femto*8", "0.000000000000008\n");
            yield return new TestCaseData("Kilo*8", "8,000\n");
            yield return new TestCaseData("Mega*8", "8,000,000\n");
            yield return new TestCaseData("0.1+0.1+0.1", "0.3\n");

        }
    }
}
