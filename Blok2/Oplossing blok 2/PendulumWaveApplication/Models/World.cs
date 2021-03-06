using System.Collections.Immutable;
using System.Windows.Media.Media3D;

namespace WpfMovingBall.Models
{
    internal class World : IWorld
    {
        private const int _worldSize = 1000;

        public Point3D Origin => new();
        public (Point3D p1, Point3D p2) Bounds { get; private set; }

        public Beam Beam { get; private set; }

        public ImmutableList<Point3D> SpherePositions { get; private set; }

        private int xOffset = 0;
        private int yOffset = 0;
        private int zOffset = 0;

        public World()
        {
            Bounds = (new Point3D(-_worldSize / 2, -_worldSize / 2, -_worldSize / 2),
                      new Point3D(_worldSize / 2, _worldSize / 2, _worldSize / 2));
            SpherePositions = ImmutableList<Point3D>.Empty;
            InitBeam();
        }

        private void InitBeam()
        {
            Beam = new Beam
            {
                AnchorPoint = GetBarPosition(),
                Angle = -90,
                Length = 1600,
                RotationalDelta = 0
            };
        }

        public void Reset()
        {
            SpherePositions = ImmutableList<Point3D>.Empty;
            InitBeam();
            xOffset = 0;
            yOffset = 0;
            zOffset = 0;
        }

        public void AddSphere()
        {
            xOffset += 0;
            yOffset += 8;
            zOffset += 40;
            var position = GetBalPosition(xOffset, yOffset, zOffset);
            SpherePositions = SpherePositions.Add(position);
        }

        public void MoveObjects()
        {
            var newPositions = ImmutableList<Point3D>.Empty;
            foreach (var position in SpherePositions)
            {
                var vector = new Vector3D(1.9, 0.3, 0);
                newPositions = newPositions.Add(position + vector);
            }
            SpherePositions = newPositions;
        }

        private Point3D GetBalPosition(int xOffset, int yOffset, int zOffset)
        {
            return new Point3D
            {
                X = xOffset,
                Y = 5 + yOffset,
                Z = zOffset
            };
        }

        private Point3D GetBarPosition()
        {
            return new Point3D
            {
                X = 0,
                Y = 500,
                Z = 0
            };
        }
    }
}
