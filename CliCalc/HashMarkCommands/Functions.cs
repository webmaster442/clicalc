// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using CliCalc.Domain;
using CliCalc.Engine;
using CliCalc.Interfaces;

using Spectre.Console;

namespace CliCalc.HashMarkCommands;

internal sealed class Functions : IHashMarkCommand
{
    public string Name => "functions";

    public string Description => "lists available function names";

    public Task<HashMarkResult> ExecuteAsync(Arguments args, IAnsiConsole ansiConsole, IMediator mediator, CancellationToken cancellationToken)
    {
        var functions = mediator.Request<IReadOnlyDictionary<string, string>>(MessageTypes.DataSets.GlobalDocumentation);
        foreach (var function in functions)
        {
            ansiConsole.MarkupLine($"[yellow]{function.Key.EscapeMarkup()}[/]");
        }
        return Task.FromResult(new HashMarkResult());
    }
}
