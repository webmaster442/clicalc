using System.Diagnostics;

using CliCalc.Domain;
using CliCalc.Engine;
using CliCalc.Interfaces;

using Spectre.Console;

namespace CliCalc.HashMarkCommands;

internal class Web : IHashMarkCommand
{
    public string Name => "web";

    public string Description => "Open program website";

    public Task<HashMarkResult> ExecuteAsync(Arguments args, IAnsiConsole ansiConsole, IMediator mediator, CancellationToken cancellationToken)
    {
        using var process = new Process();
        process.StartInfo.FileName = "https://github.com/webmaster442/clicalc";
        process.StartInfo.UseShellExecute = true;
        process.Start();
        return Task.FromResult(new HashMarkResult());
    }
}
