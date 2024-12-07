using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliCalc.Functions.Internals;

internal static class Doubles
{
    public static bool IsCloseTo(double a, double b, double epsilon)
    {
        return Math.Abs(a - b) < epsilon;
    }
}
