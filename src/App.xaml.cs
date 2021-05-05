using System;
using System.IO;
using System.Windows;
using ACCSetupManager.Services;
using ACCSetupManager.ViewModels;
using NLog;
using Syncfusion.Licensing;
using Syncfusion.SfSkinManager;

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

      LogWriter.Log(LogLevel.Info, "ACC Setup Manager started");
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

    private void HandleUnhandledException(object sender, UnhandledExceptionEventArgs eventArgs)
    {
      var exception = eventArgs.ExceptionObject as Exception;
      MessageBox.Show($"An unexpected error occurred: {exception?.Message}");
    }

    private void InitialiseApp()
    {
      AppDomain.CurrentDomain.UnhandledException += this.HandleUnhandledException;
      SyncfusionLicenseProvider.RegisterLicense(
        "NDM0NzY2QDMxMzkyZTMxMmUzMFl0aU45OEpZZzR2cEpFUUZEYnZLUC9DeXd5YkM4UFhRRDVEZXlDai9hUEE9");
      SfSkinManager.ApplyStylesOnApplication = true;
      this.EnsureAppDataFolderExists();
      this.EnsureConfigExists();
    }
  }
}