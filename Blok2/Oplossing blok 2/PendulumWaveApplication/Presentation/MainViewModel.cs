using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf3DUtils;
using WpfMovingBall.Models;

namespace WpfMovingBall.Presentation
{
    public class MainViewModel:ObservableObject
    {
        private readonly IWorld _world;
        private readonly ICameraController _cameraController;

        private readonly SolidColorBrush[] _colorBrushList = new SolidColorBrush[]
     {
            new SolidColorBrush(Colors.Crimson),
            new SolidColorBrush(Colors.MediumBlue),
            new SolidColorBrush(Colors.Green),
            new SolidColorBrush(Colors.DarkOrange),
            new SolidColorBrush(Colors.Olive),
            new SolidColorBrush(Colors.DarkCyan),
            new SolidColorBrush(Colors.Brown),
            new SolidColorBrush(Colors.SteelBlue),
            new SolidColorBrush(Colors.Gold),
            new SolidColorBrush(Colors.MistyRose),
            new SolidColorBrush(Colors.PaleTurquoise),
            new SolidColorBrush(Colors.PeachPuff),
            new SolidColorBrush(Colors.Salmon),
            new SolidColorBrush(Colors.Silver),
     };

        private readonly Model3DGroup _model3dGroup = new();
        private readonly Model3DGroup _sphereGroup = new();
        private GeometryModel3D _beam;
        private Model3DGroup _axesGroup;

        private bool _showAxes;
        
        public ProjectionCamera Camera => _cameraController.Camera;
        public Model3D Visual3dContent => _model3dGroup;
        public bool? ShowAxes
        {
            get => _showAxes;
            set
            {
                if (value == _showAxes) return;
                _showAxes = value ?? false;
                if (_showAxes)
                {
                    _model3dGroup.Children.Add(_axesGroup);
                }
                else
                {
                    _model3dGroup.Children.Remove(_axesGroup);
                }
            }
        }
        public int SphereCount => _world.SpherePositions.Count;

        public IRelayCommand AddSphereCommand { get; private set; }
        public IRelayCommand ResetCommand { get; private set; }

        public IRelayCommand MoveCommand { get; private set; }

        public MainViewModel(IWorld world, ICameraController cameraController)
        {
            _world = world;
            _cameraController = cameraController;
            _model3dGroup.Children.Add(_sphereGroup);
            AddSphereCommand = new RelayCommand(AddSphere, () => SphereCount < 40);
            ResetCommand = new RelayCommand(Reset, () => SphereCount > 0);
            MoveCommand = new RelayCommand(Move, () => SphereCount > 0);
            Init3DPresentation();
            InitBeam();
            AddSphere();
        }

        #region World Presentation

