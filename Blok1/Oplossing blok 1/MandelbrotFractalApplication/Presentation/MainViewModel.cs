﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MandelbrotFractalApplication.Models;
using System;
using System.Diagnostics;
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

        private const int MaxRow = Width;
        private const int MaxColumn = Height;

        public int selectedIterations = 250;
        public string selectedColorMode = Enum.GetName(typeof(ColorGradients), ColorGradients.Multicolor);

        public double zoomScale = 1;

        public double xOffset = 0;
        public double yOffset = 0;

        private bool working = false;

        public string CalculationTime { get; private set; }

        public string ZoomScale { get; private set; }

        public string OffsetX { get; private set; }
        public string OffsetY { get; private set; }

        public static string Title => "WPF - Mandelbrot Application - Louis D'Hont";

        private readonly ILogic logic;

        public WriteableBitmap BitmapDisplay { get; private set; }

        public IRelayCommand CalculateCommand { get; private set; }

        public MainViewModel(ILogic logic)
        {
            this.logic = logic;
            CalculateCommand = new RelayCommand(async () => await MandelbrotCalculation(), () => !working);
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

        public void ResetMandelbrot()
        {
            zoomScale = 1;
            xOffset = 0;
            yOffset = 0;
            CalculateCommand.Execute(null);
        }

        private async Task MandelbrotCalculation()
        {
            working = true;
            CalculateCommand.NotifyCanExecuteChanged();
            GenerateBitmapAsync();
            working = false;
            CalculateCommand.NotifyCanExecuteChanged();
        }

        private void GenerateBitmapAsync()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int[,] mandelbrotDepthValues = logic.CalcMandelbrotDepthAsync(zoomScale, xOffset, yOffset, Width, Height, selectedIterations);
            uint[,] bitmapPixels = new uint[Width, Height];

            for (int x = 0; x < Height; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    var color = GetColor(mandelbrotDepthValues[x, y]);
                    bitmapPixels[x, y] = BitConverter.ToUInt32(new byte[] { color.B, color.G, color.R, color.A });
                }
            }

            var rectangle = new Int32Rect(0, 0, Height, Width);
            BitmapDisplay.WritePixels(rectangle, bitmapPixels, BitmapDisplay.BackBufferStride, 0, 0);

            sw.Stop();
            ChangeProperties(sw);
        }

        private void ChangeProperties(Stopwatch sw)
        {
            ZoomScale = "Zoomscale: " + zoomScale;
            OffsetX = "X offset: " + xOffset;
            OffsetY = "Y offset: " + yOffset;
            CalculationTime = "Calc. time: " + sw.ElapsedMilliseconds +
                "ms (" + (double)(sw.ElapsedMilliseconds / 1000.0) + "sec)";
            OnPropertyChanged(nameof(CalculationTime));
            OnPropertyChanged(nameof(ZoomScale));
            OnPropertyChanged(nameof(OffsetX));
            OnPropertyChanged(nameof(OffsetY));
        }

        public Color GetColor(int depthIteration)
        {
            int calculatedColor = depthIteration * 255;

            if (selectedColorMode == Enum.GetName(typeof(ColorGradients), ColorGradients.Banding))
            {
                if (depthIteration % 2 == 0)
                {
                    return Color.FromRgb(0, 0, 0);
                }
                else
                {
                    return Color.FromRgb(255, 255, 255);
                }
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
