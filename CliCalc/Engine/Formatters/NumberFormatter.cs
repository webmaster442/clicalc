// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;

using CliCalc.Functions;
using CliCalc.Interfaces;

namespace CliCalc.Engine.Formatters;

internal sealed partial class NumberFormatter : IObjectFormatter
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
        int digits = GetDigits(formattable);

        return formattable.ToString($"N{digits}", culture)
            .TrimEnd('0')
            .TrimEnd(culture.NumberFormat.NumberDecimalSeparator[0]);
    }

    private static int GetDigits(IFormattable formattable)
    {
        const int maxDigits = 20;
        const int zeroDigitCount = 3;
        string[] str = formattable.ToString("N25", CultureInfo.InvariantCulture).Split('.');
        if (str.Length == 1)
            return 0;

        var match = DigitWith3LeadingZeros().Match(str[1]);
        return match.Success ? (match.Index + zeroDigitCount) : maxDigits;
    }

    [GeneratedRegex("[1-9]000", RegexOptions.Singleline, 2000)]
    private static partial Regex DigitWith3LeadingZeros();
}
