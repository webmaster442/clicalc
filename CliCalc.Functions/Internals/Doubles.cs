namespace CliCalc.Functions.Internals;

internal static class Doubles
{
    public static bool IsCloseTo(double a, double b, double epsilon)
    {
        return Math.Abs(a - b) < epsilon;
    }

    public static bool IsExact(double number)
    {
        if (double.IsNaN(number) || double.IsInfinity(number))
        {
            return false;
        }

        if (number == 0)
            return true;

        long bits = BitConverter.DoubleToInt64Bits(number);
        int exponent = (int)((bits >> 52) & 0x7FF) - 1023;
        long significand = bits & 0xFFFFFFFFFFFFFL;

        if (exponent != -1023)
            significand |= (1L << 52);

        while ((significand & 1) == 0)
        {
            significand >>= 1;
        }

        return significand == 1;
    }

    public static bool IsExact(float number)
    {
        if (float.IsNaN(number) || float.IsInfinity(number))
        {
            return false;
        }
        if (number == 0)
            return true;

        int bits = BitConverter.SingleToInt32Bits(number);
        int exponent = (bits >> 23) & 0xFF - 127;
        int significand = bits & 0x7FFFFF;

        if (exponent != -127)
            significand |= (1 << 23);

        while ((significand & 1) == 0)
        {
            significand >>= 1;
        }
        return significand == 1;
    }

    public static IEnumerable<double> Range(double start, int count, Func<double, double> next)
    {
        double current = start;
        for (int i = 0; i < count; i++)
        {
            yield return current;
            current = next(current);
        }
    }
}
