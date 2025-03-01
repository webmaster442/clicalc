﻿// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using System.Globalization;
using System.Numerics;
using System.Text;

using CliCalc.Domain;
using CliCalc.Functions;
using CliCalc.Interfaces;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

using Webmaster442.WindowsTerminal;

namespace CliCalc.Engine;

internal sealed class Engine : 
    IAsyncNotifyable<MessageTypes.ResetMessage>,
    INotifyable<MessageTypes.ExitMessage>,
    IRequestable<IEnumerable<string>>,
    IRequestable<IEnumerable<(string name, string typeName)>>,
    IRequestable<AngleMode>
{
    private readonly Global _globalScope;
    private readonly ScriptOptions _scriptOptions;
    private readonly IMediator _mediator;
    private readonly Configuration _configuration;
    private ScriptState<object>? _scriptState;

    public Engine(IMediator mediator, IReporter<long> reporter, Configuration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
        _mediator.Register(this);
        _globalScope = new Global(reporter);
        _scriptOptions = ScriptOptions.Default
            .WithLanguageVersion(Microsoft.CodeAnalysis.CSharp.LanguageVersion.Latest)
            .WithCheckOverflow(true)
            .WithAllowUnsafe(false)
            .WithReferences(typeof(Global).Assembly, typeof(Complex).Assembly);
    }

    public AngleMode AngleMode
        => _globalScope.Mode;

    public async Task InitializeAsync()
    {
        StringBuilder statements = new StringBuilder();
        foreach (var constant in _configuration.Constants)
        {
            statements.AppendLine($"const double {constant.Key} = {constant.Value.ToString(CultureInfo.InvariantCulture)};");
        }
        foreach (var variable in _configuration.Variables)
        {
            statements.AppendLine($"double {variable.Key} = {variable.Value.ToString(CultureInfo.InvariantCulture)};");
        }
        _scriptState = await CSharpScript.RunAsync(statements.ToString(), _scriptOptions, _globalScope);
    }

    public async Task<Result> Evaluate(string input, CancellationToken cancellationToken)
    {
        try
        {
            if (_scriptState == null)
            {
                throw new InvalidOperationException("Engine has not been initialized");
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

    public async Task Reset()
    {
        _scriptState = null;
        await InitializeAsync();
        _globalScope.Mode = AngleMode.Deg;
    }

    async Task IAsyncNotifyable<MessageTypes.ResetMessage>.OnNotifyAsync(MessageTypes.ResetMessage message)
        => await Reset();

    void INotifyable<MessageTypes.ExitMessage>.OnNotify(MessageTypes.ExitMessage message)
    {
        WindowsTerminal.SetProgressbar(ProgressbarState.Hidden, 0);
        Environment.Exit(0);
    }

    bool IRequestableBase.CanServe(string dataSetName)
        => dataSetName == MessageTypes.DataSets.Variables
        || dataSetName == MessageTypes.DataSets.VariablesWithTypes
        || dataSetName == MessageTypes.DataSets.AngleMode;

    IEnumerable<string> IRequestable<IEnumerable<string>>.OnRequest(string dataSet)
        => _scriptState != null ? _scriptState.Variables.Select(x => x.Name) : [];

    IEnumerable<(string name, string typeName)> IRequestable<IEnumerable<(string name, string typeName)>>.OnRequest(string dataSet)
        => _scriptState != null ? _scriptState.Variables.Select(x => (x.Name, x.Type.Name)) : [];

    AngleMode IRequestable<AngleMode>.OnRequest(string dataSet)
        => _globalScope.Mode;
}
