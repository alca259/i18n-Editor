using i18nEditor.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace i18nEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public IServiceProvider ServiceProvider => _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.RegisterViewModels();
            serviceCollection.RegisterFactories();
            serviceCollection.RegisterNavigation();
            serviceCollection.RegisterWindowAndPages();
            serviceCollection.RegisterServices();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
