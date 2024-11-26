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

namespace CliCalc.Engine;

internal sealed class EnginePromptCallbacks : PromptCallbacks
{
    private readonly Dictionary<string, string> _hashMarks;
    private readonly Dictionary<string, string> _functions;
    private readonly ImmutableArray<CharacterSetModificationRule> _hasmarkRules;
    private readonly ImmutableArray<CharacterSetModificationRule> _functionRules;

    public EnginePromptCallbacks(IReadOnlyDictionary<string, IHashMarkCommand> commands)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(XmlDoc), new XmlRootAttribute("doc"));
        var file = Path.Combine(AppContext.BaseDirectory, "CliCalc.Functions.xml");
        XmlDoc document;
        using (var stream = File.OpenRead(file))
        {
            document = serializer.Deserialize(stream) as XmlDoc
                ?? new XmlDoc();
        }
        _functions = document.Members
            .Where(m => m.IsFunction())
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
            : ItemsFromDictionary(typedWord, _functions, _functionRules);
    }

    private static Task<IReadOnlyList<CompletionItem>> ItemsFromDictionary(string typedWord,
                                                                           Dictionary<string, string> hashMarks,
                                                                           ImmutableArray<CharacterSetModificationRule> rules)
    {
        var list = hashMarks
            .Where(x => x.Key.StartsWith(typedWord, StringComparison.OrdinalIgnoreCase))
            .Select(x => new CompletionItem(replacementText: x.Key,
                                            displayText: x.Key,
                                            getExtendedDescription: (ct) => Task.FromResult(new FormattedString(x.Value)),
                                            commitCharacterRules: rules))
            .ToList();

        return Task.FromResult<IReadOnlyList<CompletionItem>>(list);
    }
}
