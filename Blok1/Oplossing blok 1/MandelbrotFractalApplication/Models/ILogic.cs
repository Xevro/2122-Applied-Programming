using System.Threading;

namespace MandelbrotFractalApplication.Models
{
    public interface ILogic
    {
        public int[,] CalcMandelbrotDepthAsync(double zoomScale, double xOffset, double yOffset, int width, int height, int maxIterations);
    }
}
