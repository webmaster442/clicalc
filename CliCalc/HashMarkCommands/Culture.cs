using System.Globalization;

using CliCalc.Domain;
using CliCalc.Engine;
using CliCalc.Interfaces;

using Spectre.Console;

namespace CliCalc.HashMarkCommands;
internal sealed class Culture : IHashMarkCommand
{
    public string Name => "culture";

    public string Description => "Allows changing the display culture";

    public Task<HashMarkResult> ExecuteAsync(Arguments args,
                                             IAnsiConsole ansiConsole,
                                             IMediator mediator,
                                             CancellationToken cancellationToken)
    {
        try
        {
            if (args.Count == 0 || string.IsNullOrEmpty(args[0]))
            {
                return Task.FromResult(new HashMarkResult("No culture specified"));
            }
            var culture = new CultureInfo(args[0]);
            mediator.Notify(new MessageTypes.CultureChange
            {
                CultureInfo = culture,
            });
            return Task.FromResult(new HashMarkResult());
        }
        catch (Exception)
        {
            return Task.FromResult(new HashMarkResult($"Error setting culture: {args[0]}"));
        }
    }
}
