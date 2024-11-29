using System.Globalization;
using System.Numerics;

using CliCalc.Functions.Internals;

namespace CliCalc.Functions;

/// <summary>
/// Global scope
/// </summary>
public class Global
{
    /// <summary>
    /// Represents the natural logarithmic base, specified by the constant, e.
    /// </summary>
    public double E => Math.E;

    /// <summary>
    /// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π.
    /// </summary>
    public double Pi => Math.PI;

    internal AngleMode Mode { get; set; }

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
    /// Returns the absolute value (or magnitude) of a complex number.
    /// </summary>
    /// <param name="x">A complex number</param>
    /// <returns>The absolute value of the complex number</returns>
    public double Abs(Complex x) => System.Numerics.Complex.Abs(x);

    /// <summary>
    /// Calculates the arccosine (inverse cosine) of a specified number.
    /// The angle is returned in the unit of measure specified #mode command.
    /// </summary>
    /// <param name="x">a number</param>
    /// <returns>an angle</returns>
    public double Acos(double x)
        => Trigonometry.Acos(x, Mode);

    /// <summary>
    /// Calculates the arcsine (inverse sine) of a specified number.
    /// The angle is returned in the unit of measure specified #mode command.
    /// </summary>
    /// <param name="x">a number</param>
    /// <returns>an angle</returns>
    public double Asin(double x)
        => Trigonometry.Asin(x, Mode);

    /// <summary>
    /// Calculates the arctangent (inverse tangent) of a specified number.
    /// The angle is returned in the unit of measure specified #mode command.
    /// </summary>
    /// <param name="x">a number</param>
    /// <returns>an angle</returns>
    public double Atan(double x)
        => Trigonometry.Atan(x, Mode);

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
    /// Creates a complex value from a real and an imaginary part.
    /// </summary>
    /// <param name="real">The real part of the complex number. (double)</param>
    /// <param name="imaginary">The imaginary part of the complex number. (double)</param>
    /// <returns>a complex value</returns>
    public Complex Complex(double real, double imaginary)
        => new System.Numerics.Complex(real, imaginary);
    /// <summary>
    /// Computes the conjugate of a complex number and returns the result.
    /// </summary>
    /// <param name="x">A complex number.</param>
    /// <returns>The conjugate of the number.</returns>
    public Complex Conjugate(Complex x) => System.Numerics.Complex.Conjugate(x);

    /// <summary>
    /// Calculates the cosine of a specified angle.
    /// The angle is specified in the unit of measure specified #mode command.
    /// </summary>
    /// <param name="angle">an angle</param>
    /// <returns>The cosine of the angle</returns>
    public double Cos(double angle)
        => Trigonometry.Cos(angle, Mode);

    /// <summary>
    /// Returns the cube of a double-precision floating-point number.
    /// </summary>
    /// <param name="x">a double-precision floating-point number.</param>
    /// <returns>The cube of the specified number</returns>
    public double Cube(double x) => x * x * x;

    /// <summary>
    /// Converts an angle from degrees to radians.
    /// </summary>
    /// <param name="deg">The angle in degrees.</param>
    /// <returns>The equivalent angle in radians.</returns>
    public double DegToRad(double deg)
        => Trigonometry.DegToRad(deg);

    /// <summary>
    /// Returns e raised to the specified power.
    /// </summary>
    /// <param name="x">A number specifying a power.</param>
    /// <returns>The number e raised to the power d. If d equals NaN or PositiveInfinity, that value is returned. If d equals NegativeInfinity, 0 is returned.</returns>
    public double Exp(double x) => Math.Exp(x);

    /// <summary>
    /// Returns e raised to the power specified by a complex number.
    /// </summary>
    /// <param name="x">A complex number that specifies a power.</param>
    /// <returns>The number e raised to the power value.</returns>
    public Complex Exp(Complex x) => System.Numerics.Complex.Exp(x);

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
    /// Returns the largest integral value less than or equal to the specified number
    /// </summary>
    /// <param name="x">A double-precision floating-point number.</param>
    /// <returns>
    /// The largest integral value less than or equal to d. If d is equal to System.Double.NaN,
    /// System.Double.NegativeInfinity, or System.Double.PositiveInfinity, that value is returned.
    /// </returns>
    public double Floor(double x) => Math.Floor(x);

    /// <summary>
    /// Converts a binary string representation into an Int128 value.
    /// </summary>
    /// <param name="s">A string containing a binary number (composed of '0' and '1' characters).</param>
    /// <returns>An Int128 that represents the value of the binary number provided in s</returns>
    public Int128 FromBin(string s)
        => Int128.Parse(s, NumberStyles.AllowBinarySpecifier | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingWhite);

