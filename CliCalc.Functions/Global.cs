using System.Globalization;

using CliCalc.Functions.Internals;

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
    /// Returns the absolute value of a double-precision floating-point number.
    /// </summary>
    /// <param name="x">A number that is greater than or equal to Double.MinValue, but less than or equal to Double.MaxValue.</param>
    /// <returns>A double-precision floating-point number, x, such that 0 ≤ x ≤ Double.MaxValue.</returns>
    public double Abs(double x) => Math.Abs(x);

    /// <summary>
    /// Returns the absolute value of an Int128 number.
    /// </summary>
    /// <param name="x">A number that is greater than or equal to Int128.MinValue, but less than or equal to Int128.MaxValue</param>
    /// <returns>An Int128 number, x, such that 0 ≤ x ≤ Int128.MaxValue.</returns>
    public Int128 Abs(Int128 x) => Int128.Abs(x);

    /// <summary>
    /// Returns the square root of a specified number.
    /// </summary>
    /// <param name="x">The number whose square root is to be found.</param>
    /// <returns>One of the values in the following table.</returns>
    public double Sqrt(double x) => Math.Sqrt(x);

    /// <summary>
    /// Returns a specified number raised to the specified power.
    /// </summary>
    /// <param name="x">A double-precision floating-point number to be raised to a power.</param>
    /// <param name="y">A double-precision floating-point number that specifies a power.</param>
    /// <returns>The number x raised to the power y.</returns>
    public double Pow(double x, double y) => Math.Pow(x, y);

    /// <summary>
    /// Returns e raised to the specified power.
    /// </summary>
    /// <param name="x">A number specifying a power.</param>
    /// <returns>The number e raised to the power d. If d equals NaN or PositiveInfinity, that value is returned. If d equals NegativeInfinity, 0 is returned.</returns>
    public double Exp(double x) => Math.Exp(x);

    /// <summary>
    /// Returns the natural (base e) logarithm of a specified number.
    /// </summary>
    /// <param name="x">The number whose logarithm is to be found.</param>
    /// <returns>The natural logarithm of d; that is, ln d, or log e d</returns>
    public double Log(double x) => Math.Log(x);

    /// <summary>
    /// Returns the logarithm of a specified number in a specified base.
    /// </summary>
    /// <param name="x">The number whose logarithm is to be found.</param>
    /// <param name="newBase">The base of the logarithm.</param>
    /// <returns>The logarithm of a specified number in the specified base</returns>
    public double Log(double x, double newBase) => Math.Log(x, newBase);

    /// <summary>
    /// Decides whether a long number is prime or not.
    /// </summary>
    /// <param name="number">A long number to decide</param>
    /// <returns>True, if the number is prime, otherwise fase.</returns>
    public bool IsPrime(long number)
        => Integers.IsPrime(number);

    /// <summary>
    /// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π.
    /// </summary>
    public double Pi => Math.PI;

    /// <summary>
    /// Represents the natural logarithmic base, specified by the constant, e.
    /// </summary>
    public double E => Math.E;

    /// <summary>
    /// Calculates the factorial of a given non-negative integer.
    /// </summary>
    /// <param name="number">
    /// The non-negative integer for which the factorial is to be calculated. 
    /// Must be between 0 and 33 (inclusive).
    /// </param>
    /// <returns>
    /// An Int128 representing the factorial of the input number. 
    /// If the input is 0, the result is 1 (0! = 1).
    /// If the input is negative, an exception may be thrown.
    /// </returns>
    public Int128 Factorial(int number)
        => Integers.Factorial(number);

    /// <summary>
    /// Converts a hexadecimal string representation into an Int128 value.
    /// </summary>
    /// <param name="s">A string containing a hexadecimal number (composed of characters 0-9 and A-F or a-f).</param>
    /// <returns>An Int128 that represents the value of the hexadecimal number provided in s</returns>
    public Int128 FromHex(string s)
        => Int128.Parse(s, NumberStyles.HexNumber | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingWhite);

    /// <summary>
    /// Converts a binary string representation into an Int128 value.
    /// </summary>
    /// <param name="s">A string containing a binary number (composed of '0' and '1' characters).</param>
    /// <returns>An Int128 that represents the value of the binary number provided in s</returns>
    public Int128 FromBin(string s)
        => Int128.Parse(s, NumberStyles.AllowBinarySpecifier | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingWhite);

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
