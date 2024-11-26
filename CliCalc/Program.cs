using PrettyPrompt.Highlighting;
using PrettyPrompt;
using CliCalc.Engine;
using Spectre.Console;
using CliCalc;

var mediator = new Mediator();
var hashMarkCommands = HashmarkCommandLoader.GetCommands();
var engine = new Engine(mediator);
var presenter = new ResultPresenter(mediator, AnsiConsole.Console);

await using var prompt = new Prompt(
            callbacks: new EnginePromptCallbacks(hashMarkCommands),
            configuration: new PromptConfiguration(
                prompt: GetPrompt(),
                completionItemDescriptionPaneBackground: AnsiColor.Rgb(30, 30, 30),
                selectedCompletionItemBackground: AnsiColor.Rgb(30, 30, 30),
                selectedTextBackground: AnsiColor.Rgb(20, 61, 102)));

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
        await hashMarkCommands[args.CommandName].ExecuteAsync(args, AnsiConsole.Console, mediator, default);
    else
        AnsiConsole.MarkupLine($"[red bold]Unknown command: {text}[/]");

    prompt
}

FormattedString? GetPrompt()
{
    var prompt = $"{presenter.Culture.ThreeLetterISOLanguageName} {engine.AngleMode} >";
    return new FormattedString(prompt,
                               new FormatSpan(0, 3, AnsiColor.Magenta),
                               new FormatSpan(3, 5, AnsiColor.Yellow));
}