// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using CliCalc.Functions;

namespace CliCalc.Interfaces;

internal interface IObjectFormatter
{
    bool TryFormat(object value, CultureInfo culture, AngleMode angleMode, [NotNullWhen(true)] out string? formattedValue);
}
