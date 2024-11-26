using System.Collections.Immutable;
using System.Xml.Serialization;

using CliCalc.Domain.XmlDoc;
using CliCalc.Interfaces;

using PrettyPrompt;
using PrettyPrompt.Completion;
using PrettyPrompt.Consoles;
using PrettyPrompt.Documents;

namespace CliCalc.Engine;

internal sealed class EnginePromptCallbacks : PromptCallbacks
{
    private readonly XmlDoc _xmlDoc;
    private readonly Dictionary<string, string> _hashMarks = new();

    public EnginePromptCallbacks(IReadOnlyDictionary<string, IHashMarkCommand> commands)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(XmlDoc), new XmlRootAttribute("doc"));
        var file = Path.Combine(AppContext.BaseDirectory, "CliCalc.Functions.xml");
        using (var stream = File.OpenRead(file))
        {
            _xmlDoc = serializer.Deserialize(stream) as XmlDoc
                ?? new XmlDoc();
        }
        foreach (var command in commands)
        {
            _hashMarks.Add(command.Key.Substring(1), command.Value.Description);
        }
    }

    protected override Task<IReadOnlyList<CompletionItem>> GetCompletionItemsAsync(string text, int caret, TextSpan spanToBeReplaced, CancellationToken cancellationToken)
    {
        var typedWord = text.AsSpan(spanToBeReplaced.Start, spanToBeReplaced.Length).ToString();


        if (text.StartsWith('#'))
        {

            return Task.FromResult<IReadOnlyList<CompletionItem>>(_hashMarks
                .Where(x => x.Key.StartsWith(typedWord, StringComparison.OrdinalIgnoreCase))
                .Select(x => new CompletionItem(replacementText: x.Key,
                                                displayText: x.Value,
                                                commitCharacterRules: new[] { new CharacterSetModificationRule(CharacterSetModificationKind.Remove, new[] { '#' }.ToImmutableArray()) }.ToImmutableArray()))
                .ToList());
        }
        else
        {
            return Task.FromResult<IReadOnlyList<CompletionItem>>(new List<CompletionItem>());
        }
    }
}
