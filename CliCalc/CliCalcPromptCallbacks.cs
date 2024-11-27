using System.Collections.Immutable;
using System.Xml.Serialization;

using CliCalc.Domain.XmlDoc;
using CliCalc.DomainServices;
using CliCalc.Interfaces;

using PrettyPrompt;
using PrettyPrompt.Completion;
using PrettyPrompt.Consoles;
using PrettyPrompt.Documents;
using PrettyPrompt.Highlighting;

namespace CliCalc;

internal sealed class CliCalcPromptCallbacks : PromptCallbacks
{
    private readonly Dictionary<string, string> _hashMarks;
    private readonly Dictionary<string, string> _globalMembers;
    private readonly ImmutableArray<CharacterSetModificationRule> _hasmarkRules;
    private readonly ImmutableArray<CharacterSetModificationRule> _functionRules;

    public CliCalcPromptCallbacks(IReadOnlyDictionary<string, IHashMarkCommand> commands)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(XmlDoc), new XmlRootAttribute("doc"));
        var file = Path.Combine(AppContext.BaseDirectory, "CliCalc.Functions.xml");
        XmlDoc document;
        using (var stream = File.OpenRead(file))
        {
            document = serializer.Deserialize(stream) as XmlDoc
                ?? new XmlDoc();
        }
        _globalMembers = document.Members
            .Where(m => m.IsMethod() || m.IsProperty())
            .ToDictionary(m => m.GetName(), m => m.GetDocumentation());

        _hashMarks = commands.ToDictionary(x => x.Key.Substring(1), x => x.Value.Description);
        _hasmarkRules = new[] { new CharacterSetModificationRule(CharacterSetModificationKind.Remove, new[] { '#' }.ToImmutableArray()) }.ToImmutableArray();
        _functionRules = new[] { new CharacterSetModificationRule(CharacterSetModificationKind.Add, new[] { ' ' }.ToImmutableArray()) }.ToImmutableArray();
    }

    protected override Task<IReadOnlyList<CompletionItem>> GetCompletionItemsAsync(string text,
                                                                                   int caret,
                                                                                   TextSpan spanToBeReplaced,
                                                                                   CancellationToken cancellationToken)
    {
        var typedWord = text.AsSpan(spanToBeReplaced.Start, spanToBeReplaced.Length).ToString();
        return text.StartsWith('#')
            ? ItemsFromDictionary(typedWord, _hashMarks, _hasmarkRules)
            : ItemsFromDictionary(typedWord, _globalMembers, _functionRules);
    }

    private static Task<IReadOnlyList<CompletionItem>> ItemsFromDictionary(string typedWord,
                                                                           IEnumerable<KeyValuePair<string, string>> hashMarks,
                                                                           ImmutableArray<CharacterSetModificationRule> rules)
    {
        static string GetReplaceText(string key)
        {
            return key.Contains('(') 
                ? key[..(key.IndexOf('(') + 1)]
                : key;
        }

        var list = hashMarks
            .Where(x => x.Key.StartsWith(typedWord, StringComparison.OrdinalIgnoreCase))
            .Select(x => new CompletionItem(replacementText: GetReplaceText(x.Key),
                                            displayText: x.Key,
                                            getExtendedDescription: (ct) => Task.FromResult(new FormattedString(x.Value)),
                                            commitCharacterRules: rules))
            .ToList();

        return Task.FromResult<IReadOnlyList<CompletionItem>>(list);
    }
}
