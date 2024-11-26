using System.Text.RegularExpressions;

using CliCalc.Domain.XmlDoc;

namespace CliCalc.DomainServices;
internal static partial class XmlDocExtensions
{
    [GeneratedRegex(@"M\:CliCalc\.Functions\.Global\.(.+)\(")]
    private static partial Regex MethodNameRegex(); 

    public static bool IsFunction(this DocMember member)
        => member.Name.StartsWith("M:");

    public static string GetName(this DocMember member)
    {
        var parts = MethodNameRegex().Split(member.Name);
        return parts[1];
    }

    public static string GetDocumentation(this DocMember member)
    {
        return member.Summary;
    }
}
