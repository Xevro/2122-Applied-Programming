namespace MandelbrotFractalApplication.Models
{
    public interface ILogic
    {
        public int[,] CalculateMandelbrotDepthAsync(double zoomScale, double xOffset, double yOffset, int width, int height, int maxIterations);
    }
}
