using CliCalc.Interfaces;

namespace CliCalc;

internal sealed class Mediator : IMediator
{
    private readonly List<IMediatable> _mediatables;

    public Mediator()
    {
        _mediatables = new List<IMediatable>();
    }

    public void Notify<T>(T message)
    {
        foreach (var mediatable in _mediatables)
        {
            if (mediatable is INotifyable<T> notifyable)
            {
                notifyable.OnNotify(message);
            }
        }
    }

    public T Request<T>(string dataSetName)
    {
        foreach (var mediatable in _mediatables)
        {
            if (mediatable is IRequestable<T> requestable
                && requestable.CanServe(dataSetName))
            {
                return requestable.OnRequest(dataSetName);
            }
        }
        throw new InvalidOperationException($"No handler found for: {dataSetName}");
    }

    public void Register(IMediatable mediatable)
    {
        _mediatables.Add(mediatable);
    }
}
