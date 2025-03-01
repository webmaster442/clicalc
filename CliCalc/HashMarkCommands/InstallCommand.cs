﻿// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using CliCalc.Domain;
using CliCalc.Engine;
using CliCalc.Interfaces;

using Spectre.Console;

using Webmaster442.WindowsTerminal;

namespace CliCalc.HashMarkCommands;

internal class InstallCommand : IHashMarkCommand
{
    public const string AppName = "CliCalc";
#if DEBUG
    public const string File = "CliCalc.dev.json";
#else
    public const string File = "CliCalc.json";
#endif

    public string Name => "install";

    public string Description => "Install calculator to Windows terminal";

    public async Task<HashMarkResult> ExecuteAsync(Arguments args, IAnsiConsole ansiConsole, IMediator mediator, CancellationToken cancellationToken)
    {
        if (!OperatingSystem.IsWindows())
            return new HashMarkResult("This command is only available on Windows");

        if (WindowsTerminal.FragmentExtensions.IsFragmentInstalled(AppName, File))
            return new HashMarkResult("Calculator is already installed");

        TerminalFragment fragment = CreateFragment();

        bool result = await WindowsTerminal.FragmentExtensions.TryInstallFragmentAsync(AppName, File, fragment);
        if (!result)
            return new HashMarkResult("Failed to install calculator");

        return new HashMarkResult();
    }

    private static TerminalFragment CreateFragment()
    {
        return new TerminalFragment
        {
            Profiles =
            {
                new TerminalProfile
                {
#if DEBUG
                    Name = "CliCalc (dev)",
#else
                    Name = "CliCalc",
#endif
                    CommandLine = Path.Combine(AppContext.BaseDirectory, "CliCalc.exe"),
                    StartingDirectory = EnvironmentVariables.UserProfile,
                    Icon = Path.Combine(AppContext.BaseDirectory, "192x192.png"),
                    UseAcrylic = true,
                    Hidden = false,
                    ColorScheme = TerminalSchemes.TangoDark,
                }
            }
        };
    }
}