        private void InitBeam()
        {
            if (_beam != null) _model3dGroup.Children.Remove(_beam);
            var brush = _colorBrushList[^1];
            var matGroup = new MaterialGroup();
            matGroup.Children.Add(new DiffuseMaterial(brush));
            matGroup.Children.Add(new SpecularMaterial(brush, 100));
            _beam = Models3D.CreateLine(start: _world.Origin,
                                           end: _world.Origin + (_world.Beam.Length * new Vector3D(1, 0, 0)),
                                           thickness: 10,
                                           brush: brush);
            var transform = new Transform3DGroup();
            transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), _world.Beam.Angle)));
            transform.Children.Add(new TranslateTransform3D(_world.Beam.AnchorPoint - _world.Origin));
            _beam.Transform = transform;
            _model3dGroup.Children.Add(_beam);

        }

        private void AddSphere()
        {
            _world.AddSphere();
            var brush = _colorBrushList[_world.SpherePositions.Count % _colorBrushList.Length];
            var matGroup = new MaterialGroup();
            matGroup.Children.Add(new DiffuseMaterial(brush));
            matGroup.Children.Add(new SpecularMaterial(brush, 100));
            var sphere = Models3D.CreateSphere(matGroup);
            var transform = new Transform3DGroup();
            transform.Children.Add(new ScaleTransform3D(20, 20, 20));
            transform.Children.Add(new TranslateTransform3D(_world.SpherePositions[^1] - _world.Origin)); 
            sphere.Transform = transform;
            _sphereGroup.Children.Add(sphere);
            AddSphereCommand.NotifyCanExecuteChanged();
            ResetCommand.NotifyCanExecuteChanged();
            MoveCommand.NotifyCanExecuteChanged();
            OnPropertyChanged(nameof(SphereCount));
        }

        private void Reset()
        {
            _world.Reset();
            _sphereGroup.Children.Clear();
            InitBeam();
            AddSphereCommand.NotifyCanExecuteChanged();
            ResetCommand.NotifyCanExecuteChanged();
            MoveCommand.NotifyCanExecuteChanged();
            OnPropertyChanged(nameof(SphereCount));
        }

        private void Move()
        {
            _world.MoveObjects();
            MoveBeam();
            MoveSpheres();
        }

        private void MoveSpheres()
        {
            for (int i = 0; i < _world.SpherePositions.Count; i++)
            {
                var transform = new Transform3DGroup();
                transform.Children.Add(new ScaleTransform3D(20, 20, 20));
                transform.Children.Add(new TranslateTransform3D(_world.SpherePositions[i] - _world.Origin));
                _sphereGroup.Children[i].Transform = transform;
            }
        }

        private void MoveBeam()
        {
            var transform = new Transform3DGroup();
            transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), _world.Beam.Angle)));
            transform.Children.Add(new TranslateTransform3D(_world.Beam.AnchorPoint - _world.Origin));
            _beam.Transform = transform;
        }

        #endregion World Presentation

        #region Presentation setup

        private void Init3DPresentation()
        {
            SetupCamera();
            SetUpLights();
            CreateAxesGroup();            
            ShowAxes = true;
        }

        private void SetUpLights()
        {
            _model3dGroup.Children.Add(new AmbientLight(Colors.Gray));
            var direction = new Vector3D(1.5, -3, -5);
            _model3dGroup.Children.Add(new DirectionalLight(Colors.Gray, direction));
        }

        private void CreateAxesGroup()
        {
            double xLength = Math.Abs(_world.Bounds.p2.X - _world.Bounds.p1.X) / 2;
            double yLength = Math.Abs(_world.Bounds.p2.Y - _world.Bounds.p1.Y) / 2;
            double zLength = Math.Abs(_world.Bounds.p2.Z - _world.Bounds.p1.Z) / 2;
            double thickness = (_world.Bounds.p2 - _world.Bounds.p1).Length / 500;
            _axesGroup = new Model3DGroup();
            _axesGroup.Children.Add(Models3D.CreateLine(new Point3D(xLength, 0, 0), new Point3D(0, 0, 0), thickness, Brushes.Red));
            _axesGroup.Children.Add(Models3D.CreateLine(new Point3D(0, yLength, 0), new Point3D(0, 0, 0), thickness, Brushes.Green));
            _axesGroup.Children.Add(Models3D.CreateLine(new Point3D(0, 0, zLength), new Point3D(0, 0, 0), thickness, Brushes.Blue));
            _axesGroup.Freeze();
        }

        #endregion Presentation setup



        #region Camera control

        private void SetupCamera()
        {
            double l1 = (_world.Bounds.p1 - _world.Origin).Length;
            double l2 = (_world.Bounds.p2 - _world.Origin).Length;
            double radius = 2.3 * Math.Max(l1, l2);
            _cameraController.PositionCamera(radius, Math.PI / 10, 2.0 * Math.PI / 5);
        }

        public void ProcessKey(Key key)
        {
            _cameraController.ControlByKey(key);
        }

        public void Zoom(int Delta)
        {
            _cameraController.Zoom(Delta);
        }

        public void ControlByMouse(Vector vector)
        {
            _cameraController.ControlByMouse(vector);
        }


        #endregion Camera control


    }
}
