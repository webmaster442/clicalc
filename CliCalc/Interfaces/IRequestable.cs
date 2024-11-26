namespace CliCalc.Interfaces;

internal interface IRequestable<out T> : IMediatable
{
    bool CanServe(string dataSetName);
    T OnRequest(string dataSet);
}
