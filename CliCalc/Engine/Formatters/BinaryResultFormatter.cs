using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

using CliCalc.Functions;
using CliCalc.Interfaces;

namespace CliCalc.Engine.Formatters;

internal sealed class BinaryResultFormatter : IObjectFormatter
{
    public bool TryFormat(object value, CultureInfo culture, AngleMode angleMode, [NotNullWhen(true)] out string? formattedValue)
    {
        if (value is not BinaryResult binaryResult)
        {
            formattedValue = null;
            return false;
        }

        StringBuilder result = new StringBuilder();
        result.AppendFormat("Decimal: {0}",
                            binaryResult.IsLong
                            ? NumberFormatter.FormatFloat(binaryResult.DataAsLong, culture)
                            : NumberFormatter.FormatFloat(binaryResult.DataAsDouble, culture))
            .AppendLine()
            .Append("Hex: ");

        for (int i=binaryResult.Data.Length - 1; i >= 0; i--)
        {
            result.Append($"{binaryResult.Data[i].ToString("X2").PadLeft(8, ' ')}").Append(' ');
        }
        result.AppendLine();

        result.Append("Bin: ");

        int dataTypeBytes = 0;
        int requiredBits = 0;
        for (int i = binaryResult.Data.Length - 1; i >= 0; i--)
        {
            var part = Convert.ToString(binaryResult.Data[i], 2);
            result.Append($"{part.PadLeft(8, '0')} ");
            if (binaryResult.Data[i] != 0)
            {
                dataTypeBytes += 1;
                requiredBits += part.Length;
            }
        }
        result.AppendLine();
        result.AppendLine($"Data type bytes: {dataTypeBytes}");
        result.AppendLine($"Required bits: {requiredBits}");
        formattedValue = result.ToString();
        return true;
    }
}
