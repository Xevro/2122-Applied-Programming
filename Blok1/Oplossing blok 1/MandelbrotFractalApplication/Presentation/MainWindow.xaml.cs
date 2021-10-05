using MandelbrotFractalApplication.Models;
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

        private bool windowShowFirstTimeIterations = true;
        private bool windowShowFirstTimeColor = true;


        private string xDirection = "";
        private string yDirection = "";

        public int oldX = 0;
        public int oldY = 0;

        public MainWindow(MainViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
            PopulateIterationsComboBox();
            PopulateColorsComboBox();
            viewModel = DataContext as MainViewModel;
            if (viewModel.CalculateCommand.CanExecute(null)) viewModel.CalculateCommand.Execute(null);
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
            if (windowShowFirstTimeIterations)
            {
                windowShowFirstTimeIterations = false;
                return;
            }
            viewModel.selectedIterations = (int)IterationsCbx.SelectedItem;
            if (viewModel.CalculateCommand.CanExecute(null)) viewModel.CalculateCommand.Execute(null);
        }

        private void MdbImage_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                viewModel.zoomScale *= 1.4d;
                if (viewModel.CalculateCommand.CanExecute(null)) viewModel.CalculateCommand.Execute(null);
            }
            else if (e.Delta < 0)
            {
                viewModel.zoomScale /= 1.4d;
                if (viewModel.CalculateCommand.CanExecute(null)) viewModel.CalculateCommand.Execute(null);
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
                if (viewModel.CalculateCommand.CanExecute(null)) viewModel.CalculateCommand.Execute(null);
            }
        }

        private void ResetMandelBrot_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.ResetCommand.CanExecute(null)) viewModel.ResetCommand.Execute(null);
        }

        private void MandelbrotColorCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (windowShowFirstTimeColor)
            {
                windowShowFirstTimeColor = false;
                return;
            }
            viewModel.selectedColorMode = (string)MandelbrotColorCbx.SelectedItem;
            if (viewModel.CalculateCommand.CanExecute(null)) viewModel.CalculateCommand.Execute(null);
        }
    }
}
