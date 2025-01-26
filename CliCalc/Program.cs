using System.Globalization;

using CliCalc;
using CliCalc.Engine;
using CliCalc.Infrastructure;

using Webmaster442.WindowsTerminal;
using PrettyPrompt;
using PrettyPrompt.Highlighting;

using Spectre.Console;

Console.OutputEncoding = System.Text.Encoding.UTF8;

ConfigReader configReader = new();
using var mediator = new Mediator();
var state = new State(mediator);
var hashMarkCommands = HashmarkCommandLoader.GetCommands();
var globalDocumentationProvider = new GlobalDocumentationProvider(mediator, hashMarkCommands);

var programConfiguration = await configReader.ReadAsync();

var engine = new Engine(mediator, new Reporter(AnsiConsole.Console), programConfiguration);
var presenter = new ResultPresenter(mediator, AnsiConsole.Console, CultureInfo.CurrentUICulture);

var promptConfiguration = new PromptConfiguration(prompt: GetPrompt(),
                                                  completionItemDescriptionPaneBackground: AnsiColor.Rgb(30, 30, 30),
                                                  selectedCompletionItemBackground: AnsiColor.Rgb(30, 30, 30),
                                                  selectedTextBackground: AnsiColor.Rgb(20, 61, 102));

await using var prompt = new Prompt(
            callbacks: new CliCalcPromptCallbacks(mediator, hashMarkCommands),
            configuration: promptConfiguration);

AnsiConsole.MarkupLine("""
    
    Welcome to [magenta bold]CliCalc[/]
    Type [red italic]#exit[/], [green italic]#help[/] for help.

    """);

using ConsoleCancellationTokenSource cts = new();

await engine.InitializeAsync();

while (true)
{
    WindowsTerminal.ShellIntegration.StartOfPrompt();
    promptConfiguration.Prompt = GetPrompt();
    WindowsTerminal.ShellIntegration.CommandStart();
    PromptResult response = await prompt.ReadLineAsync().ConfigureAwait(false);
    if (response.IsSuccess)
    {
        int exitCode = 0;
        WindowsTerminal.ShellIntegration.CommandExecuted();
        WindowsTerminal.SetProgressbar(ProgressbarState.Indeterminate, 0);
        if (response.Text.StartsWith('#'))
        {
            exitCode = await ExecuteHashMark(response.Text, cts.Token).ConfigureAwait(false);
        }
        else
        {
            CliCalc.Domain.Result result = await engine.Evaluate(response.Text, cts.Token).ConfigureAwait(false);
            presenter.Display(result);
            exitCode = result.IsScuccess ? 0 : 1;
        }
        WindowsTerminal.SetProgressbar(ProgressbarState.Hidden, 0);
        WindowsTerminal.ShellIntegration.CommandFinished(exitCode);
    }
}

async Task<int> ExecuteHashMark(string text, CancellationToken cancellationToken)
{
    var args = new Arguments(text);
    int exitCode = 0;
    if (hashMarkCommands.TryGetValue(args.CommandName, out CliCalc.Interfaces.IHashMarkCommand? value))
    {
        var result = await value.ExecuteAsync(args, AnsiConsole.Console, mediator, cancellationToken);
        if (!result.Success)
        {
            AnsiConsole.MarkupLine($"[red bold]{result.Content}[/]");
            exitCode = 1;
        }
    }
    else
    {
        AnsiConsole.MarkupLine($"[red bold]Unknown command: {text}[/]");
        exitCode = 2;
    }
    return exitCode;
}

FormattedString GetPrompt()
{
    var folder = Path.GetFileName(state.Workdir);
    if (string.IsNullOrEmpty(folder))
        folder = state.Workdir;

    var folderPath = $"(Dir: {folder})";
    var prompt = $"{folderPath} {presenter.Culture.ThreeLetterISOLanguageName} {engine.AngleMode} >";

    return new FormattedString(prompt,
                               new FormatSpan(0, folderPath.Length, AnsiColor.BrightCyan),
                               new FormatSpan(folderPath.Length+1, 3, AnsiColor.Magenta),
                               new FormatSpan(folderPath.Length+4, 5, AnsiColor.Yellow));
}