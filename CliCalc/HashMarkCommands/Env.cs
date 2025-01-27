// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using CliCalc.Domain;
using CliCalc.Engine;
using CliCalc.Interfaces;

using Spectre.Console;

namespace CliCalc.HashMarkCommands;

internal class Env : IHashMarkCommand
{
    public string Name => "env";
    public string Description => "Displays information about the environment.";

    public Task<HashMarkResult> ExecuteAsync(Arguments args, IAnsiConsole ansiConsole, IMediator mediator, CancellationToken cancellationToken)
    {
        var table = new Table();
        table.AddColumns("OS Version", $"{Environment.OSVersion.VersionString}");
        table.AddRow("Processor Count", $"{Environment.ProcessorCount}");
        table.AddRow("64-bit OS", $"{Environment.Is64BitOperatingSystem}");
        table.AddRow("64-bit Process", $"{Environment.Is64BitProcess}");
        table.AddRow("User Domain Name", $"{Environment.UserDomainName}");
        table.AddRow("User Name", $"{Environment.UserName}");
        table.AddRow("Machine Name", $"{Environment.MachineName}");
        ansiConsole.Write(table);
        return Task.FromResult(new HashMarkResult());
    }
}
