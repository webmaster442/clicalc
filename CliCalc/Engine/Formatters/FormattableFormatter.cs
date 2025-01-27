// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using CliCalc.Functions;
using CliCalc.Interfaces;

namespace CliCalc.Engine.Formatters;

internal sealed class FormattableFormatter : IObjectFormatter
{
    public bool TryFormat(object value, CultureInfo culture, AngleMode angleMode, [NotNullWhen(true)] out string? formattedValue)
    {
        if (value is IFormattable formattable)
        {
            formattedValue = formattable.ToString(null, culture);
            return true;
        }
        formattedValue = null;
        return false;
    }
}
