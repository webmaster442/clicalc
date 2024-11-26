namespace CliCalc.Interfaces;

internal interface IMediator
{
    void Register(IMediatable mediatable);
    void Notify<T>(T message);
    T Request<T>(string dataSetName);
}
