using System;
using System.IO;
using ACCSetupManager.Abstractions;
using ACCSetupManager.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public class MainViewModel : ObservableObject
  {
    private readonly IMasterSetupSync masterSetupSync;
    private readonly ISetupFileProvider setupFileProvider;
    private string statusMessage = "Started";

    public MainViewModel(IMasterSetupSync masterSetupSync, ISetupFileProvider setupFileProvider)
    {
      this.masterSetupSync = masterSetupSync;
      this.setupFileProvider = setupFileProvider;

      this.SyncMasterSetups();
      this.EnsureMastersAreVersioned();
    }

    public string StatusMessage
    {
      get => this.statusMessage;
      set => this.SetProperty(ref this.statusMessage, value);
    }

    private void EnsureFolderExists(string targetFolderPath)
    {
      if(!Directory.Exists(targetFolderPath))
      {
        Directory.CreateDirectory(targetFolderPath);
      }
    }

    private void EnsureMastersAreVersioned()
    {
      this.StatusMessage = "Updating versions for master setups...";
      this.EnsureFolderExists(PathProvider.VersionsFolderPath);
      var masterCarFolderPaths = Directory.GetDirectories(PathProvider.MasterSetupsFolderPath);
      foreach(var masterCarFolderPath in masterCarFolderPaths)
      {
        this.EnsureMastersAreVersionedForCar(masterCarFolderPath);
      }
    }

    private void EnsureMastersAreVersionedForCar(string masterCarFolderPath)
    {
      var folderName = PathProvider.GetLastFolderName(masterCarFolderPath);
      var versionedCarFolderPath = Path.Combine(PathProvider.VersionsFolderPath, folderName);
      this.EnsureFolderExists(versionedCarFolderPath);

      var masterTrackFolderPaths = Directory.GetDirectories(masterCarFolderPath);
      foreach(var masterTrackFolderPath in masterTrackFolderPaths)
      {
        this.EnsureMastersAreVersionedForTrack(versionedCarFolderPath, masterTrackFolderPath);
      }
    }

    private void EnsureMastersAreVersionedForTrack(string versionedCarFolderPath,
      string masterTrackFolderPath)
    {
      var trackFolderName = PathProvider.GetLastFolderName(masterTrackFolderPath);
      var carFolderName = PathProvider.GetLastFolderName(versionedCarFolderPath);
      var versionedTrackFolderPath = Path.Combine(versionedCarFolderPath, trackFolderName);
      this.EnsureFolderExists(versionedTrackFolderPath);

      var masterSetupFilePaths = Directory.GetFiles(masterTrackFolderPath, "*.json");
      foreach(var masterSetupFilePath in masterSetupFilePaths)
      {
        var masterSetupFileName = Path.GetFileName(masterSetupFilePath);
        var latestVersion =
          this.setupFileProvider.LatestVersion(carFolderName, trackFolderName, masterSetupFileName);
        if(latestVersion == null)
        {
          this.setupFileProvider.CreateNewVersionFromSource(carFolderName, trackFolderName, masterSetupFilePath);
        }
      }
    }

    private void SyncMasterSetups()
    {
      this.StatusMessage = "Synchronising master setups...";
      this.masterSetupSync.SyncMasters();
    }
  }
}