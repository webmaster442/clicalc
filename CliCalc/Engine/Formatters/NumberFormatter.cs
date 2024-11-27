using System.Globalization;

using CliCalc.Interfaces;

namespace CliCalc.Engine.Formatters;

internal sealed class NumberFormatter : IObjectFormatter
{
    public bool TryFormat(object value, CultureInfo culture, out string? formattedValue)
    {
        static bool CanFormat(object value)
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
                or UInt128
                or Half
                or float
                or double
                or decimal;
        }

        if (CanFormat(value)
            && value is IFormattable formattable)
        {
            formattedValue = formattable.ToString("N", culture);
            return true;
        }
        formattedValue = null;
        return false;
    }
}
