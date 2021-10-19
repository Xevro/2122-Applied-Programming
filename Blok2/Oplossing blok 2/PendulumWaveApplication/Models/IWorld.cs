using System.Collections.Generic;
using System.Collections.Immutable;
using System.Windows.Media.Media3D;

namespace WpfMovingBall.Models
{
    public interface IWorld
    {
        Point3D Origin { get; }
        (Point3D p1, Point3D p2) Bounds { get; }

        public Beam Beam { get; }
        ImmutableList<Point3D> SpherePositions { get; }

        void Reset();
        void AddSphere();
        void MoveObjects();
    }
}