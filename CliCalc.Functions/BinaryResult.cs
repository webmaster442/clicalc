namespace CliCalc.Functions;

/// <summary>
/// Represent a binary result.
/// </summary>
public sealed class BinaryResult
{
    /// <summary>
    /// The data
    /// </summary>
    public byte[] Data { get; }

    /// <summary>
    /// Returns the data as long
    /// </summary>
    public long DataAsLong => BitConverter.ToInt64(Data, 0);

    /// <summary>
    /// Returns the data as double
    /// </summary>
    public double DataAsDouble => BitConverter.ToDouble(Data, 0);

    /// <summary>
    /// The data was created from a long
    /// </summary>
    public bool IsLong { get; }


    /// <summary>
    /// Creates a new instance form a long result
    /// </summary>
    /// <param name="data">the data source long</param>
    public BinaryResult(long data)
    {
        IsLong = true;
        Data = BitConverter.GetBytes(data);
    }

    /// <summary>
    /// Cereates a new instance from a double result
    /// </summary>
    /// <param name="data">the data source double</param>
    public BinaryResult(double data)
    {
        IsLong = false;
        Data = BitConverter.GetBytes(data);
    }
}