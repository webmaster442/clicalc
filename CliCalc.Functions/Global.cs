﻿// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using System.Globalization;
using System.Numerics;

using CliCalc.Functions.Internals;

using OxyPlot;
using OxyPlot.Series;

namespace CliCalc.Functions;

/// <summary>
/// Global scope
/// </summary>
public sealed class Global
{
    private readonly IReporter<long> _rerporter;

    /// <summary>
    /// Initializes a new instance of the Global class.
    /// </summary>
    /// <param name="rerporter">progress reporter for long running actions</param>
    public Global(IReporter<long> rerporter)
    {
        _rerporter = rerporter;
    }

    /// <summary>
    /// Represents the natural logarithmic base, specified by the constant, e.
    /// </summary>
    public double E => Math.E;

    /// <summary>
    /// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π.
    /// </summary>
    public double Pi => Math.PI;

    /// <summary>
    /// Represents the SI prefix deca, which is 10^1.
    /// </summary>
    public double Deca => 10;

    /// <summary>
    /// Represents the SI prefix hecto, which is 10^2.
    /// </summary>
    public double Hecto => 100;

    /// <summary>
    /// Represents the SI prefix kilo, which is 10^3.
    /// </summary>
    public double Kilo => 1000;

    /// <summary>
    /// Represents the SI prefix mega, which is 10^6.
    /// </summary>
    public double Mega => 1E6;

    /// <summary>
    /// Represents the SI prefix giga, which is 10^9.
    /// </summary>
    public double Giga => 1E9;

    /// <summary>
    /// Represents the SI prefix tera, which is 10^12.
    /// </summary>
    public double Tera => 1E12;

    /// <summary>
    /// Represent the SI prefix peta, which is 10^15.
    /// </summary>
    public double Peta => 1E15;

    /// <summary>
    /// Represents the SI prefix exa, which is 10^18.
    /// </summary>
    public double Exa => 1E18;

    /// <summary>
    /// Represents the SI prefix deci, which is 10^-1.
    /// </summary>
    public double Deci => 0.1;

    /// <summary>
    /// Represents the SI prefix centi, which is 10^-2.
    /// </summary>
    public double Centi => 0.01;

    /// <summary>
    /// Represents the SI prefix milli, which is 10^-3.
    /// </summary>
    public double Milli => 0.001;

    /// <summary>
    /// Represents the SI prefix micro, which is 10^-6.
    /// </summary>
    public double Micro => 1E-6;

    /// <summary>
    /// Represents the SI prefix nano, which is 10^-9.
    /// </summary>
    public double Nano => 1E-9;

    /// <summary>
    /// Represents the SI prefix pico, which is 10^-12.
    /// </summary>
    public double Pico => 1E-12;

    /// <summary>
    /// Represents the SI prefix femto, which is 10^-15.
    /// </summary>
    public double Femto => 1E-15;

    /// <summary>
    /// Represents the SI prefix atto, which is 10^-18.
    /// </summary>
    public double Atto => 1E-18;

    internal AngleMode Mode { get; set; }

    /// <summary>
    /// Returns the absolute value of a double-precision floating-point number.
    /// </summary>
    /// <param name="x">A number that is greater than or equal to Double.MinValue, but less than or equal to Double.MaxValue.</param>
    /// <returns>A double-precision floating-point number, x, such that 0 ≤ x ≤ Double.MaxValue.</returns>
    public double Abs(double x) => Math.Abs(x);

    /// <summary>
    /// Returns the absolute value of an int number.
    /// </summary>
    /// <param name="x">A number that is greater than or equal to int.MinValue, but less than or equal to int.MaxValue.</param>
    /// <returns>An 32 bit signed number, x, such that 0 ≤ x ≤ int.MaxValue.</returns>
    public double Abs(int x) => Math.Abs(x);

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
    /// Returns the absolute value of a fraction.
    /// </summary>
    /// <param name="x">A fraction number</param>
    /// <returns>The absolute value of the fraction</returns>
    public Fraction Abs(Fraction x) => x.Numerator < 0 ? -x : x;

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

