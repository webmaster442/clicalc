// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using System.Text;
using System.Text.RegularExpressions;

using CliCalc.Domain.XmlDoc;

namespace CliCalc.DomainServices;
internal static partial class XmlDocExtensions
{
    private static readonly Dictionary<string, string> _typeNameReplacements = new()
    {
        { "System.Double", "double" },
        { "System.Single", "float" },
        { "System.Half", "Half" },
        { "System.Decimal", "decimal" },
        { "System.Int32", "int" },
        { "System.Int64", "long" },
        { "System.Int128", "Int128" },
        { "System.DateTime", "DateTime" },
        { "System.TimeSpan", "TimeSpan" },
        { "System.String", "string" },
        { "System.Numerics.Complex", "Complex" },
        { "System.Guid", "Guid" },
        { "CliCalc.Functions.File", "File"  },
        { "CliCalc.Functions.Fraction", "Fraction"  }
    };

    [GeneratedRegex(@"^(M|P)(\:CliCalc\.Functions\.Global\.)(.+)$", RegexOptions.None, 2000)]
    private static partial Regex MethodNameRegex();

    [GeneratedRegex(@"\s\s", RegexOptions.None, 2000)]
    private static partial Regex RemoveWhiteSpaces();

    public static bool IsMethod(this DocMember member)
        => member.Name.StartsWith('M') && !member.Name.Contains("#ctor");

    public static bool IsProperty(this DocMember member)
        => member.Name.StartsWith('P');

    public static string GetName(this DocMember member)
    {
        static string CorrectTypeNames(string name)
        {
            StringBuilder str = new StringBuilder(name);
            foreach (var replacement in _typeNameReplacements)
            {
                str.Replace(replacement.Key, replacement.Value);
            }
            return str.ToString();
        }

        var parts = MethodNameRegex().Split(member.Name);
        return CorrectTypeNames(parts[^2]);
    }

    public static string GetDocumentation(this DocMember member)
    {
        static string Cleanup(string returns)
        {
            return RemoveWhiteSpaces().Replace(returns, "").Trim();
        }

        StringBuilder sb = new();
        sb.AppendLine(Cleanup(member.Summary));

        if (member.Name.StartsWith('P'))
            return sb.ToString();

        if (member.Param != null && member.Param.Length > 0)
        {
            sb.AppendLine("Parameters:\r\n");
            foreach (var param in member.Param)
            {
                sb.AppendLine($"{param.Name}: {Cleanup(param.Value)}");
            }
            sb.AppendLine();
        }
        if (!string.IsNullOrWhiteSpace(member.Returns))
        {
            sb.AppendLine("Returns:\r\n");
            sb.AppendLine($"{Cleanup(member.Returns)}");
        }
        return sb.ToString();
    }
}
