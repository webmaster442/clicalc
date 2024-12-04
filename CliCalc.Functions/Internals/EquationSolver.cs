using System.Numerics;

namespace CliCalc.Functions.Internals;

internal static class EquationSolver
{
    private const double Delta = 0.00000000000001;

    public static EquationSolution Solve(params double[] numbers)
    {
        EquationSolution roots = new();

        if (numbers.Length > 4 || numbers.Length < 1)
            throw new ArgumentException("Only linear, quadratic and cubic equations are supported.");

        switch (numbers.Length)
        {
            case 0:
                break;
            case 2:
                roots.AddRange(Linear(numbers[1] / numbers[0]));
                break;
            case 3:
                roots.AddRange(Quadratic(numbers[1] / numbers[2], numbers[0] / numbers[2]));
                break;
            case 4:
                roots.AddRange(Cubic(numbers[2] / numbers[3], numbers[1] / numbers[3], numbers[0] / numbers[3]));
                break;
        }
        return roots;
    }

    private static IEnumerable<Complex> Linear(double a)
    {
        yield return -a;
    }

    private static IEnumerable<Complex> Quadratic(double b, double c)
    {
        Complex cplx = Complex.Sqrt(b * b - 4.0 * c);
        yield return (cplx - b) / 2.0;
        yield return (b + cplx) / -2.0;
    }

    private static IEnumerable<Complex> Cubic(double a, double b, double c)
    {
        const double SQRT2_DIV_3 = 0.86602540378443864676;

        double d = c;
        c = b;
        b = a;
        a = 1.0;

        double delta_0 = b * b - 3.0 * a * c;
        double delta_1 = 2.0 * b * b * b - 9.0 * a * b * c + 27.0 * a * a * d;

        if (Math.Abs(delta_0) <= Delta && Math.Abs(delta_1) <= Delta)
        {
            Complex root = new(-b / (3.0 * a), 0.0);
            yield return root;
            yield break;
        }

        Complex extra0 = Complex.Pow(delta_1 * delta_1 - 4.0 * delta_0 * delta_0 * delta_0, 1.0 / 2.0);

        Complex C = Complex.Pow((delta_1 + extra0) / 2.0, 1.0 / 3.0);
        if (Complex.Abs(C) <= Delta) C = Complex.Pow((delta_1 - extra0) / 2.0, 1.0 / 3.0); // delta_0 == 0

        Complex xi = new(-0.5, SQRT2_DIV_3);

        yield return x_k(0);
        yield return x_k(1);
        yield return x_k(2);

        Complex x_k(int k)
        {
            Complex xiRaisedToK = xi;
            for (int i = 0; i < k; i++) xiRaisedToK *= xi;
            return -1.0 / (3.0 * a) * (b + xiRaisedToK * C + delta_0 / (xiRaisedToK * C));
        }
    }
}
