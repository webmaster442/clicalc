using System.Globalization;
using System.Reflection;
using System.Text;

using CliCalc.Interfaces;

namespace CliCalc.Engine.Formatters;

internal sealed class ObjectMembersFormatter : IObjectFormatter
{
    public bool TryFormat(object value, CultureInfo culture, out string? formattedValue)
    {
        var properties = value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
        StringBuilder sb = new StringBuilder();
        foreach (var property in properties)
        {
            sb.Append(property.Name);
            sb.Append(": ");

            var propValue = property.GetValue(value);
            if (propValue is IFormattable formattable)
            {
                sb.AppendLine(formattable.ToString(null, culture));
            }
            else
            {
                sb.AppendLine(propValue?.ToString() ?? "null");
            }
        }
        formattedValue = sb.ToString();
        return true;
    }
}