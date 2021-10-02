using System;
using System.Windows;

namespace MandelbrotFractalApplication.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MainViewModel viewModel;

        public MainWindow(MainViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
            viewModel = DataContext as MainViewModel;
            if (viewModel.DoWorkCommand.CanExecute(null)) viewModel.DoWorkCommand.Execute(null);
        }
    }
}
