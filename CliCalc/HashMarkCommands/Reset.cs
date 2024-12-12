﻿using CliCalc.Domain;
using CliCalc.Engine;
using CliCalc.Interfaces;

using Spectre.Console;

namespace CliCalc.HashMarkCommands;

internal sealed class Reset : IHashMarkCommand
{
    public string Name => "reset";

    public string Description => "reset calculator state";

    public async Task<HashMarkResult> ExecuteAsync(Arguments args, IAnsiConsole ansiConsole, IMediator mediator, CancellationToken cancellationToken)
    {
        await mediator.NotifyAsync(new MessageTypes.ResetMessage());
        return new HashMarkResult();
    }
}