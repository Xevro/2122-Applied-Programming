namespace MandelbrotFractalApplication.Models
{
    public interface ILogic
    {
        public int CalcMandelbrotDepth(ComplexNumber c, int maxIterations);
    }
}
