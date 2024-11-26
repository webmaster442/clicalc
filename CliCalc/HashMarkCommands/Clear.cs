using CliCalc.Interfaces;

using Spectre.Console;

namespace CliCalc.HashMarkCommands;

internal sealed class Clear : IHashMarkCommand
{
    public string Name => "clear";

    public string Description => "Clears the console window";

    public Task<object> ExecuteAsync(IAnsiConsole ansiConsole, IAPI api, CancellationToken cancellationToken)
    {
        ansiConsole.Clear();
        return Task.FromResult(new object());
    }
}
