using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

using CliCalc.Functions;
using CliCalc.Interfaces;

namespace CliCalc.Engine.Formatters;

internal sealed class OverridenToStringFormatter : IObjectFormatter
{
    public bool TryFormat(object value, CultureInfo culture, AngleMode angleMode, [NotNullWhen(true)] out string? formattedValue)
    {
        static bool HasOverriddenToString(object obj)
        {
            Type type = obj.GetType();
            MethodInfo toStringMethod = type.GetMethod("ToString", BindingFlags.Public | BindingFlags.Instance, Type.EmptyTypes)!;
            return toStringMethod.DeclaringType != typeof(object);
        }

        if (!HasOverriddenToString(value))
        {
            formattedValue = null;
            return false;
        }

        formattedValue = value.ToString() ?? "null";
        return true;

    }
}