using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using CliCalc.Functions;
using CliCalc.Functions.Internals;
using CliCalc.Interfaces;

namespace CliCalc.Engine.Formatters;

internal sealed class NumberFormatter : IObjectFormatter
{
    public bool TryFormat(object value, CultureInfo culture, AngleMode angleMode, [NotNullWhen(true)] out string? formattedValue)
    {
        static bool IsInteger(object value)
        {
            return value is byte
                or sbyte
                or short
                or ushort
                or int
                or uint
                or long
                or ulong
                or Int128
                or UInt128;
        }

        static bool IsFloat(object value)
        {
            return value is
                Half
                or float
                or double
                or decimal;
        }

        if (value is Fraction fraction)
        {
            formattedValue = fraction.ToString("N0", culture);
            return true;
        }


        if ((IsInteger(value) || IsFloat(value))
            && value is IFormattable formattable)
        {
            formattedValue = IsInteger(value)
                ? formattable.ToString("N0", culture)
                : FormatFloat(formattable, culture);

            return true;
        }
        formattedValue = null;
        return false;
    }

    public static string FormatFloat(IFormattable formattable, CultureInfo culture)
    {
        return formattable.ToString("N14", culture)
            .TrimEnd('0')
            .TrimEnd(culture.NumberFormat.NumberDecimalSeparator[0]);
    }
}
