namespace CliCalc.Interfaces;

internal interface INotifyable<in T> : IMediatable
{
    void OnNotify(T message);
}
