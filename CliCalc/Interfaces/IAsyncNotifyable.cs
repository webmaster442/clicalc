namespace CliCalc.Interfaces;

internal interface IAsyncNotifyable<in T> : IMediatable
{
    Task OnNotifyAsync(T message);
}