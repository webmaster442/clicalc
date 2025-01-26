namespace CliCalc.Interfaces;

internal interface IRequestableBase: IMediatable
{
    bool CanServe(string dataSetName);
}