    /// <summary>
    /// Converts a hexadecimal string representation into an Int128 value.
    /// </summary>
    /// <param name="s">A string containing a hexadecimal number (composed of characters 0-9 and A-F or a-f).</param>
    /// <returns>An Int128 that represents the value of the hexadecimal number provided in s</returns>
    public Int128 FromHex(string s)
        => Int128.Parse(s, NumberStyles.HexNumber | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingWhite);

    /// <summary>
    /// Converts an angle from gradians to radians.
    /// </summary>
    /// <param name="grad">The angle in gradians.</param>
    /// <returns>The equivalent angle in radians.</returns>
    public double GradToRad(double grad)
        => Trigonometry.GradToRad(grad);

    /// <summary>
    /// Decides whether a long number is prime or not.
    /// </summary>
    /// <param name="number">A long number to decide</param>
    /// <returns>True, if the number is prime, otherwise fase.</returns>
    public bool IsPrime(long number)
        => Integers.IsPrime(number);

    /// <summary>
    /// Returns the natural (base e) logarithm of a specified number.
    /// </summary>
    /// <param name="x">The number whose logarithm is to be found.</param>
    /// <returns>The natural logarithm of d; that is, ln d, or log e d</returns>
    public double Log(double x) => Math.Log(x);

    /// <summary>
    /// Returns the natural (base e) logarithm of a specified complex number.
    /// </summary>
    /// <param name="x">A complex number.</param>
    /// <returns>The natural (base e) logarithm of value.</returns>
    public Complex Log(Complex x) => System.Numerics.Complex.Log(x);

    /// <summary>
    /// Returns the logarithm of a specified number in a specified base.
    /// </summary>
    /// <param name="x">The number whose logarithm is to be found.</param>
    /// <param name="newBase">The base of the logarithm.</param>
    /// <returns>The logarithm of a specified number in the specified base</returns>
    public double Log(double x, double newBase) => Math.Log(x, newBase);

    /// <summary>
    /// Returns the logarithm of a specified complex number in a specified base.
    /// </summary>
    /// <param name="x">A complex number.</param>
    /// <param name="newBase">The base of the logarithm.</param>
    /// <returns>The logarithm of value in base baseValue.</returns>
    public Complex Log(Complex x, double newBase) => System.Numerics.Complex.Log(x, newBase);

    /// <summary>
    /// Returns a specified number raised to the specified power.
    /// </summary>
    /// <param name="x">A double-precision floating-point number to be raised to a power.</param>
    /// <param name="y">A double-precision floating-point number that specifies a power.</param>
    /// <returns>The number x raised to the power y.</returns>
    public double Pow(double x, double y) => Math.Pow(x, y);

    /// <summary>
    /// Returns a specified complex number raised to a power specified by a double-precision floating-point number.
    /// </summary>
    /// <param name="x">A complex number to be raised to a power.</param>
    /// <param name="y">A double-precision floating-point number that specifies a power.</param>
    /// <returns>The complex number value raised to the power power.</returns>
    public Complex Pow(Complex x, double y) => System.Numerics.Complex.Pow(x, y);

    /// <summary>
    /// Returns a specified complex number raised to a power specified by a complex number.
    /// </summary>
    /// <param name="x">A complex number to be raised to a power.</param>
    /// <param name="y">A complex number that specifies a power.</param>
    /// <returns>The complex number value raised to the power power.</returns>
    public Complex Pow(Complex x, Complex y) => System.Numerics.Complex.Pow(x, y);

    /// <summary>
    /// Converts an angle from radians to degrees.
    /// </summary>
    /// <param name="rad">The angle in radians.</param>
    /// <returns>The equivalent angle in degrees.</returns>
    public double RadToDeg(double rad)
        => Trigonometry.RadToDeg(rad);

    /// <summary>
    /// Converts an angle from radians to gradians.
    /// </summary>
    /// <param name="rad">The angle in radians</param>
    /// <returns>The equivalent angle in gradians.</returns>
    public double RadToGrad(double rad)
        => Trigonometry.RadToGrad(rad);

    /// <summary>
    /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.
    /// </summary>
    /// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
    public double Rand() => Random.Shared.NextDouble();

    /// <summary>
    /// Returns a non-negative random integer that is less than the specified maximum.
    /// </summary>
    /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to 0.</param>
    /// <returns>A 32-bit signed integer that is greater than or equal to 0, and less than maxValue; that is, the range of return values ordinarily includes 0 but not maxValue. However, if maxValue equals 0, 0 is returned.</returns>
    public int Rand(int maxValue) => Random.Shared.Next(maxValue);

    /// <summary>
    /// Returns a random integer that is within a specified range.
    /// </summary>
    /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
    /// <returns>A 32-bit signed integer greater than or equal to minValue and less than maxValue; that is, the range of return values includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.</returns>
    public int Rand(int minValue, int maxValue) => Random.Shared.Next(minValue, maxValue);

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
    /// Calculates the sine of a specified angle.
    /// The angle is specified in the unit of measure specified #mode command.
    /// </summary>
    /// <param name="angle">an angle</param>
    /// <returns>The sine of the angle</returns>
    public double Sin(double angle)
        => Trigonometry.Sin(angle, Mode);

