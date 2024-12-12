namespace CliCalc.Interfaces;

internal interface IMediator : IDisposable
{
    void Register(IMediatable mediatable);
    void UnRegister(IMediatable mediatable);
    void Notify<T>(T message);
    Task NotifyAsync<T>(T message);
    T Request<T>(string dataSetName);
}
