// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

namespace CliCalc.Interfaces;

internal interface IAsyncRequestable<T> : IRequestableBase
{
    Task<T> OnRequestAsync(string dataSet, CancellationToken cancellationToken = default);
}
