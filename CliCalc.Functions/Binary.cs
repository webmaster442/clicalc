// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using System.Collections;

namespace CliCalc.Functions;

/// <summary>
/// Represents a binary value.
/// </summary>
public sealed class Binary : IReadOnlyList<byte>
{
    private readonly byte[] _data;

    /// <summary>
    /// Initializes a new instance of the Binary class.
    /// </summary>
    /// <param name="data">Byte data</param>
    public Binary(byte[] data)
    {
        _data = data;
    }

    /// <inheritdoc/>
    public byte this[int index] => _data[index];

    /// <inheritdoc/>
    public int Count => _data.Length;

    /// <inheritdoc/>
    public IEnumerator<byte> GetEnumerator()
    {
        return ((IEnumerable<byte>)_data).GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _data.GetEnumerator();
    }

    /// <summary>
    /// Returns the byte array.
    /// </summary>
    /// <returns>The underlying Byte array</returns>
    public byte[] AsArray()
    {
        return _data;
    }
}
