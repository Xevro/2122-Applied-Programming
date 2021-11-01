using System.Windows.Media.Media3D;

namespace WpfMovingBall.Models
{
    public class Beam
    {
        public Point3D AnchorPoint { get; set; }

        public double Length { get; set; }

        public double Angle { get; set; }

        public double RotationalDelta { get; set; }
    }
}
