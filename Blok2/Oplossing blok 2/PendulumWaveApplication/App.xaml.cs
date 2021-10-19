using Microsoft.Extensions.DependencyInjection;
using System;
using Wpf3DUtils;
using System.Windows;
using WpfMovingBall.Models;
using WpfMovingBall.Presentation;

namespace PendulumWaveApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddWpf3DUtils();
            services.AddSingleton<IWorld, World>();
            services.AddTransient<MainViewModel>();
            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}