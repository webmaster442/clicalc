// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using CliCalc.Functions;
using CliCalc.Interfaces;

using Spectre.Console;
using Spectre.Console.Rendering;

namespace CliCalc.Engine.Renderers;

internal sealed class BinaryRenderer : IObjectRenderer
{
    public bool TryRender(object value, CultureInfo culture, AngleMode angleMode, [NotNullWhen(true)] out IRenderable? renderable)
    {
        if (value is not Binary binary)
        {
            renderable = null;
            return false;
        }

        if (binary.Count <= 8)
            return RenderNumberInfo(binary, out renderable);

        return RenderHexDump(binary, out renderable);
    }

    private static bool RenderNumberInfo(Binary binary, out IRenderable? renderable)
    {
        static string GetFloatValue(Binary binary)
        {
            return binary.Count switch
            {
                2 => BitConverter.ToHalf(binary.AsArray()).ToString(CultureInfo.InvariantCulture),
                4 => BitConverter.ToSingle(binary.AsArray()).ToString(CultureInfo.InvariantCulture),
                8 => BitConverter.ToDouble(binary.AsArray()).ToString(CultureInfo.InvariantCulture),
                _ => string.Empty,
            };
        }

        static string GetSignedValue(Binary binary)
        {
            return binary.Count switch
            {
                1 => ((sbyte)binary[0]).ToString(CultureInfo.InvariantCulture),
                2 => BitConverter.ToInt16(binary.AsArray()).ToString(CultureInfo.InvariantCulture),
                4 => BitConverter.ToInt32(binary.AsArray()).ToString(CultureInfo.InvariantCulture),
                8 => BitConverter.ToInt64(binary.AsArray()).ToString(CultureInfo.InvariantCulture),
                _ => string.Empty,
            };
        }

        static string GetUnsignedValue(Binary binary)
        {
            return binary.Count switch
            {
                1 => binary[0].ToString(CultureInfo.InvariantCulture),
                2 => BitConverter.ToUInt16(binary.AsArray()).ToString(CultureInfo.InvariantCulture),
                4 => BitConverter.ToUInt32(binary.AsArray()).ToString(CultureInfo.InvariantCulture),
                8 => BitConverter.ToUInt64(binary.AsArray()).ToString(CultureInfo.InvariantCulture),
                _ => string.Empty,
            };
        }

        string asUnsigned = GetUnsignedValue(binary);
        string asSigned = GetSignedValue(binary);
        string asFloat = GetFloatValue(binary);

        Table table = new Table();
        for (int i= binary.Count -1; i>= 0; i--)
        {
            table.AddColumn($"Byte {i}");
        }
        table.AddRow(binary.Reverse().Select(x => x.ToString("X2").PadLeft(8, ' ')).ToArray());
        table.AddRow(binary.Reverse().Select(x => Convert.ToString(x, 2).PadLeft(8, '0')).ToArray());

        Tree tree = new("Result");
        tree.AddNode(table);
        tree.AddNode(new Text($"Bytes: {binary.Count}"));
        if (!string.IsNullOrEmpty(asUnsigned))
        {
            tree.AddNode(new Text($"Unsigned: {asUnsigned}"));
        }
        if (!string.IsNullOrEmpty(asSigned))
        {
            tree.AddNode(new Text($"Signed: {asSigned}"));
        }
        if (!string.IsNullOrEmpty(asFloat))
        {
            tree.AddNode(new Text($"Float: {asFloat}"));
        }

        renderable = tree;
        return true;
    }

    private static bool RenderHexDump(Binary binary, out IRenderable? renderable)
    {
        var table = new Table();
        table.AddColumns("Offset", "", "Text");
        var rows = binary.Chunk(16);
        int offset = 0;
        foreach (var row in rows)
        {
            var offsetStr = offset.ToString("X8");
            var hex = string.Join(" ", row.Select(b => b.ToString("X2")));
            var text = string.Join("", row.Select(b => char.IsControl((char)b) ? '.' : (char)b));
            table.AddRow(offsetStr, hex, text);
            offset += row.Length;
        }
        renderable = table;
        return true;
    }
}
