// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using CliCalc.Domain;
using CliCalc.Engine;
using CliCalc.Interfaces;

using Spectre.Console;

namespace CliCalc.HashMarkCommands;

internal sealed class Variables : IHashMarkCommand
{
    public string Name => "variables";

    public string Description => "Lists all currently set variables";

    public Task<HashMarkResult> ExecuteAsync(Arguments args,
                                             IAnsiConsole ansiConsole,
                                             IMediator mediator,
                                             CancellationToken cancellationToken)
    {
        var variables = mediator.Request<IEnumerable<string>>(MessageTypes.DataSets.Variables);
        if (variables.Any())
        {
            ansiConsole.MarkupLine("Variables:");
            foreach (var variable in variables)
            {
                ansiConsole.MarkupLine($"[yellow]{variable}[/]");
            }
        }
        else
        {
            ansiConsole.MarkupLine("[yellow]No variables have been set.[/]");
        }
        return Task.FromResult(new HashMarkResult());
    }
}
