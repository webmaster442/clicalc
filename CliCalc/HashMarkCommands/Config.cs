// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using System.Diagnostics;

using CliCalc.Domain;
using CliCalc.Engine;
using CliCalc.Infrastructure;
using CliCalc.Interfaces;

using Spectre.Console;

namespace CliCalc.HashMarkCommands;

internal sealed class Config : IHashMarkCommand
{
    public string Name => "config";

    public string Description => "open configuration file";

    public async Task<HashMarkResult> ExecuteAsync(Arguments args, IAnsiConsole ansiConsole, IMediator mediator, CancellationToken cancellationToken)
    {
        static void LaunchForEdit(string file)
        {
            using var process = new Process();
            process.StartInfo.FileName = "notepad.exe";
            process.StartInfo.Arguments = file;
            process.Start();
        }

        ConfigWriter writer = new();
        if (!File.Exists(ConfigWriter.ConfigPath))
        {
            await writer.WriteAsync(new Configuration());
        }
        LaunchForEdit(ConfigWriter.ConfigPath);
        return new HashMarkResult();
    }
}
