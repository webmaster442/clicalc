using CliCalc.Domain;
using CliCalc.Engine;

using Spectre.Console;

namespace CliCalc.Interfaces;

internal interface IHashMarkCommand
{
    string Name { get; }
    string Description { get; }
    Task<HashMarkResult> ExecuteAsync(Arguments args, IAnsiConsole ansiConsole, IMediator mediator, CancellationToken cancellationToken);
}
