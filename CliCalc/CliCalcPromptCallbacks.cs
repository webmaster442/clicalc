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
    private readonly Dictionary<string, AnsiColor> _highlighting;

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

        _highlighting = _globalMembers
            .Where(x => x.Key.Contains('('))
            .Select(x => x.Key.Substring(0, x.Key.IndexOf('(')))
            .Distinct()
            .ToDictionary(x => x, _ => AnsiColor.Cyan);
        AddToHighLight(AnsiColor.Yellow, "+", "-", "*", "/", "%", "<", ">", "=", "~", "^", "|", "&", "?", ":");
        AddToHighLight(AnsiColor.BrightGreen,
                      "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked",
                      "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else",
                      "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for",
                      "foreach", "goto", "if", "implicit", "int", "interface", "internal", "is", "lock",
                      "long", "namespace", "new", "null", "object", "operator", "out", "override", "params",
                      "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short",
                      "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true",
                      "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual",
                      "void", "volatile", "var", "record", "while");

    }

    private void AddToHighLight(AnsiColor color, params string[] strs)
    {
        foreach (var str in strs)
        {
            _highlighting.Add(str, color);
        }
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

    protected override Task<IReadOnlyCollection<FormatSpan>> HighlightCallbackAsync(string text, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<FormatSpan> spans = EnumerateFormatSpans(text).ToArray();
        return Task.FromResult(spans);
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

    private IEnumerable<FormatSpan> EnumerateFormatSpans(string text)
    {
        foreach (var format in _highlighting)
        {
            int startIndex;
            int offset = 0;
            while ((startIndex = text.AsSpan(offset).IndexOf(format.Key)) != -1)
            {
                yield return new FormatSpan(offset + startIndex, format.Key.Length, format.Value);
                offset += startIndex + format.Key.Length;
            }
        }
    }
}
