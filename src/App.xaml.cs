using System;
using System.IO;
using System.Windows;
using ACCSetupManager.Services;
using ACCSetupManager.ViewModels;
using NLog;

namespace ACCSetupManager
{
  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs eventArgs)
    {
      this.InitialiseApp();
      Configuration.Init();
      LogWriter.Init();

      this.MainWindow = new MainWindow
                        {
                          DataContext = new MainViewModel()
                        };
      this.MainWindow.Show();

      LogWriter.Log(LogLevel.Info, "ACC SetupViewModel Manager started");
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
  }
}