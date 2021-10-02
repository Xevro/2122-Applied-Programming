using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MandelbrotFractalApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MandelbrotFractalApplication.Presentation
{
    public class MainViewModel : ObservableObject
    {

        private const int maxRow = 800;
        private const int maxColumn = 800;

        private readonly ILogic logic;

        private readonly Color[] colors = new[] { Colors.Red, Colors.Blue, Colors.Green, Colors.Yellow };

        public WriteableBitmap BitmapDisplay { get; private set; }

        public IRelayCommand DoWorkCommand { get; private set; }

        private bool working = false;

        public MainViewModel(ILogic logic)
        {
            this.logic = logic;
            DoWorkCommand = new RelayCommand(async () => await DoWorkAsync(), () => !working);
            CreateBitmap(maxColumn, maxRow);
        }

        private void CreateBitmap(int width, int height)
        {
            double dpiX = 96d;
            double dpiY = 96d;
            var pixelFormat = PixelFormats.Pbgra32;
            BitmapDisplay = new WriteableBitmap(width, height, dpiX, dpiY, pixelFormat, null);
            OnPropertyChanged(nameof(BitmapDisplay));
        }

        private async Task DoWorkAsync()
        {
            working = true;
            DoWorkCommand.NotifyCanExecuteChanged();
            await ShowPixels();
            working = false;
            DoWorkCommand.NotifyCanExecuteChanged();
        }

        private async Task ShowPixels()
        {
            var pointList = await logic.GetPointsAsync(10000);
            var color = colors[2];
            int rowCount = maxRow;
            var pixelBlock = new Color[maxColumn, rowCount];
            foreach (var point in pointList)
            {
                int column = (int)(maxColumn * point.X);
                int row = (int)(rowCount * point.Y);
                pixelBlock[column, row] = color;
            }
            SetBlock(pixelBlock, 0, 0);
        }

        private void SetBlock(Color[,] colors, int startRow, int startColumn)
        {
            // Set a specific block of the bitmap to the specified colors

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
    }
}
