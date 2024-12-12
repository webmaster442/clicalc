using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using CliCalc.Functions;

using Spectre.Console.Rendering;

namespace CliCalc.Interfaces;

internal interface IObjectRenderer
{
    bool TryRender(object value, CultureInfo culture, AngleMode angleMode, [NotNullWhen(true)] out IRenderable? renderable);
}