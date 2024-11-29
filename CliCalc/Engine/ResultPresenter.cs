using System.Globalization;

using CliCalc.Domain;
using CliCalc.Engine.Formatters;
using CliCalc.Functions;
using CliCalc.Interfaces;

using Spectre.Console;

namespace CliCalc.Engine;

internal class ResultPresenter : INotifyable<MessageTypes.CultureChange>
{
    private readonly IObjectFormatter[] _formatters;
    private readonly Mediator _mediator;
    private readonly IAnsiConsole _console;

    public CultureInfo Culture { get; set; }

    public ResultPresenter(Mediator mediator, IAnsiConsole console)
    {
        _formatters =[
            new NumberFormatter(),
            new ComplexNumberFormatter(),
            new FormattableFormatter(),
            new OverridenToStringFormatter(),
            new ObjectMembersFormatter(),
        ];
        Culture = CultureInfo.CurrentUICulture;
        _mediator = mediator;
        _console = console;
        _mediator.Register(this);
    }

    public void Display(Result result)
    {
        result.Handle(Success, Failure);
    }

    private void Failure(Exception exception)
        => _console.MarkupLine($"[bold red]{exception.Message}[/]");

    private void Success(object? obj)
    {
        AngleMode angleMode = _mediator.Request<AngleMode>(MessageTypes.DataSets.AngleMode);

        if (obj == null)
            return;

        foreach (var formatter in _formatters)
        {
            if (formatter.TryFormat(obj, Culture, angleMode, out string? formatted))
            {
                _console.MarkupLine($"[green]{formatted}[/]");
                return;
            }
        }
    }

    void INotifyable<MessageTypes.CultureChange>.OnNotify(MessageTypes.CultureChange message)
        => Culture = message.CultureInfo;
}