    /// <summary>
    /// Calculates the greatest common divisor (GCD) of two integers using the Euclidean algorithm. The GCD is the largest positive integer that divides both numbers without leaving a remainder.
    /// </summary>
    /// <param name="a">The first integer.</param>
    /// <param name="b">The second integer.</param>
    /// <returns>The greatest common divisor of a and b.</returns>
    public long Gcd(long a, long b)
        => Integers.GreatestCommonDivisor(a, b);

    /// <summary>
    /// Calculates the least common multiple (LCM) of two integers. The LCM is the smallest positive integer that is divisible by both numbers.
    /// </summary>
    /// <param name="a">The first integer.</param>
    /// <param name="b">The second integer.</param>
    /// <returns>The least common multiple of a and b.</returns>
    public long Lcm(long a, long b)
        => Integers.Lcm(a, b);

    /// <summary>
    /// Creates a fraction representation from the given numerator and denominator. The fraction is usually reduced to its simplest form.
    /// </summary>
    /// <param name="numerator">The numerator of the fraction.</param>
    /// <param name="denominator">The denominator of the fraction.</param>
    /// <returns>A fraction object representing the ratio numerator/denominator.</returns>
    public Fraction Fraction(long numerator, long denominator)
        => new Fraction(numerator, denominator);

    /// <summary>
    /// Solve a polinomal equation. Supports linear, quadratic and cubic equations.
    /// Coefficients must be provided in ascending order of the power of x.
    /// Example: 2x^2 + 7x + 3 = 0 => SolvePolinomalEquation(3, 7, 2)
    /// </summary>
    /// <param name="coefficients">Coefficients, powers of x</param>
    /// <returns>Equation solutions</returns>
    public EquationSolution SolvePolinomalEquation(params double[] coefficients)
        => EquationSolver.Solve(coefficients);

    /// <summary>
    /// Converts an integer to a binary representation.
    /// </summary>
    /// <param name="value">integer to convert</param>
    /// <returns>Binary representation</returns>
    public Binary ToBinary(int value)
        => new(BitConverter.GetBytes(value));

    /// <summary>
    /// Converts an long to a binary representation.
    /// </summary>
    /// <param name="value">long to convert</param>
    /// <returns>Binary representation</returns>
    public Binary ToBinary(long value)
        => new(BitConverter.GetBytes(value));

    /// <summary>
    /// Converts a double precision floating-point number to a binary representation.
    /// </summary>
    /// <param name="value">double to convert</param>
    /// <returns>Binary representation</returns>
    public Binary ToBinary(double value)
        => new(BitConverter.GetBytes(value));

    /// <summary>
    /// Converts a single precision floating-point number to a binary representation.
    /// </summary>
    /// <param name="value">float to convert</param>
    /// <returns>Binary representation</returns>
    public Binary ToBinary(float value)
        => new(BitConverter.GetBytes(value));

    /// <summary>
    /// Checks if a double precision floating-point number can be exactly represented as a decimal number.
    /// </summary>
    /// <param name="number">number to check</param>
    /// <returns>true, if the number can be represented exactly in the double type, false if it can be represented with rounding errors</returns>
    public bool CanBeExactlyRepresented(double number)
        => Doubles.IsExact(number);

    /// <summary>
    /// Checks if a double precision floating-point number can be exactly represented as a decimal number.
    /// </summary>
    /// <param name="number">number to check</param>
    /// <returns>true, if the number can be represented exactly in the double type, false if it can be represented with rounding errors</returns>
    public bool CanBeExactlyRepresented(float number)
        => Doubles.IsExact(number);

    /// <summary>
    /// Create a Version 1 type Globaly Unique Identifier (GUID).
    /// </summary>
    /// <returns>A Guid</returns>
    public Guid GuidV1() => Guids.V1();

