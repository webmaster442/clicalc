using CliCalc.Domain;

using Spectre.Console;

namespace CliCalc.Interfaces;

internal interface IHashMarkCommand
{
    string Name { get; }
    string Description { get; }
    Task<HashMarkResult> ExecuteAsync(IAnsiConsole ansiConsole, IMediator mediator, CancellationToken cancellationToken);
}
