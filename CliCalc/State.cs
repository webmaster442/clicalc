// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using CliCalc.Domain;
using CliCalc.Interfaces;

namespace CliCalc;
internal sealed class State 
    : INotifyable<MessageTypes.ChangeWorkdir>,
      IRequestable<string>
{
    public string Workdir
    {
        get => Environment.CurrentDirectory;
        set => Environment.CurrentDirectory = value;
    }

    public State(IMediator mediator)
    {
        mediator.Register(this);
    }

    void INotifyable<MessageTypes.ChangeWorkdir>.OnNotify(MessageTypes.ChangeWorkdir message)
    {
        Workdir = message.Path;
    }

    bool IRequestableBase.CanServe(string dataSetName)
        => dataSetName == MessageTypes.DataSets.Workdir;

    string IRequestable<string>.OnRequest(string dataSet)
        => Workdir;
}
