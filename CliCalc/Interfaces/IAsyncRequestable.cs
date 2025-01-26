namespace CliCalc.Interfaces;

internal interface IAsyncRequestable<T> : IRequestableBase
{
    Task<T> OnRequestAsync(string dataSet, CancellationToken cancellationToken = default);
}
