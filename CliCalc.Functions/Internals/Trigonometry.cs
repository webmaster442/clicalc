// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

namespace CliCalc.Functions.Internals;
internal static class Trigonometry
{
    public static double RadToDeg(double rad) => rad * 180 / Math.PI;

    public static double DegToRad(double deg) => deg * Math.PI / 180;

    public static double GradToRad(double grad) => grad * Math.PI / 200;

    public static double RadToGrad(double rad) => rad * 200 / Math.PI;

    public static double Sin(double angle, AngleMode mode)
    {
        double rad = mode switch
        {
            AngleMode.Deg => DegToRad(angle),
            AngleMode.Grad => GradToRad(angle),
            _ => angle
        };
        return Math.Round(Math.Sin(rad), 8);
    }

    public static double Cos(double angle, AngleMode mode)
    {
        double rad = mode switch
        {
            AngleMode.Deg => DegToRad(angle),
            AngleMode.Grad => GradToRad(angle),
            _ => angle
        };
        return Math.Round(Math.Cos(rad), 8);
    }

    public static double Tan(double angle, AngleMode mode)
    {
        double rad = mode switch
        {
            AngleMode.Deg => DegToRad(angle),
            AngleMode.Grad => GradToRad(angle),
            _ => angle
        };
        return Math.Round(Math.Tan(rad), 8);
    }

    public static double Asin(double x, AngleMode mode)
    {
        double rad = Math.Asin(x);
        return mode switch
        {
            AngleMode.Deg => RadToDeg(rad),
            AngleMode.Grad => RadToGrad(rad),
            _ => rad
        };
    }

    public static double Acos(double x, AngleMode mode)
    {
        double rad = Math.Acos(x);
        return mode switch
        {
            AngleMode.Deg => RadToDeg(rad),
            AngleMode.Grad => RadToGrad(rad),
            _ => rad
        };
    }

    public static double Atan(double x, AngleMode mode)
    {
        double rad = Math.Atan(x);
        return mode switch
        {
            AngleMode.Deg => RadToDeg(rad),
            AngleMode.Grad => RadToGrad(rad),
            _ => rad
        };
    }
}
