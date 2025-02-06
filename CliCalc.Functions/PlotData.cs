using System.Globalization;

using OxyPlot;
using OxyPlot.Series;

namespace CliCalc.Functions;

/// <summary>
/// Plot data
/// </summary>
public sealed class PlotData
{
    internal PlotModel Model { get; }

    /// <summary>
    /// Creates a new instance of plot data
    /// </summary>
    public PlotData()
    {
        Model = new PlotModel
        {
            Background = OxyColor.FromRgb(255, 255, 255),
            Culture = CultureInfo.InvariantCulture,
        };
    }

    /// <summary>
    /// Number of elements in the plot
    /// </summary>
    public int ElementCount
        => Model.Series.Count;
}
