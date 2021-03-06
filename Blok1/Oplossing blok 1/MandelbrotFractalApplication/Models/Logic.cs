using System;
using System.Threading.Tasks;

namespace MandelbrotFractalApplication.Models
{
    class Logic : ILogic
    {
        private const double MaxValueExtent = 2.0;

        // Source code: https://rosettacode.org/wiki/Mandelbrot_set#C.23
        // Used on 28/09/2021
        public int[,] CalculateMandelbrotDepthAsync(double zoomScale, double xOffset, double yOffset, int width, int height, int maxIterations)
        {
            int[,] pixels = new int[width, height];
            double scale = 2 * MaxValueExtent / Math.Min(width, height);
            Parallel.For(0, height, x =>
            {
                for (int y = 0; y < width; y++)
                {
                    double a = (width / 2 - x) * scale / zoomScale + xOffset;
                    double b = (y - height / 2) * scale / zoomScale + yOffset;
                    ComplexNumber c = new ComplexNumber(b, a);
                    const double MaxNorm = MaxValueExtent * MaxValueExtent;
                    int iteration = 0;
                    ComplexNumber z = new ComplexNumber();
                    do
                    {
                        z = z * z + c;
                        iteration++;
                    } while (z.Norm() < MaxNorm && iteration < maxIterations);
                    if (iteration < maxIterations)
                        pixels[x, y] = iteration;
                    else
                        pixels[x, y] = 0;
                }
            });
            return pixels;
        }
    }
}