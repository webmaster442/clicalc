﻿namespace CliCalc.Domain;

internal sealed class Result
{
    private readonly object? _value;
    private readonly Exception? _exception;
    private readonly bool _isScuccess;

    private Result(object? value, Exception? ex, bool isScuccess)
    {
        _value = value;
        _exception = ex;
        _isScuccess = isScuccess;
    }

    public static Result Success(object value) => new(value, null, true);

    public static Result Failure(Exception ex) => new(null, ex, false);

    public void Handle(Action<object?> onSuccess, Action<Exception> onFailure)
    {
        if (_isScuccess)
        {
            onSuccess(_value);
        }
        else
        {
            onFailure(_exception!);
        }
    }
}