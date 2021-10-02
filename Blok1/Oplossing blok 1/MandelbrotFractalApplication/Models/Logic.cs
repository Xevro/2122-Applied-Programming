namespace MandelbrotFractalApplication.Models
{
    class Logic : ILogic
    {
        public int CalcMandelbrotDepth(ComplexNumber c, int maxIterations)
        {
            const double MaxValueExtent = 2.0;
            const double MaxNorm = MaxValueExtent * MaxValueExtent;
            int iteration = 0;
            ComplexNumber z = new ComplexNumber();
            do
            {
                z = z * z + c;
                iteration++;
            } while (z.Norm() < MaxNorm && iteration < maxIterations);
            if (iteration < maxIterations)
                return iteration;
            else
                return 0;
        }
    }
}
