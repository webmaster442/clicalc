using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
