// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

namespace CliCalc.Interfaces;

internal interface IRequestableBase: IMediatable
{
    bool CanServe(string dataSetName);
}