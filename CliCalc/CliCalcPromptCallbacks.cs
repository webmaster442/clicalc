using System.Collections.Immutable;

using CliCalc.Domain;
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
    private readonly IReadOnlyDictionary<string, string> _globalMembers;
    private readonly ImmutableArray<CharacterSetModificationRule> _hasmarkRules;
    private readonly ImmutableArray<CharacterSetModificationRule> _functionRules;
    private readonly IMediator _mediator;

    public CliCalcPromptCallbacks(IMediator mediator, IReadOnlyDictionary<string, IHashMarkCommand> commands)
    {
        _mediator = mediator;
        _globalMembers = _mediator.Request<IReadOnlyDictionary<string, string>>(MessageTypes.DataSets.GlobalDocumentation);

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

        var variables = _mediator
            .Request<IEnumerable<(string name, string typeName)>>(MessageTypes.DataSets.VariablesWithTypes)
            .Where(x => x.name.StartsWith(typedWord, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.name, x => x.typeName);

        if (variables.Count > 0)
            return ItemsFromDictionary(typedWord, variables, _functionRules);

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
            .OrderBy(x => x.Key)
            .Select(x => new CompletionItem(replacementText: GetReplaceText(x.Key),
                                            displayText: x.Key,
                                            getExtendedDescription: (ct) => Task.FromResult(new FormattedString(x.Value)),
                                            commitCharacterRules: rules))
            .ToList();

        return Task.FromResult<IReadOnlyList<CompletionItem>>(list);
    }
}
