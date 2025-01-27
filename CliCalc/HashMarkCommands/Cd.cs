// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using CliCalc.Domain;
using CliCalc.Engine;
using CliCalc.Interfaces;

using Spectre.Console;

namespace CliCalc.HashMarkCommands;

internal sealed class Cd : IHashMarkCommand
{
    public string Name => "cd";

    public string Description => "Change current working directory";

    public Task<HashMarkResult> ExecuteAsync(Arguments args, IAnsiConsole ansiConsole, IMediator mediator, CancellationToken cancellationToken)
    {
        string currentDirectory = mediator.Request<string>(MessageTypes.DataSets.Workdir);

        if (args.Count == 0)
        {
            return Task.FromResult(new HashMarkResult(currentDirectory));
        }

        if (args[0] == "..")
        {
            var path = Path.GetDirectoryName(currentDirectory);
            if (path != null)
            {
                mediator.Notify(new MessageTypes.ChangeWorkdir { Path = path });
            }
        }
        else
        {
            var path = Path.Combine(currentDirectory, args[0]);
            if (Directory.Exists(path))
            {
                mediator.Notify(new MessageTypes.ChangeWorkdir { Path = path });
            }
            else
            {
                return Task.FromResult(new HashMarkResult($"Directory '{path}' not found"));
            }
        }

        return Task.FromResult(new HashMarkResult());
    }
}
