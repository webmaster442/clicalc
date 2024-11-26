using Spectre.Console;

namespace CliCalc.Interfaces;

internal interface IHashMarkCommand
{
    string Name { get; }
    string Description { get; }
    Task<object> ExecuteAsync(IAnsiConsole ansiConsole, IAPI api, CancellationToken cancellationToken);
}
