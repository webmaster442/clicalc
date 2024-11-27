using CliCalc;
using CliCalc.Engine;

using PrettyPrompt;
using PrettyPrompt.Highlighting;

using Spectre.Console;

var mediator = new Mediator();
var hashMarkCommands = HashmarkCommandLoader.GetCommands();
var globalDocumentationProvider = new GlobalDocumentationProvider(mediator, hashMarkCommands);
var engine = new Engine(mediator);
var presenter = new ResultPresenter(mediator, AnsiConsole.Console);

var configuration = new PromptConfiguration(
                prompt: GetPrompt(),
                completionItemDescriptionPaneBackground: AnsiColor.Rgb(30, 30, 30),
                selectedCompletionItemBackground: AnsiColor.Rgb(30, 30, 30),
                selectedTextBackground: AnsiColor.Rgb(20, 61, 102));

await using var prompt = new Prompt(
            callbacks: new CliCalcPromptCallbacks(mediator, hashMarkCommands),
            configuration: configuration);

AnsiConsole.MarkupLine("""
    
    Welcome to [magenta bold]CliCalc[/]
    Type [red italic]#exit[/], [green italic]#help[/] for help.

    """);

while (true)
{
    var response = await prompt.ReadLineAsync().ConfigureAwait(false);
    if (response.IsSuccess)
    {
        if (response.Text.StartsWith('#'))
        {
            await ExecuteHashMark(response.Text);
        }
        else
        {
            var result = await engine.Evaluate(response.Text, default);
            presenter.Display(result);
        }
    }
}

async Task ExecuteHashMark(string text)
{
    var args = new Arguments(text);

    if (hashMarkCommands.ContainsKey(args.CommandName))
    {
        var result = await hashMarkCommands[args.CommandName].ExecuteAsync(args, AnsiConsole.Console, mediator, default);
        if (!result.Success)
        {
            AnsiConsole.MarkupLine($"[red bold]{result.Content}[/]");
        }
    }
    else
    {
        AnsiConsole.MarkupLine($"[red bold]Unknown command: {text}[/]");
    }
    configuration.Prompt = GetPrompt();
}

FormattedString GetPrompt()
{
    var prompt = $"{presenter.Culture.ThreeLetterISOLanguageName} {engine.AngleMode} >";
    return new FormattedString(prompt,
                               new FormatSpan(0, 3, AnsiColor.Magenta),
                               new FormatSpan(3, 5, AnsiColor.Yellow));
}