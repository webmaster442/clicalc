using System.Security.Cryptography;

namespace CliCalc.Functions.Internals;

internal static class HashCalculator
{
    private const int BufferSize = 4096;

    public static HashValue ComputeSha1(File input, IReporter<long> processed)
    {
        using var sha1 = SHA1.Create();
        return ComputeHashCore(input, sha1, processed);
    }

    public static HashValue ComputeSha256(File input, IReporter<long> processed)
    {
        using var sha256 = SHA256.Create();
        return ComputeHashCore(input, sha256, processed);
    }

    public static HashValue ComputeSha384(File input, IReporter<long> processed)
    {
        using var sha384 = SHA384.Create();
        return ComputeHashCore(input, sha384, processed);
    }

    public static HashValue ComputeSha512(File input, IReporter<long> processed)
    {
        using var sha512 = SHA512.Create();
        return ComputeHashCore(input, sha512, processed);
    }

    public static HashValue ComputeMd5(File input, IReporter<long> processed)
    {
        using var md5 = MD5.Create();
        return ComputeHashCore(input, md5, processed);
    }

    private static HashValue ComputeHashCore(File input, HashAlgorithm algorithm, IReporter<long> processed)
    {
        using var stream = input.OpenRead();
        processed.Start(stream.Length);

        byte[] buffer = new byte[BufferSize];
        long totalBytesRead = 0;
        int bytesRead;

        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            algorithm.TransformBlock(buffer, 0, bytesRead, null, 0);
            totalBytesRead += bytesRead;
            processed.ReportCrurrent(totalBytesRead);
        }

        // Finalize the hash computation
        algorithm.TransformFinalBlock(Array.Empty<byte>(), 0, 0);

        if (algorithm.Hash == null)
        {
            throw new InvalidOperationException("Failed to compute the hash");
        }

        processed.Done();

        return new HashValue(algorithm.Hash);
    }
}
