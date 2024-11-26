using System.Text;
using System.Text.RegularExpressions;

using CliCalc.Domain.XmlDoc;

namespace CliCalc.DomainServices;
internal static partial class XmlDocExtensions
{
    [GeneratedRegex(@"M\:CliCalc\.Functions\.Global\.(.+)\(")]
    private static partial Regex MethodNameRegex();

    [GeneratedRegex(@"\s\s")]
    private static partial Regex RemoveWhiteSpaces();

    public static bool IsFunction(this DocMember member)
        => member.Name.StartsWith("M:");

    public static string GetName(this DocMember member)
    {
        var parts = MethodNameRegex().Split(member.Name);
        return parts[1];
    }

    public static string GetDocumentation(this DocMember member)
    {
        static string Cleanup(string returns)
        {
            return RemoveWhiteSpaces().Replace(returns, "").Trim();
        }

        StringBuilder sb = new();
        sb.AppendLine(member.Summary);
        if (member.Param.Length > 0)
        {
            sb.AppendLine("Parameters:\r\n");
            foreach (var param in member.Param)
            {
                sb.AppendLine($"{param.Name}: {param.Value}");
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