    /// <summary>
    /// Create a Version 4 type Globaly Unique Identifier (GUID).
    /// </summary>
    /// <returns>A Guid</returns>
    public Guid GuidV4() => Guids.V4();

    /// <summary>
    /// Create a Version 7 type Globaly Unique Identifier (GUID).
    /// </summary>
    /// <returns>A Guid</returns>
    public Guid GuidV7() => Guids.V7();

    /// <summary>
    /// Generate a sequence of double-precision floating-point numbers.
    /// </summary>
    /// <param name="start">start value</param>
    /// <param name="count">sequence lenght</param>
    /// <param name="next">a function to compute the next value.</param>
    /// <returns>A sequence of double-precision floating-point numbers.</returns>
    public IEnumerable<double> Range(double start, int count, Func<double, double> next)
        => Doubles.Range(start, count, next);

    /// <summary>
    /// Create a new file object.
    /// </summary>
    /// <param name="path">The File path on the file system.</param>
    /// <returns>A file object.</returns>
    public File File(string path)
        => new File(path);

    /// <summary>
    /// Computes the SHA1 hash of a file.
    /// </summary>
    /// <param name="file">The file, whose hash value will be computed.</param>
    /// <returns>The Hash of the file.</returns>
    public HashValue Sha1(File file)
        => HashCalculator.ComputeSha1(file, _rerporter);

    /// <summary>
    /// Computes the SHA1 hash of a string.
    /// </summary>
    /// <param name="str">The string, whose hash value will be computed.</param>
    /// <returns>The Hash of the sring.</returns>
    public HashValue Sha1(string str)
        => HashCalculator.ComputeSha1(str);

    /// <summary>
    /// Computes the SHA256 hash of a file.
    /// </summary>
    /// <param name="file">The file, whose hash value will be computed.</param>
    /// <returns>The Hash of the file.</returns>
    public HashValue Sha256(File file)
        => HashCalculator.ComputeSha256(file, _rerporter);

    /// <summary>
    /// Computes the SHA256 hash of a string.
    /// </summary>
    /// <param name="str">The string, whose hash value will be computed.</param>
    /// <returns>The Hash of the sring.</returns>
    public HashValue Sha256(string str)
        => HashCalculator.ComputeSha256(str);

    /// <summary>
    /// Computes the SHA384 hash of a file.
    /// </summary>
    /// <param name="file">The file, whose hash value will be computed.</param>
    /// <returns>The Hash of the file.</returns>
    public HashValue Sha384(File file)
        => HashCalculator.ComputeSha384(file, _rerporter);

    /// <summary>
    /// Computes the SHA384 hash of a string.
    /// </summary>
    /// <param name="str">The string, whose hash value will be computed.</param>
    /// <returns>The Hash of the sring.</returns>
    public HashValue Sha384(string str)
        => HashCalculator.ComputeSha384(str);

    /// <summary>
    /// Computes the SHA512 hash of a file.
    /// </summary>
    /// <param name="file">The file, whose hash value will be computed.</param>
    /// <returns>The Hash of the file.</returns>
    public HashValue Sha512(File file)
        => HashCalculator.ComputeSha512(file, _rerporter);

    /// <summary>
    /// Computes the SHA512 hash of a string.
    /// </summary>
    /// <param name="str">The string, whose hash value will be computed.</param>
    /// <returns>The Hash of the sring.</returns>
    public HashValue Sha512(string str)
        => HashCalculator.ComputeSha512(str);

    /// <summary>
    /// Computes the Md5 hash of a file.
    /// </summary>
    /// <param name="file">The file, whose hash value will be computed.</param>
    /// <returns>The Hash of the file.</returns>
    public HashValue Md5(File file)
        => HashCalculator.ComputeMd5(file, _rerporter);

