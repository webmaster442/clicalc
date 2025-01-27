// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using CliCalc.Functions;

using Spectre.Console;

namespace CliCalc;
internal class Reporter : IReporter<long>
{
#pragma warning disable Spectre1000 // Use AnsiConsole instead of System.Console
    private long _max;
    private int _lastReported =0;
    private readonly IAnsiConsole _ansiConsole;

    public Reporter(IAnsiConsole ansiConsole)
    {
        HideTerminalProgress();
        _ansiConsole = ansiConsole;
    }

    private void ReportToTerminal(int percent)
    {
        Console.Write($"\e]9;4;1;{percent}\x07");

    }

    private void HideTerminalProgress()
    {
        Console.Write("\e]9;4;0;0\x07");
    }

    public void Done()
    {
        HideTerminalProgress();
        _ansiConsole.Write("\e[?1049l"); // Switch back to the normal screen
    }

    public void ReportCrurrent(long value)
    {
        int percent = (int)((value * 100) / _max);
        if (percent == _lastReported)
        {
            return;
        }
        _lastReported = percent;
        ReportToTerminal(percent);
        int max = Console.WindowWidth;
        int count = (int)(max * value / _max);
        _ansiConsole.Write(new string('█', count));
        Console.Write('\r');
    }

    public void Start(long value)
    {
        _max = value;
        ReportToTerminal(0);
        Console.Write("\e[?1049h"); // Switch to alternate screen
        _ansiConsole.Clear();
    }
#pragma warning restore Spectre1000 // Use AnsiConsole instead of System.Console
}
