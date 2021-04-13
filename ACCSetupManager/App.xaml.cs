using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Windows;
using ACCSetupManager.Abstractions;
using ACCSetupManager.Services;
using ACCSetupManager.ViewModels;
using Microsoft.Extensions.Configuration;

namespace ACCSetupManager
{
    public partial class App : Application
    {
        private const string AppDocumentsFolderName = "ACC Setup Manager";
        private const string AppSettingsFileName = "appsettings.json";
        private string appDataFolderPath;
        private string appSettingsFilePath;

        private ServiceProvider serviceProvider;
        private IConfigurationRoot configuration;

        protected override void OnStartup(StartupEventArgs eventArgs)
        {
            this.InitialiseApp();
            this.LoadConfig();
            this.ConfigureServices();

            this.MainWindow = new MainWindow
                              {
                                  DataContext = this.serviceProvider.GetService<MainViewModel>()
                              };
            this.MainWindow.Show();
        }

        private void LoadConfig()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile(this.appSettingsFilePath, false, true);
            this.configuration = configurationBuilder.Build();
        }

        private void ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<ISetupFileProvider, SetupFileProvider>();
            serviceCollection.AddSingleton<MainViewModel>();

            this.serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void EnsureAppDataFolderExists()
        {
            if(!Directory.Exists(this.appDataFolderPath))
            {
                Directory.CreateDirectory(this.appDataFolderPath);
            }
        }

        private void EnsureConfigExists()
        {
            this.appSettingsFilePath = Path.Combine(this.appDataFolderPath, AppSettingsFileName);
            if(File.Exists(this.appSettingsFilePath))
            {
                return;
            }

            var appFolderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly()
                                                              .Location);
            var defaultSettingsFilePath = Path.Combine(appFolderPath!, AppSettingsFileName);
            File.Copy(defaultSettingsFilePath, this.appSettingsFilePath);
        }

        private void InitialiseApp()
        {
            this.InitialiseAppDataFolderPath();
            this.EnsureAppDataFolderExists();
            this.EnsureConfigExists();
        }

        private void InitialiseAppDataFolderPath()
        {
            var processName = Process.GetCurrentProcess()
                                     .ProcessName;
            var localAppDataFolderPath =
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            this.appDataFolderPath = Path.Combine(localAppDataFolderPath, processName);
        }
    }
}