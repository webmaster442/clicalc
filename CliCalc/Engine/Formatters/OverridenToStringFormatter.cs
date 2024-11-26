using System.Globalization;
using System.Reflection;

using CliCalc.Interfaces;

namespace CliCalc.Engine.Formatters;

internal sealed class OverridenToStringFormatter : IObjectFormatter
{
    public bool TryFormat(object value, CultureInfo culture, out string? formattedValue)
    {
        static bool HasOverriddenToString(object obj)
        {
            Type type = obj.GetType();
            MethodInfo toStringMethod = type.GetMethod("ToString", BindingFlags.Public | BindingFlags.Instance)!;
            return toStringMethod.DeclaringType != typeof(object);
        }

        if (!HasOverriddenToString(value))
        {
            formattedValue = null;
            return false;
        }

        formattedValue = value.ToString();
        return true;

    }
}