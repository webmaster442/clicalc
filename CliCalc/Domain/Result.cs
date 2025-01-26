namespace CliCalc.Domain;

internal sealed class Result
{
    private readonly object? _value;
    private readonly Exception? _exception;

    public bool IsScuccess { get; private set; }

    private Result(object? value, Exception? ex, bool isScuccess)
    {
        _value = value;
        _exception = ex;
        IsScuccess = isScuccess;
    }

    public static Result Success(object value) => new(value, null, true);

    public static Result Failure(Exception ex) => new(null, ex, false);

    public void Handle(Action<object?> onSuccess, Action<Exception> onFailure)
    {
        if (IsScuccess)
        {
            onSuccess(_value);
        }
        else
        {
            onFailure(_exception!);
        }
    }
}
