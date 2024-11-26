using CliCalc.Domain;
using CliCalc.Functions;
using CliCalc.Interfaces;

using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace CliCalc.Engine;

internal sealed class Engine : INotifyable<MessageType.ResetMessage>
{
    private readonly Global _globalScope;
    private readonly ScriptOptions _scriptOptions;
    private readonly IMediator _mediator;
    private ScriptState<object>? _scriptState;

    public Engine(IMediator mediator)
    {
        _mediator = mediator;
        _mediator.Register(this);
        _globalScope = new Global();
        _scriptOptions = ScriptOptions.Default
            .WithLanguageVersion(Microsoft.CodeAnalysis.CSharp.LanguageVersion.Latest)
            .WithCheckOverflow(true);
    }

    public async Task<Result> Evaluate(string input, CancellationToken cancellationToken)
    {
        try
        {
            if (_scriptState == null)
            {
                _scriptState = await CSharpScript.RunAsync(input, _scriptOptions, _globalScope, cancellationToken: cancellationToken);
                return Result.Success(_scriptState.ReturnValue);
            }
            else
            {
                await _scriptState.ContinueWithAsync(input, cancellationToken: cancellationToken);
                return Result.Success(_scriptState.ReturnValue);
            }
        }
        catch (Exception ex)
        {
            return Result.Failure(ex);
        }
    }

    public void OnNotify(MessageType.ResetMessage message)
    {
        _scriptState = null;
    }
}
