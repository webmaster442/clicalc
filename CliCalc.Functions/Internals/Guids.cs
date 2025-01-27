// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using System.Security.Cryptography;

namespace CliCalc.Functions.Internals;

internal static class Guids
{
    public static Guid V1()
    {
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() * 10000L + 0x01B21DD213814000L;
        byte[] timestampBytes = BitConverter.GetBytes(timestamp);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(timestampBytes);
        }

        byte[] nodeBytes = new byte[6];
        RandomNumberGenerator.Fill(nodeBytes);

        byte[] clockSequenceBytes = new byte[2];
        RandomNumberGenerator.Fill(clockSequenceBytes);

        byte[] guidBytes = new byte[16];
        Array.Copy(timestampBytes, 2, guidBytes, 0, 4); // time-low
        Array.Copy(timestampBytes, 0, guidBytes, 4, 2); // time-mid
        Array.Copy(timestampBytes, 6, guidBytes, 6, 2); // time-high-and-version

        // Set version to 1 (bit pattern: 0001)
        guidBytes[6] = (byte)((guidBytes[6] & 0x0F) | 0x10);

        // Clock sequence (14 bits)
        Array.Copy(clockSequenceBytes, 0, guidBytes, 8, 2);

        // Set variant to RFC 4122 (bit pattern: 10xx)
        guidBytes[8] = (byte)((guidBytes[8] & 0x3F) | 0x80);

        // Node (48 bits)
        Array.Copy(nodeBytes, 0, guidBytes, 10, 6);

        return new Guid(guidBytes);
    }

    public static Guid V4()
        => Guid.NewGuid();

    public static Guid V7()
        => Guid.CreateVersion7();
}
