﻿using CliCalc.Domain;
using CliCalc.Engine;
using CliCalc.Interfaces;

using Spectre.Console;

namespace CliCalc.HashMarkCommands;

internal sealed class Exit : IHashMarkCommand
{
    public string Name => "exit";

    public string Description => "Exits the application";

    public Task<HashMarkResult> ExecuteAsync(Arguments args,
                                             IAnsiConsole ansiConsole,
                                             IMediator mediator,
                                             CancellationToken cancellationToken)
    {
        mediator.Notify(new MessageTypes.ExitMessage());
        return Task.FromResult(new HashMarkResult());
    }
}