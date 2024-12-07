using System.Collections;
using System.Numerics;

namespace CliCalc.Functions.Internals;

/// <summary>
/// Represents a collection of solutions for an equation.
/// </summary>
public sealed class EquationSolution : IEnumerable<IFormattable>
{
    private readonly HashSet<Complex> _rawSolutions;

    /// <summary>
    /// Creates a new instance of EquationSolution
    /// </summary>
    public EquationSolution()
    {
        _rawSolutions = new HashSet<Complex>();
    }

    /// <summary>
    /// Adds solution to the collection.
    /// </summary>
    /// <param name="solutions">solutions to add</param>
    public void AddRange(IEnumerable<Complex> solutions)
    {
        foreach (var solution in solutions)
        {
            _rawSolutions.Add(solution);
        }
    }

    /// <inheritdoc/>
    public IEnumerator<IFormattable> GetEnumerator()
    {
        foreach (var solution in _rawSolutions)
        {
            yield return Doubles.IsCloseTo(solution.Imaginary, 0.0, 1E-9) 
                ? solution.Real 
                : (IFormattable)solution;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}
