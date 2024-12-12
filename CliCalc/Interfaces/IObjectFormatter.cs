using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using CliCalc.Functions;

namespace CliCalc.Interfaces;

internal interface IObjectFormatter
{
    bool TryFormat(object value, CultureInfo culture, AngleMode angleMode, [NotNullWhen(true)] out string? formattedValue);
}
