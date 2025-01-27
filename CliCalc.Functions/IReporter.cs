// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

namespace CliCalc.Functions;

/// <summary>
/// A progress reporter interface with maximum and current values.
/// </summary>
/// <typeparam name="T">Type of progress reporting value</typeparam>
public interface IReporter<in T>
{
    /// <summary>
    /// Report that the progress is starting.
    /// </summary>
    /// <param name="value">maximum value to report</param>
    void Start(T value);

    /// <summary>
    /// Report the current value of the progress.
    /// </summary>
    /// <param name="value">value to report</param>
    void ReportCrurrent(T value);

    /// <summary>
    /// Report that the progress is done.
    /// </summary>
    void Done();
}
