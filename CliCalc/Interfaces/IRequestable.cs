namespace CliCalc.Interfaces;

internal interface IRequestable<out T> : IRequestableBase
{
    T OnRequest(string dataSet);
}
