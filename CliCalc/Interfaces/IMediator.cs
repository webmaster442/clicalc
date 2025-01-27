// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

namespace CliCalc.Interfaces;

internal interface IMediator : IDisposable
{
    void Register(IMediatable mediatable);
    void UnRegister(IMediatable mediatable);
    void Notify<T>(T message);
    Task NotifyAsync<T>(T message);
    T Request<T>(string dataSetName);
}
