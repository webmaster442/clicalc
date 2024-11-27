namespace CliCalc.Functions;

/// <summary>
/// Global scope
/// </summary>
public class Global
{
    /// <summary>
    /// Returns the largest integral value less than or equal to the specified number
    /// </summary>
    /// <param name="x">A double-precision floating-point number.</param>
    /// <returns>
    /// The largest integral value less than or equal to d. If d is equal to System.Double.NaN,
    /// System.Double.NegativeInfinity, or System.Double.PositiveInfinity, that value is returned.
    /// </returns>
    public double Floor(double x) => Math.Floor(x);

    /// <summary>
    /// Returns the smallest integral value greater than or equal to the specified number.
    /// </summary>
    /// <param name="x">A double-precision floating-point number.</param>
    /// <returns>
    /// The smallest integral value that is greater than or equal to a. If a is equal to NaN, 
    /// NegativeInfinity, or PositiveInfinity, that value is returned.
    /// </returns>
    public double Ceiling(double x) => Math.Ceiling(x);

    /// <summary>
    /// Rounds a double-precision floating-point value to the nearest integral value, and rounds midpoint values to the nearest even number.
    /// </summary>
    /// <param name="x">A double-precision floating-point number to be rounded</param>
    /// <returns>
    /// The integer nearest a. If the fractional component of a is halfway between two
    /// integers, one of which is even and the other odd, then the even number is returned.
    /// </returns>
    public double Round(double x) => Math.Round(x);

    /// <summary>
    /// Rounds a double-precision floating-point value to a specified number of fractional digits, and rounds midpoint values to the nearest even number.
    /// </summary>
    /// <param name="x">A double-precision floating-point number to be rounded.</param>
    /// <param name="digits">The number of fractional digits in the return value.</param>
    /// <returns>The number nearest to value that contains a number of fractional digits equal to digits.</returns>
    public double Round(double x, int digits) => Math.Round(x, digits);

    /// <summary>
    /// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π.
    /// </summary>
    public double Pi => Math.PI;

    /// <summary>
    /// Represents the natural logarithmic base, specified by the constant, e.
    /// </summary>
    public double E => Math.E;

    /// <summary>
    /// Angle system modes
    /// </summary>
    public enum AngleMode
    {
        /// <summary>
        /// Degrees
        /// </summary>
        Deg,
        /// <summary>
        /// Radians
        /// </summary>
        Rad,
        /// <summary>
        /// Gradians
        /// </summary>
        Grad
    }

    /// <summary>
    /// Current angle mode
    /// </summary>
    public AngleMode Mode { get; set; }
}
