﻿using MandelbrotFractalApplication.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MandelbrotFractalApplication.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel viewModel;

        private readonly int[] Iterations = { 50, 100, 250, 500, 1000, 2000, 5000 };

        private readonly string[] MandelbrotColors = {
            Enum.GetName(typeof(ColorGradients), ColorGradients.Banding),
            Enum.GetName(typeof(ColorGradients), ColorGradients.Grayscale),
            Enum.GetName(typeof(ColorGradients), ColorGradients.Multicolor)
        };

        private bool firstTimeIterations = true;
        private bool firstTimeColor = true;

        private string xDirection = "";
        private string yDirection = "";

        private int oldX = 0;
        private int oldY = 0;

        public MainWindow(MainViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
            PopulateIterationsComboBox();
            PopulateColorsComboBox();
            viewModel = DataContext as MainViewModel;
            RerenderMandelbrot();
        }

        private void PopulateIterationsComboBox()
        {
            IterationsCbx.ItemsSource = Iterations;
            IterationsCbx.SelectedIndex = 2;
        }

        private void PopulateColorsComboBox()
        {
            MandelbrotColorCbx.ItemsSource = MandelbrotColors;
            MandelbrotColorCbx.SelectedIndex = 2;
        }

        private void IterationsCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (firstTimeIterations)
            {
                firstTimeIterations = false;
                return;
            }
            viewModel.selectedIterations = (int)IterationsCbx.SelectedItem;
            RerenderMandelbrot();
        }

        private void MdbImage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                viewModel.zoomScale *= 1.4d;
                RerenderMandelbrot();
            }
            else if (e.Delta < 0)
            {
                viewModel.zoomScale /= 1.4d;
                RerenderMandelbrot();
            }
        }

        private void MdbImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (oldX < e.GetPosition(this).X)
                {
                    xDirection = "right";
                }
                else if (oldX > e.GetPosition(this).X)
                {
                    xDirection = "left";
                }

                if (oldY < e.GetPosition(this).Y)
                {
                    yDirection = "down";
                }
                else if (oldY > e.GetPosition(this).Y)
                {
                    yDirection = "up";
                }

                if (xDirection == "right")
                {
                    viewModel.xOffset -= (0.6 / viewModel.zoomScale);
                }
                if (xDirection == "left")
                {
                    viewModel.xOffset += (0.6 / viewModel.zoomScale);
                }
                if (yDirection == "up")
                {
                    viewModel.yOffset -= (0.6 / viewModel.zoomScale);
                }
                if (yDirection == "down")
                {
                    viewModel.yOffset += (0.6 / viewModel.zoomScale);
                }

                oldX = (int)e.GetPosition(this).X;
                oldY = (int)e.GetPosition(this).Y;
                RerenderMandelbrot();
            }
        }

        private void ResetMandelBrot_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ResetMandelbrot();
        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            viewModel.xOffset += (0.6 / viewModel.zoomScale);
            RerenderMandelbrot();
        }

        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            viewModel.xOffset -= (0.6 / viewModel.zoomScale);
            RerenderMandelbrot();
        }

        private void ButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            viewModel.yOffset -= (0.6 / viewModel.zoomScale);
            RerenderMandelbrot();
        }

        private void ButtonRight_Click(object sender, RoutedEventArgs e)
        {
            viewModel.yOffset += (0.6 / viewModel.zoomScale);
            RerenderMandelbrot();
        }

        private void MandelbrotColorCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (firstTimeColor)
            {
                firstTimeColor = false;
                return;
            }
            viewModel.selectedColorMode = (string)MandelbrotColorCbx.SelectedItem;
            RerenderMandelbrot();
        }

        private void RerenderMandelbrot()
        {
            if (viewModel.CalculateCommand.CanExecute(null)) viewModel.CalculateCommand.Execute(null);
        }
    }
}
