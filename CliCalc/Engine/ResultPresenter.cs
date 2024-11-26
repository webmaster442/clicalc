using System.Globalization;

using CliCalc.Domain;
using CliCalc.Engine.Formatters;
using CliCalc.Interfaces;

using Spectre.Console;

namespace CliCalc.Engine;

internal class ResultPresenter : INotifyable<MessageTypes.CultureChange>
{
    private readonly IObjectFormatter[] _formatters;
    private readonly IAnsiConsole _console;

    public CultureInfo Culture { get; set; }

    public ResultPresenter(IAnsiConsole console)
    {
        _formatters =[
            new NumberFormatter(),
            new OverridenToStringFormatter(),
            new ObjectMembersFormatter(),
        ];
        Culture = CultureInfo.CurrentUICulture;
        _console = console;
    }

    public void Display(Result result)
    {
        result.Handle(Success, Failure);
    }

    private void Failure(Exception exception)
        => _console.MarkupLine($"[bold red]{exception.Message}[/]");

    private void Success(object? obj)
    {
        if (obj == null)
            return;

        foreach (var formatter in _formatters)
        {
            if (formatter.TryFormat(obj, Culture, out string? formatted))
            {
                _console.MarkupLine($"[green]{formatted}[/]");
                return;
            }
        }
    }

    void INotifyable<MessageTypes.CultureChange>.OnNotify(MessageTypes.CultureChange message) 
        => Culture = message.CultureInfo;
}
