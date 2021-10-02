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
        private readonly ILogic logic;

        public WriteableBitmap BitmapDisplay { get; private set; }

        public IRelayCommand CalculateCommand { get; private set; }

        private const int Width = 800;
        private const int Height = 800;

        private const int MaxRow = 800;
        private const int MaxColumn = 800;

        private const double MaxValueExtent = 2.0;

        private bool working = false;

        public MainViewModel(ILogic logic)
        {
            this.logic = logic;
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

        private async Task MandelbrotAsync()
        {
            working = true;
            CalculateCommand.NotifyCanExecuteChanged();
            await GenerateBitmap();
            working = false;
            CalculateCommand.NotifyCanExecuteChanged();
        }

        private async Task GenerateBitmap()
        {
            var pixelBlock = new Color[Height, Width];
            double scale = MaxValueExtent / Math.Min(Width, Height);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    double a = (Width / 2 - x) * scale;
                    double b = (y - Height / 2) * scale;
                    int mutations = logic.CalcMandelbrotDepth(new ComplexNumber(b, a), 250);

                    pixelBlock[x, y] = GetColor(mutations);
                };
            };
            SetBlock(pixelBlock, 0, 0);
        }

        private void SetBlock(Color[,] colors, int startRow, int startColumn)
        {
            int numberOfRows = colors.GetLength(1);
            int numberOfColumns = colors.GetLength(0);
            uint[,] pixels = new uint[numberOfColumns, numberOfRows];
            for (int row = 0; row < numberOfRows; row++)
            {
                for (int column = 0; column < numberOfColumns; column++)
                {
                    var color = colors[column, row];
                    pixels[column, row] = BitConverter.ToUInt32(new byte[] { color.B, color.G, color.R, color.A });
                }
            }
            var rectangle = new Int32Rect(0, 0, numberOfColumns, numberOfRows);
            BitmapDisplay.WritePixels(rectangle, pixels, BitmapDisplay.BackBufferStride, startColumn, startRow);
        }

        public static Color GetColor(int value)
        {
            if (value % 2 == 0)
            {
                return Color.FromRgb(0, 0, 0);
            }
            else
            {
                return Color.FromRgb(255, 255, 255);
            }
        }
    }
}
