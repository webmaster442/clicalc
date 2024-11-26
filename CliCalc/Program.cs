using PrettyPrompt.Highlighting;
using PrettyPrompt;
using CliCalc.Engine;
using Spectre.Console;
using CliCalc;

var mediator = new Mediator();
var HashMarkCommands = HashmarkCommandLoader.GetCommands();
var engine = new Engine(mediator);
var api = new API(mediator);

await using var prompt = new Prompt(
            callbacks: new EnginePromptCallbacks(HashMarkCommands),
            configuration: new PromptConfiguration(
                prompt: new FormattedString(">>> ", new FormatSpan(0, 1, AnsiColor.Red), new FormatSpan(1, 1, AnsiColor.Yellow), new FormatSpan(2, 1, AnsiColor.Green)),
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
            result.Handle(result =>
            {
                AnsiConsole.MarkupLine($"[bold green]{result}[/]");
            }, error =>
            {
                AnsiConsole.MarkupLine($"[bold red]{error.Message.EscapeMarkup()}[/]");
            });
        }
    }
}

async Task ExecuteHashMark(string text)
{
    await HashMarkCommands[text].ExecuteAsync(AnsiConsole.Console, api, default);
}