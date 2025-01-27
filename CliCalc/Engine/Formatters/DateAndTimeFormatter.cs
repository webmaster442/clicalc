// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using CliCalc.Functions;
using CliCalc.Interfaces;

namespace CliCalc.Engine.Formatters;

internal sealed class DateAndTimeFormatter : IObjectFormatter
{
    public bool TryFormat(object value, CultureInfo culture, AngleMode angleMode, [NotNullWhen(true)] out string? formattedValue)
    {
        if (value is DateTime dateTime)
        {
            if (dateTime.Year != 1
                || dateTime.Month != 1
                || dateTime.Day != 1)
            {
                formattedValue = dateTime.ToString("F", culture);
                return true;
            }

            formattedValue = dateTime.ToString("T", culture);
            return true;
        }
        else if (value is TimeSpan timeSpan)
        {
            formattedValue = timeSpan.ToString("T", culture);
            return true;
        }

        formattedValue = null;
        return false;
    }
}
