using System.Globalization;

namespace CliCalc.Interfaces;

internal interface IObjectFormatter
{
    bool TryFormat(object value, CultureInfo culture, out string? formattedValue);
}
