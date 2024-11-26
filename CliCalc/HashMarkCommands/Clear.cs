using CliCalc.Domain;
using CliCalc.Interfaces;

using Spectre.Console;

namespace CliCalc.HashMarkCommands;

internal sealed class Clear : IHashMarkCommand
{
    public string Name => "clear";

    public string Description => "Clears the console window";

    public Task<HashMarkResult> ExecuteAsync(IAnsiConsole ansiConsole, IMediator mediator, CancellationToken cancellationToken)
    {
        ansiConsole.Clear();
        return Task.FromResult(new HashMarkResult());
    }
}
