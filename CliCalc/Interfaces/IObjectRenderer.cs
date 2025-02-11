// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using CliCalc.Functions;

using Spectre.Console;
using Spectre.Console.Rendering;

namespace CliCalc.Interfaces;

internal interface IObjectRenderer
{
    bool TryRender(object value,
                   CultureInfo culture,
                   AngleMode angleMode,
                   [NotNullWhen(true)] out IRenderable? renderable);
}