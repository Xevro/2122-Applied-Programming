namespace MandelbrotFractalApplication.Models
{
    public struct ComplexNumber
    {
        public double realAxis;
        public double imaginaryAxis;

        public ComplexNumber(double realAxis, double imaginaryAxis)
        {
            this.realAxis = realAxis;
            this.imaginaryAxis = imaginaryAxis;
        }

        public static ComplexNumber operator +(ComplexNumber x, ComplexNumber y)
        {
            return new ComplexNumber(x.realAxis + y.realAxis, x.imaginaryAxis + y.imaginaryAxis);
        }

        public static ComplexNumber operator *(ComplexNumber x, ComplexNumber y)
        {
            return new ComplexNumber(x.realAxis * y.realAxis - x.imaginaryAxis * y.imaginaryAxis,
                x.realAxis * y.imaginaryAxis + x.imaginaryAxis * y.realAxis);
        }


        public double Norm()
        {
            return realAxis * realAxis + imaginaryAxis * imaginaryAxis;
        }
    }
}
