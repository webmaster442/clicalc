using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

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
            new DateAndTimeFormatter(),
            new FormattableFormatter(),
            new OverridenToStringFormatter(),
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

    private bool TryFormat(object obj, AngleMode angleMode, [NotNullWhen(true)] out string? formatted)
    {
        foreach (var formatter in _formatters)
        {
            if (formatter.TryFormat(obj, Culture, angleMode, out formatted))
            {
                return true;
            }
        }
        formatted = null;
        return false;
    }

    private void Failure(Exception exception)
        => _console.MarkupLine($"[bold red]{exception.Message}[/]");

    private void Success(object? obj)
    {
        AngleMode angleMode = _mediator.Request<AngleMode>(MessageTypes.DataSets.AngleMode);

        if (obj == null)
            return;

        if (TryFormat(obj, angleMode, out var formatted))
        {
            AnsiConsole.MarkupLine($"[bold green]{formatted.EscapeMarkup()}[/]");
        }
        else
        {
            //format using property display
            var properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            Table table = new Table();
            table.AddColumns("Property", "Value");
            foreach (var property in properties)
            {
                var propValue = property.GetValue(obj);
                if (propValue == null)
                {
                    table.AddRow(property.Name, "null");
                    continue;
                }
                if (TryFormat(propValue, angleMode, out string? formattedPropValue))
                {
                    table.AddRow(property.Name, formattedPropValue);
                }
                else
                {
                    table.AddRow(property.Name, propValue.ToString() ?? "null");
                }
                AnsiConsole.Write(table);
            }
        }
    }

    void INotifyable<MessageTypes.CultureChange>.OnNotify(MessageTypes.CultureChange message)
        => Culture = message.CultureInfo;
}