    /// <summary>
    /// Returns the square of a specified double number.
    /// </summary>
    /// <param name="x">a double-precision floating-point number.</param>
    /// <returns>The square of the specified number</returns>
    public double Sqr(double x) => x * x;

    /// <summary>
    /// Returns the square of a specified complex number.
    /// </summary>
    /// <param name="x">A complex number</param>
    /// <returns>The square of the specified number</returns>
    public Complex Sqr(Complex x) => x * x;

    /// <summary>
    /// Returns the square root of a double-precision floating-point number.
    /// </summary>
    /// <param name="x">The number whose square root is to be found.</param>
    /// <returns>One of the values in the following table.</returns>
    public double Sqrt(double x) => Math.Sqrt(x);

    /// <summary>
    /// Returns the square root of a specified complex number.
    /// </summary>
    /// <param name="x">A complex number</param>
    /// <returns>The square root of the number</returns>
    public Complex Sqrt(Complex x) => System.Numerics.Complex.Sqrt(x);

    /// <summary>
    /// Calculates the tangent of a specified angle.
    /// The angle is specified in the unit of measure specified #mode command.
    /// </summary>
    /// <param name="angle">an angle</param>
    /// <returns>The tangent of the angle</returns>
    public double Tan(double angle)
        => Trigonometry.Tan(angle, Mode);

    /// <summary>
    /// Returns a TimeSpan that represents a specified number of seconds, where the specification is accurate to the nearest millisecond.
    /// </summary>
    /// <param name="seconds">A number of seconds, accurate to the nearest millisecond.</param>
    /// <returns>An object that represents value.</returns>
    public TimeSpan Seconds(double seconds) => TimeSpan.FromSeconds(seconds);

    /// <summary>
    /// Returns a TimeSpan that represents a specified number of minutes, where the specification is accurate to the nearest millisecond.
    /// </summary>
    /// <param name="minutes">A number of minutes, accurate to the nearest millisecond.</param>
    /// <returns>An object that represents value.</returns>
    public TimeSpan Minutes(double minutes) => TimeSpan.FromMinutes(minutes);

    /// <summary>
    /// Returns a TimeSpan that represents a specified number of hours, where the specification is accurate to the nearest millisecond.
    /// </summary>
    /// <param name="hours">A number of hours accurate to the nearest millisecond.</param>
    /// <returns>An object that represents value.</returns>
    public TimeSpan Hours(double hours) => TimeSpan.FromHours(hours);

    /// <summary>
    /// Returns a TimeSpan that represents a specified number of days, where the specification is accurate to the nearest millisecond.
    /// </summary>
    /// <param name="days">A number of days, accurate to the nearest millisecond.</param>
    /// <returns>An object that represents value.</returns>
    public TimeSpan Days(double days) => TimeSpan.FromDays(days);

    /// <summary>
    /// Returns a TimeSpan that represents a specified number of weeks, where the specification is accurate to the nearest millisecond.
    /// </summary>
    /// <param name="weeks">A number of weeks, accurate to the nearest millisecond.</param>
    /// <returns>An object that represents value.</returns>
    public TimeSpan Weeks(double weeks) => TimeSpan.FromDays(weeks * 7);

    /// <summary>
    /// Initializes a new instance of the DateTime structure to the specified year, month, and day.
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="day"></param>
    /// <returns>A DateTime instance</returns>
    public DateTime Date(int year, int month, int day)
        => new DateTime(year, month, day);

    /// <summary>
    /// Initializes a new instance of the DateTime structure to the specified hour, minute, and second.
    /// </summary>
    /// <param name="hour">The hours (0 through 23).</param>
    /// <param name="minute">The minutes (0 through 59).</param>
    /// <param name="second">The seconds (0 through 59).</param>
    /// <returns>A DateTime instance</returns>
    public DateTime Time(int hour, int minute, int second)
        => new DateTime(1, 1, 1, hour, minute, second);

    /// <summary>
    /// Initializes a new instance of the DateTime structure to the specified year, month, day, hour, minute, and second.
    /// </summary>
    /// <param name="year">The year (1 through 9999).</param>
    /// <param name="month">The month (1 through 12).</param>
    /// <param name="day">The day (1 through the number of days in month).</param>
    /// <param name="hour">The hours (0 through 23).</param>
    /// <param name="minute">The minutes (0 through 59).</param>
    /// <param name="second">The seconds (0 through 59).</param>
    /// <returns>A DateTime instance</returns>
    public DateTime DateTime(int year, int month, int day, int hour, int minute, int second)
        => new DateTime(year, month, day, hour, minute, second);


}