    /// <summary>
    /// Computes the Md5 hash of a string.
    /// </summary>
    /// <param name="str">The string, whose hash value will be computed.</param>
    /// <returns>The Hash of the sring.</returns>
    public HashValue Md5(string str)
        => HashCalculator.ComputeMd5(str);

    /// <summary>
    /// Computes the Crc32 hash of a file.
    /// </summary>
    /// <param name="file">The file, whose hash value will be computed.</param>
    /// <returns>The Hash of the file.</returns>
    public HashValue Crc32(File file)
        => HashCalculator.ComputeCrc32(file, _rerporter);

    /// <summary>
    /// Computes the Crc32 hash of a string.
    /// </summary>
    /// <param name="str">The string, whose hash value will be computed.</param>
    /// <returns>The Hash of the sring.</returns>
    public HashValue Crc32(string str)
        => HashCalculator.ComputeCrc32(str);

    /// <summary>
    /// Computes the Crc64 hash of a file.
    /// </summary>
    /// <param name="file">The file, whose hash value will be computed.</param>
    /// <returns>The Hash of the file.</returns>
    public HashValue Crc64(File file)
        => HashCalculator.ComputeCrc64(file, _rerporter);

    /// <summary>
    /// Computes the Crc64 hash of a string.
    /// </summary>
    /// <param name="str">The string, whose hash value will be computed</param>
    /// <returns>The Hash of the sring</returns>
    public HashValue Crc64(string str)
        => HashCalculator.ComputeCrc64(str);

    /// <summary>
    /// Returns the maximum value in the sample data. Returns NaN if data is empty or if any entry is NaN.
    /// </summary>
    /// <param name="data">The sample data.</param>
    /// <returns>The maximum value in the sample data.</returns>
    public double Maximum(params double[] data)
        => MathNet.Numerics.Statistics.Statistics.Maximum(data);

    /// <summary>
    ///  Returns the maximum absolute value in the sample data. Returns NaN if data is empty or if any entry is NaN.
    /// </summary>
    /// <param name="data">The sample data.</param>
    /// <returns>The maximum value in the sample data.</returns>
    public double MaximumAbsolute(params double[] data)
        => MathNet.Numerics.Statistics.Statistics.MaximumAbsolute(data);

    /// <summary>
    /// Returns the minimum value in the sample data. Returns NaN if data is empty or if any entry is NaN.
    /// </summary>
    /// <param name="data">The sample data.</param>
    /// <returns>The minimum value in the sample data.</returns>
    public double Minimum(params double[] data)
        => MathNet.Numerics.Statistics.Statistics.Minimum(data);

    /// <summary>
    /// Returns the minimum absolute value in the sample data. Returns NaN if data is  empty or if any entry is NaN.
    /// </summary>
    /// <param name="data">The sample data.</param>
    /// <returns>The minimum value in the sample data.</returns>
    public double MinimumAbsolute(params double[] data)
        => MathNet.Numerics.Statistics.Statistics.MinimumAbsolute(data);

    /// <summary>
    /// Evaluates the sample mean, an estimate of the population mean. Returns NaN if data is empty or if any entry is NaN.
    /// </summary>
    /// <param name="data"> The data to calculate the mean of.</param>
    /// <returns>The mean of the sample.</returns>
    public double Mean(params double[] data)
        => MathNet.Numerics.Statistics.Statistics.Mean(data);

    /// <summary>
    /// Evaluates the geometric mean. Returns NaN if data is empty or if any entry is NaN.
    /// </summary>
    /// <param name="data">The data to calculate the geometric mean of.</param>
    /// <returns>The geometric mean of the sample.</returns>
    public double GeometricMean(params double[] data)
        => MathNet.Numerics.Statistics.Statistics.GeometricMean(data);

