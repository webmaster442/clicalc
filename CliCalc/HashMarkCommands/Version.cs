// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using CliCalc.Domain;
using CliCalc.Engine;
using CliCalc.Interfaces;

using Spectre.Console;

namespace CliCalc.HashMarkCommands;

internal sealed class Version : IHashMarkCommand
{
    public string Name => "version";

    public string Description => "Prints the version of the application";

    public Task<HashMarkResult> ExecuteAsync(Arguments args, IAnsiConsole ansiConsole, IMediator mediator, CancellationToken cancellationToken)
    {
        System.Version? assemblyVersion = typeof(Version).Assembly.GetName().Version;

        if (assemblyVersion == null)
            return Task.FromResult(new HashMarkResult("Version not found"));

        ansiConsole.WriteLine($"Version: {assemblyVersion}");

        return Task.FromResult(new HashMarkResult());
    }
}
