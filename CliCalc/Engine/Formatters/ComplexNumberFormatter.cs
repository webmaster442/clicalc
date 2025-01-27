// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Text;

using CliCalc.Functions;
using CliCalc.Functions.Internals;
using CliCalc.Interfaces;

namespace CliCalc.Engine.Formatters;

internal sealed class ComplexNumberFormatter : IObjectFormatter
{
    public bool TryFormat(object value, CultureInfo culture, AngleMode angleMode, [NotNullWhen(true)] out string? formattedValue)
    {
        static string ConvertAngle(double angle, CultureInfo culture, AngleMode angleMode)
        {
            return angleMode switch
            {
                AngleMode.Deg => $"{NumberFormatter.FormatFloat(Trigonometry.RadToDeg(angle), culture)} deg",
                AngleMode.Grad => $"{NumberFormatter.FormatFloat(Trigonometry.RadToGrad(angle), culture)} grad",
                _ => $"{NumberFormatter.FormatFloat(angle, culture)} rad",
            };
        }

        if (value is not Complex complex)
        {
            formattedValue = null;
            return false;
        }

        StringBuilder result = new();
        result.AppendLine($"{NumberFormatter.FormatFloat(complex.Real, culture)} + {NumberFormatter.FormatFloat(complex.Imaginary, culture)}i");
        result.Append($"r: {NumberFormatter.FormatFloat(complex.Magnitude, culture)} phi: {ConvertAngle(complex.Phase, culture, angleMode)}");
        formattedValue = result.ToString();
        return true;

    }
}