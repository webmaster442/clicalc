namespace CliCalc.Functions;

/// <summary>
/// Represent a binary result.
/// </summary>
public sealed class BinaryResult
{
    private readonly byte[] _data;

    /// <summary>
    /// Creates a new instance form a long result
    /// </summary>
    /// <param name="data">the data source long</param>
    public BinaryResult(long data)
    {
        _data = BitConverter.GetBytes(data);
    }

    /// <summary>
    /// Cereates a new instance from a double result
    /// </summary>
    /// <param name="data">the data source double</param>
    public BinaryResult(double data)
    {
        _data = BitConverter.GetBytes(data);
    }
}