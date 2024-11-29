using System.Globalization;

using CliCalc.Functions;
using CliCalc.Interfaces;

namespace CliCalc.Engine.Formatters;

internal sealed class FormattableFormatter : IObjectFormatter
{
    public bool TryFormat(object value, CultureInfo culture, AngleMode angleMode, out string? formattedValue)
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
