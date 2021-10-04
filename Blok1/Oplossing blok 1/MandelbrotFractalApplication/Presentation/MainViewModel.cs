using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MandelbrotFractalApplication.Models;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MandelbrotFractalApplication.Presentation
{
    public class MainViewModel : ObservableObject
    {
        private const int Width = 800;
        private const int Height = 800;

        private const int MaxRow = 800;
        private const int MaxColumn = 800;

        private const double MaxValueExtent = 2.0;

        public int selectedIterations = 250;
        public string selectedColorMode = Enum.GetName(typeof(ColorGradients), ColorGradients.Multicolor);

        public double zoomScale = 1;

        public double xOffset = 0;
        public double yOffset = 0;

        private bool working = false;

        private readonly ILogic logic;

        public WriteableBitmap BitmapDisplay { get; private set; }

        public IRelayCommand CalculateCommand { get; private set; }

        public IRelayCommand ResetCommand { get; private set; }

        public MainViewModel(ILogic logic)
        {
            this.logic = logic;
            ResetCommand = new RelayCommand(() => ResetMandelbrot());
            CalculateCommand = new RelayCommand(async () => await MandelbrotAsync(), () => !working);
            CreateBitmap(MaxColumn, MaxRow);
        }

        private void CreateBitmap(int width, int height)
        {
            double dpiX = 96d;
            double dpiY = 96d;
            var pixelFormat = PixelFormats.Pbgra32;
            BitmapDisplay = new WriteableBitmap(width, height, dpiX, dpiY, pixelFormat, null);
            OnPropertyChanged(nameof(BitmapDisplay));
        }

        private void ResetMandelbrot()
        {
            zoomScale = 1;
            xOffset = 0;
            yOffset = 0;
            CalculateCommand.Execute(null);
        }

        private async Task MandelbrotAsync()
        {
            working = true;
            CalculateCommand.NotifyCanExecuteChanged();
            GenerateBitmap();
            working = false;
            CalculateCommand.NotifyCanExecuteChanged();
        }

        private void GenerateBitmap()
        {
            var pixelBlock = new Color[Height, Width];
            double scale = MaxValueExtent / Math.Min(Width, Height);
            uint[,] pixels = new uint[Height, Width];

            Parallel.For(0, Height, x =>
            {
                Parallel.For(0, Width, y =>
                {
                    double a = (Width / 2 - x) * scale / zoomScale + 0.41687344;
                    double b = (y - Height / 2) * scale / zoomScale + 0.3434739373;
                    int mutationsDepth = logic.CalcMandelbrotDepth(new ComplexNumber(b, a), selectedIterations);

                    var color = GetColor(mutationsDepth);  
                    pixels[x, y] = BitConverter.ToUInt32(new byte[] { color.B, color.G, color.R, color.A });
                });
            });
            var rectangle = new Int32Rect(0, 0, Height, Width);
            BitmapDisplay.WritePixels(rectangle, pixels, BitmapDisplay.BackBufferStride, 0, 0);
        }

        public Color GetColor(int depthIteration)
        {
            int calculatedColor = depthIteration * 255;

            if (selectedColorMode == Enum.GetName(typeof(ColorGradients), ColorGradients.Banding))
            {
                byte red = (byte)(calculatedColor / 255);
                byte green = (byte)(calculatedColor / 255);
                byte blue = (byte)(calculatedColor / 255);
                return Color.FromArgb(255, red, green, blue);
            }
            else if (selectedColorMode == Enum.GetName(typeof(ColorGradients), ColorGradients.Grayscale))
            {
                byte red = (byte)(calculatedColor);
                byte green = (byte)(calculatedColor);
                byte blue = (byte)(calculatedColor);
                return Color.FromRgb(red, green, blue);
            }
            else if (selectedColorMode == Enum.GetName(typeof(ColorGradients), ColorGradients.Multicolor))
            {
                byte red = (byte)(calculatedColor / 64);
                byte green = (byte)(calculatedColor / 84);
                byte blue = (byte)(calculatedColor / 20);
                return Color.FromRgb(red, green, blue);
            }
            else
            {
                return Color.FromRgb(0, 0, 0);
            }
        }
    }
}
