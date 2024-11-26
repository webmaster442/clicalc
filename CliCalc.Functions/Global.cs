﻿namespace CliCalc.Functions;

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
