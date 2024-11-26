using CliCalc.Interfaces;

using Spectre.Console;

namespace CliCalc.HashMarkCommands;

internal sealed class Exit : IHashMarkCommand
{
    public string Name => "exit";

    public string Description => "Exits the application";

    public Task<object> ExecuteAsync(IAnsiConsole ansiConsole, IAPI api, CancellationToken cancellationToken)
    {
        api.Exit();
        return Task.FromResult(new object());
    }
}