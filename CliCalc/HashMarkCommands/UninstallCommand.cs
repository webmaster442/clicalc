// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using CliCalc.Domain;
using CliCalc.Engine;
using CliCalc.Interfaces;

using Spectre.Console;

using Webmaster442.WindowsTerminal;

namespace CliCalc.HashMarkCommands;

internal class UninstallCommand : IHashMarkCommand
{
    public string Name => "uninstall";

    public string Description => "UnInstall calculator from Windows terminal";

    public Task<HashMarkResult> ExecuteAsync(Arguments args, IAnsiConsole ansiConsole, IMediator mediator, CancellationToken cancellationToken)
    {
        if (!WindowsTerminal.FragmentExtensions.IsFragmentInstalled(InstallCommand.AppName, InstallCommand.File))
            return Task.FromResult(new HashMarkResult("Calculator is already installed"));

        bool result = WindowsTerminal.FragmentExtensions.TryRemoveFragment(InstallCommand.AppName, InstallCommand.File);

        if (!result)
            return Task.FromResult(new HashMarkResult("Failed to uninstall calculator"));

        return Task.FromResult(new HashMarkResult());
    }
}
