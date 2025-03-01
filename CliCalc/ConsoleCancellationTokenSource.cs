﻿// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

namespace CliCalc;

internal sealed class ConsoleCancellationTokenSource : IDisposable
{
    private CancellationTokenSource? _tokenSource;

    public CancellationToken Token
    {
        get
        {
            if (_tokenSource == null)
            {
                _tokenSource = new CancellationTokenSource();
            }
            return _tokenSource.Token;
        }
    }

    public ConsoleCancellationTokenSource()
    {
        _tokenSource = new CancellationTokenSource();
        Console.CancelKeyPress += OnCancelKeyPress;
    }

    public void Dispose()
    {
        if (_tokenSource != null)
        {
            _tokenSource.Dispose();
            _tokenSource = null;
        }
        Console.CancelKeyPress -= OnCancelKeyPress;
    }

    private void OnCancelKeyPress(object? sender, ConsoleCancelEventArgs e)
    {
        if (_tokenSource != null)
        {
            e.Cancel = true;
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _tokenSource = null;
        }
    }
}
