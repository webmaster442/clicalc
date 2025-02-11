using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using CliCalc.Functions;
using CliCalc.Infrastructure;
using CliCalc.Interfaces;

using OxyPlot;

using Spectre.Console.Rendering;

using Webmaster442.WindowsTerminal;

namespace CliCalc.Engine.Renderers;

internal sealed class PlotDataRenderer : IObjectRenderer
{
    internal sealed class PngExporter : IExporter
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public double Resolution { get; set; }

        public PngExporter(int width, int height, double resolution = 96.0)
        {
            Width = width;
            Height = height;
            Resolution = resolution;
        }

        public void Export(IPlotModel model, Stream stream)
        {
            using ImageRenderContext imageRenderContext = new ImageRenderContext(Width, Height, model.Background, Resolution);
            double num = Resolution / 96.0;
            model.Update(updateData: true);
            model.Render(imageRenderContext, new OxyRect(0.0, 0.0, (double)Width / num, (double)Height / num));
            imageRenderContext.SaveAsPng(stream);
        }
    }

    internal sealed class SixelRenderable : IRenderable
    {
        private readonly PlotModel _model;

        public SixelRenderable(PlotModel model)
        {
            _model = model;
        }

        public Measurement Measure(RenderOptions options, int maxWidth)
        {
            return new Measurement(maxWidth, maxWidth);
        }

        public IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
        {
            int w = (Console.WindowWidth-1) * 10;
            int h = (Console.WindowHeight-1) * 20;

            if (_model.Series.Count < 1)
                yield break;

            if (Sixel.IsSupported)
            {
                using var ms = new MemoryStream();
                PngExporter exporter = new PngExporter(w, h, 96);
                exporter.Export(_model, ms);
                ms.Seek(0, SeekOrigin.Begin);
                yield return new Segment(Sixel.ImageToSixel(ms, (w, h)));
            }
            else
            {
                yield return new Segment("Sixel graphics is not supported on this terminal.");
            }
        }
    }

    public bool TryRender(object value,
                          CultureInfo culture,
                          AngleMode angleMode,
                          [NotNullWhen(true)] out IRenderable? renderable)
    {
        if (value is not PlotData plotData)
        {
            renderable = null;
            return false;
        }

        renderable = new SixelRenderable(plotData.Model);
        return true;
    }
}
