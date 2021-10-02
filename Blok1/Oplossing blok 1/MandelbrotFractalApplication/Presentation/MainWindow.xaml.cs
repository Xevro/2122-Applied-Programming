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
            if (viewModel.CalculateCommand.CanExecute(null)) viewModel.CalculateCommand.Execute(null);
        }
    }
}
