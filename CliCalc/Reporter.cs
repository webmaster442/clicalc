// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using CliCalc.Functions;

using Spectre.Console;

using Webmaster442.WindowsTerminal;

namespace CliCalc;
internal class Reporter : IReporter<long>
{
#pragma warning disable Spectre1000 // Use AnsiConsole instead of System.Console
    private long _max;
    private int _lastReported =0;
    private readonly IAnsiConsole _ansiConsole;

    public Reporter(IAnsiConsole ansiConsole)
    {
        WindowsTerminal.SetProgressbar(ProgressbarState.Hidden, 0);
        _ansiConsole = ansiConsole;
    }

    public void Done()
    {
        WindowsTerminal.SetProgressbar(ProgressbarState.Hidden, 0);
        WindowsTerminal.SwitchToMainBuffer();
    }

    public void ReportCrurrent(long value)
    {
        int percent = (int)((value * 100) / _max);
        if (percent == _lastReported)
        {
            return;
        }
        _lastReported = percent;
        WindowsTerminal.SetProgressbar(ProgressbarState.Default, percent);
        int max = Console.WindowWidth;
        int count = (int)(max * value / _max);
        _ansiConsole.Write(new string('█', count));
        Console.Write('\r');
    }

    public void Start(long value)
    {
        _max = value;
        WindowsTerminal.SetProgressbar(ProgressbarState.Default, 0);
        WindowsTerminal.SwitchToAlternateBuffer();
        _ansiConsole.Clear();
    }
#pragma warning restore Spectre1000 // Use AnsiConsole instead of System.Console
}
