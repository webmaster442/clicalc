using CliCalc.Domain;
using CliCalc.Engine;
using CliCalc.Interfaces;

using Spectre.Console;

namespace CliCalc.HashMarkCommands;

internal sealed class Help: IHashMarkCommand
{
    public string Name => "help";

    public string Description => "lists available functions with details and the hashmark commands";

    public Task<HashMarkResult> ExecuteAsync(Arguments args, IAnsiConsole ansiConsole, IMediator mediator, CancellationToken cancellationToken)
    {
        var functions = mediator.Request<IReadOnlyDictionary<string, string>>(MessageTypes.DataSets.GlobalDocumentation);
        var hashMarks = mediator.Request<IReadOnlyDictionary<string, string>>(MessageTypes.DataSets.HasmarksDocumentation);

        var text = new FigletText("# commands");
        ansiConsole.Write(text);

        foreach (var hashMark in hashMarks)
        {
            ansiConsole.MarkupLine($"[yellow]{hashMark.Key.EscapeMarkup()}[/]");
            ansiConsole.MarkupLine($"[italic]{hashMark.Value.EscapeMarkup()}[/]");
            ansiConsole.WriteLine();
        }

        text = new FigletText("Functions");
        ansiConsole.Write(text);

        foreach (var function in functions)
        {
            ansiConsole.MarkupLine($"[yellow]{function.Key.EscapeMarkup()}[/]");
            ansiConsole.MarkupLine($"[italic]{function.Value.EscapeMarkup()}[/]");
            ansiConsole.WriteLine();
        }
        return Task.FromResult(new HashMarkResult());
    }
}
