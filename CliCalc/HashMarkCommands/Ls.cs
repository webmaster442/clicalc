using CliCalc.Domain;
using CliCalc.Engine;
using CliCalc.Interfaces;

using Spectre.Console;

namespace CliCalc.HashMarkCommands;

internal sealed class Ls : IHashMarkCommand
{
    public string Name => "ls";

    public string Description => "List current directory files";

    public Task<HashMarkResult> ExecuteAsync(Arguments args, IAnsiConsole ansiConsole, IMediator mediator, CancellationToken cancellationToken)
    {
        string currentDirectory = mediator.Request<string>(MessageTypes.DataSets.Workdir);
        var directories = Directory.GetDirectories(currentDirectory).Select(x => Path.GetFileName(x));
        var files = Directory.GetFiles(currentDirectory).Select(x => Path.GetFileName(x));

        ansiConsole.MarkupLine($"[bold]Directories:[/]");
        foreach (var directory in directories)
        {
            ansiConsole.MarkupLine($"  [cyan]{directory.EscapeMarkup()}[/]");
        }

        ansiConsole.WriteLine();
        ansiConsole.MarkupLine($"[bold]Files:[/]");
        foreach (var file in files)
        {
            ansiConsole.MarkupLine($"  [yellow]{file.EscapeMarkup()}[/]");
        }

        return Task.FromResult(new HashMarkResult());
    }
}
