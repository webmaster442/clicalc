namespace CliCalc.Functions.Internals;

internal static class Integers
{
    public static bool IsPrime(long number)
    {
        if (number <= 1) return false;
        if (number <= 3) return true;
        if (number % 2 == 0 || number % 3 == 0) return false;

        for (long i = 5; i * i <= number; i += 6)
        {
            if (number % i == 0 || number % (i + 2) == 0) return false;
        }
        return true;
    }

    public static Int128 Factorial(int number)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(number, 0, nameof(number));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(number, 33, nameof(number));
        Int128 result = 1;
        for (int i = 1; i <= number; i++)
        {
            result *= i;
        }
        return result;
    }
}
