using System.Numerics;

using CliCalc.Domain;
using CliCalc.Functions;
using CliCalc.Interfaces;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace CliCalc.Engine;

internal sealed class Engine : 
    INotifyable<MessageTypes.ResetMessage>,
    INotifyable<MessageTypes.ExitMessage>,
    IRequestable<IEnumerable<string>>,
    IRequestable<IEnumerable<(string name, string typeName)>>,
    IRequestable<AngleMode>
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
            .WithCheckOverflow(true)
            .WithAllowUnsafe(false)
            .WithReferences(typeof(Global).Assembly, typeof(Complex).Assembly);
    }

    public AngleMode AngleMode
        => _globalScope.Mode;

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
                _scriptState = await _scriptState.ContinueWithAsync(input, cancellationToken: cancellationToken);
                return Result.Success(_scriptState.ReturnValue);
            }
        }
        catch (Exception ex)
        {
            return Result.Failure(ex);
        }
    }

    public void Reset()
    {
        _scriptState = null;
        _globalScope.Mode = AngleMode.Deg;
    }

    void INotifyable<MessageTypes.ResetMessage>.OnNotify(MessageTypes.ResetMessage message)
        => Reset();

    void INotifyable<MessageTypes.ExitMessage>.OnNotify(MessageTypes.ExitMessage message)
        => Environment.Exit(0);

    bool IRequestable<IEnumerable<string>>.CanServe(string dataSetName)
        => dataSetName == MessageTypes.DataSets.Variables;

    IEnumerable<string> IRequestable<IEnumerable<string>>.OnRequest(string dataSet)
        => _scriptState != null ? _scriptState.Variables.Select(x => x.Name) : [];

    bool IRequestable<IEnumerable<(string name, string typeName)>>.CanServe(string dataSetName)
        => dataSetName == MessageTypes.DataSets.VariablesWithTypes;

    IEnumerable<(string name, string typeName)> IRequestable<IEnumerable<(string name, string typeName)>>.OnRequest(string dataSet)
        => _scriptState != null ? _scriptState.Variables.Select(x => (x.Name, x.Type.Name)) : [];

    bool IRequestable<AngleMode>.CanServe(string dataSetName)
        => dataSetName == MessageTypes.DataSets.AngleMode;

    AngleMode IRequestable<AngleMode>.OnRequest(string dataSet)
        => _globalScope.Mode;
}
