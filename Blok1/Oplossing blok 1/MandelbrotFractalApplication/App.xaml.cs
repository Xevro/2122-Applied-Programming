using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using MandelbrotFractalApplication.Presentation;
using MandelbrotFractalApplication.Models;

namespace MandelbrotFractalApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<MainViewModel>();
            services.AddSingleton<MainWindow>();
            services.AddTransient<ILogic, Logic>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
