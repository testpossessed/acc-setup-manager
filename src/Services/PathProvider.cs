using System;
using System.Diagnostics;
using System.IO;

namespace ACCSetupManager.Services
{
  public static class PathProvider
  {
    private const string AccFolderName = "Assetto Corsa Competizione";
    private const string AccSetupsFolderName = "Setups";
    public const string AppSettingsFileName = "appsettings.json";
    private const string MasterSetupsFolderName = "MasterSetups";
    private const string VersionsFolderName = "Versions";
    private const string SetupDataFolderName = "SetupData";
    private const string NotesFolderName = "Notes";

    static PathProvider()
    {
      var processName = Process.GetCurrentProcess()
                               .ProcessName;
      var localAppDataFolderPath =
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      AppDataFolderPath = Path.Combine(localAppDataFolderPath, processName);
      AppSettingsFilePath = Path.Combine(AppDataFolderPath, AppSettingsFileName);
      var executionFolder = AppDomain.CurrentDomain.BaseDirectory;
      AppFolderPath = Path.GetDirectoryName(executionFolder);
      DefaultSettingsFilePath = Path.Combine(AppFolderPath!, AppSettingsFileName);
      MasterSetupsFolderPath = Path.Combine(AppDataFolderPath, MasterSetupsFolderName);
      AccSetupsFolderPath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
          AccFolderName,
          AccSetupsFolderName);
      VersionsFolderPath = Path.Combine(AppDataFolderPath, VersionsFolderName);
      SetupDataFolderPath = Path.Combine(AppFolderPath, SetupDataFolderName);
      NotesFolderPath = Path.Combine(AppDataFolderPath, NotesFolderName);
    }

    public static string AccSetupsFolderPath { get; }
    public static string AppDataFolderPath { get; }
    public static string AppFolderPath { get; }
    public static string AppSettingsFilePath { get; }
    public static string DefaultSettingsFilePath { get; }
    public static string MasterSetupsFolderPath { get; }
    public static string NotesFolderPath { get; }
    public static string SetupDataFolderPath { get; }
    public static string VersionsFolderPath { get; set; }

    public static string GetLastFolderName(string path)
    {
      var folderPath = Path.HasExtension(path)? Path.GetDirectoryName(path): path;
      return new DirectoryInfo(folderPath!).Name;
    }
  }
}