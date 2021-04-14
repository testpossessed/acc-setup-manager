using System;
using System.IO;
using System.Windows;
using ACCSetupManager.Abstractions;
using ACCSetupManager.Services;
using ACCSetupManager.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Extensions.Logging;

namespace ACCSetupManager
{
  public partial class App : Application
  {
    private IConfigurationRoot configuration;
    private Logger logger;
    private ServiceProvider serviceProvider;

    protected override void OnStartup(StartupEventArgs eventArgs)
    {
      this.InitialiseApp();
      this.LoadConfig();
      this.ConfigureLogging();
      this.ConfigureServices();

      this.MainWindow = new MainWindow
                        {
                          DataContext = this.serviceProvider.GetService<MainViewModel>()
                        };
      this.MainWindow.Show();
    }

    private void ConfigureLogging()
    {
      this.logger = LogManager.Setup()
                              .LoadConfigurationFromSection(this.configuration)
                              .GetCurrentClassLogger();
      LogManager.Configuration.Variables["appDataFolder"] = PathProvider.AppDataFolderPath;
      this.logger.Log(LogLevel.Info, "ACC Setup Manager started");
    }

    private void ConfigureServices()
    {
      var serviceCollection = new ServiceCollection();

      serviceCollection.AddSingleton<ISetupFileProvider, SetupFileProvider>();
      serviceCollection.AddSingleton<IMasterSetupSync, MasterSetupSync>();
      serviceCollection.AddSingleton<IFolderNameMapper, FolderNameMapper>();

      serviceCollection.AddSingleton<MainViewModel>();

      this.serviceProvider = serviceCollection.BuildServiceProvider();
    }

    private void EnsureAppDataFolderExists()
    {
      if(!Directory.Exists(PathProvider.AppDataFolderPath))
      {
        Directory.CreateDirectory(PathProvider.AppDataFolderPath);
      }
    }

    private void EnsureConfigExists()
    {
      if(File.Exists(PathProvider.AppSettingsFilePath))
      {
        return;
      }

      File.Copy(PathProvider.DefaultSettingsFilePath, PathProvider.AppSettingsFilePath);
    }

    private void InitialiseApp()
    {
      this.EnsureAppDataFolderExists();
      this.EnsureConfigExists();
    }

    private void LoadConfig()
    {
      var configurationBuilder = new ConfigurationBuilder();
      configurationBuilder.SetBasePath(PathProvider.AppDataFolderPath)
                          .AddJsonFile(PathProvider.AppSettingsFileName, false, true)
                          .AddEnvironmentVariables();
      this.configuration = configurationBuilder.Build();
    }
  }
}