// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using System.Text;

namespace CliCalc.Functions;

/// <summary>
/// Represents a result of a hash calculation
/// </summary>
public sealed class HashValue : IFormattable, IEquatable<HashValue?>
{
    private readonly byte[] _data;

    /// <summary>
    /// Initializes a new instance of the HashValue class.
    /// </summary>
    /// <param name="data">data bytes</param>
    public HashValue(byte[] data)
    {
        _data = data;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as HashValue);
    }

    /// <inheritdoc/>
    public bool Equals(HashValue? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other._data.Length != _data.Length)
        {
            return false;
        }

        for (int i = 0; i < _data.Length; i++)
        {
            if (_data[i] != other._data[i])
            {
                return false;
            }
        }

        return true;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        for (int i = 0; i < _data.Length; i++)
        {
            hash.Add(_data[i]);
        }
        return hash.ToHashCode();
    }

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Hex: {Convert.ToHexString(_data)}");
        sb.AppendLine($"Base64: {Convert.ToBase64String(_data)}");
        return sb.ToString();
    }

    /// <inheritdoc/>
    public static bool operator ==(HashValue? left, HashValue? right)
    {
        return EqualityComparer<HashValue>.Default.Equals(left, right);
    }

    /// <inheritdoc/>
    public static bool operator !=(HashValue? left, HashValue? right)
    {
        return !(left == right);
    }
}