    /// <summary>
    /// Evaluates the harmonic mean. Returns NaN if data is empty or if any entry is NaN.
    /// </summary>
    /// <param name="data">The data to calculate the harmonic mean of.</param>
    /// <returns>The harmonic mean of the sample.</returns>
    public double HarmonicMean(params double[] data)
        => MathNet.Numerics.Statistics.Statistics.HarmonicMean(data);

    /// <summary>
    /// Evaluates the variance from the provided full population. On a dataset of size
    /// N will use an N normalizer and would thus be biased if applied to a subset. Returns
    /// NaN if data is empty or if any entry is NaN.
    /// </summary>
    /// <param name="data">The full population data.</param>
    /// <returns>The variance of the data</returns>
    public double Variance(params double[] data)
        => MathNet.Numerics.Statistics.Statistics.PopulationVariance(data);

    /// <summary>
    /// Evaluates the standard deviation from the provided full population. On a dataset
    /// of size N will use an N normalizer and would thus be biased if applied to a subset.
    /// Returns NaN if data is empty or if any entry is NaN.
    /// </summary>
    /// <param name="data">The full population data.</param>
    /// <returns>The Standard Deviation of the data</returns>
    public double StandardDeviation(params double[] data)
         => MathNet.Numerics.Statistics.Statistics.PopulationStandardDeviation(data);

    /// <summary>
    /// Evaluates the skewness from the full population. Does not use a normalizer and
    /// would thus be biased if applied to a subset (type 1). Returns NaN if data has
    /// less than two entries or if any entry is NaN.
    /// </summary>
    /// <param name="data">The full population data.</param>
    /// <returns>The Skewness of the data</returns>
    public double Skewness(params double[] data)
        => MathNet.Numerics.Statistics.Statistics.PopulationSkewness(data);

    /// <summary>
    /// Approximation of the definite integral of an analytic smooth function on a closed interval.
    /// </summary>
    /// <param name="f">The function to integrate</param>
    /// <param name="begin">Where the interval starts, inclusive and finite.</param>
    /// <param name="end">Where the interval stops, inclusive and finite.</param>
    /// <returns>Approximation of the finite integral in the given interval.</returns>
    public double Integrate(Func<double, double> f, double begin, double end)
        => MathNet.Numerics.Integrate.OnClosedInterval(f, begin, end, 1e-6);

    /// <summary>
    /// Create a new plot
    /// </summary>
    /// <returns>A plot data that's printable</returns>
    public PlotData CreatePlot()
        => new PlotData();

    /// <summary>
    /// Set Y axis title
    /// </summary>
    /// <param name="plot">Plot data that's Y axis title to set</param>
    /// <param name="title">Y axis title</param>
    public void SetYAxis(PlotData plot, string title)
    {
        plot.Model.DefaultYAxis.Title = title;
    }

    /// <summary>
    /// Set X axis title
    /// </summary>
    /// <param name="plot">Plot data that's X axis title to set</param>
    /// <param name="title">Y axis title</param>
    public void SetXAxis(PlotData plot, string title)
    {
        plot.Model.DefaultXAxis.Title = title;
    }

    /// <summary>
    /// Set plot title
    /// </summary>
    /// <param name="plot">Plot data that's title to set</param>
    /// <param name="title">plot title</param>
    public void SetTitle(PlotData plot, string title)
    {
        plot.Model.Title = title;
    }

    /// <summary>
    /// Add a function series
    /// </summary>
    /// <param name="plot">Plot data to add series</param>
    /// <param name="function">Function to plot</param>
    /// <param name="title">Function title</param>
    /// <param name="from">start value</param>
    /// <param name="to">End value</param>
    /// <param name="step">Stepping value</param>
    public PlotData PlotSeries(PlotData plot, Func<double, double> function, string title, double from, double to, double? step = null)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(to, from);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(from, to);

        if (!step.HasValue)
        {
            step = (to - from) / 2048;
        }

        plot.Model.Series.Add(new FunctionSeries(function, from, to, step.Value, title));

        return plot;
    }
}
