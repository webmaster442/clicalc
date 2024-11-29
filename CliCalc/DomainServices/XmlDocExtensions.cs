using System.Text;
using System.Text.RegularExpressions;

using CliCalc.Domain.XmlDoc;

namespace CliCalc.DomainServices;
internal static partial class XmlDocExtensions
{
    [GeneratedRegex(@"^(M|P)(\:CliCalc\.Functions\.Global\.)(.+)$")]
    private static partial Regex MethodNameRegex();

    [GeneratedRegex(@"\s\s")]
    private static partial Regex RemoveWhiteSpaces();

    public static bool IsMethod(this DocMember member)
        => member.Name.StartsWith('M');

    public static bool IsProperty(this DocMember member)
        => member.Name.StartsWith('P');

    public static string GetName(this DocMember member)
    {
        var parts = MethodNameRegex().Split(member.Name);
        return parts[^2];
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
