using System;
using System.IO;
using ACCSetupManager.Abstractions;

namespace ACCSetupManager.Services
{
  public class MasterSetupSync : IMasterSetupSync
  {
    private readonly ISetupFileProvider setupFileProvider;

    public MasterSetupSync(ISetupFileProvider setupFileProvider)
    {
      this.setupFileProvider = setupFileProvider;
    }

    public void SyncMasters(Action<string> statusCallback)
    {
      statusCallback("Synchronising master setups");
      this.EnsureMasterSetupsFolderExists();
      this.UpdateMasterSetupsFromSource(statusCallback);
    }

    private void CopyFolderRecursively(string sourceFolder,
      string destinationFolder,
      Action<string> statusCallback)
    {
      if(!Directory.Exists(destinationFolder))
      {
        statusCallback($"Creating destination folder {destinationFolder}");
        Directory.CreateDirectory(destinationFolder);
      }

      var filePaths = Directory.GetFiles(sourceFolder);
      foreach(var filePath in filePaths)
      {
        var fileName = Path.GetFileName(filePath);
        var destinationFilePath = Path.Combine(destinationFolder, fileName);
        statusCallback($"Updating {destinationFilePath}");
        File.Copy(filePath, destinationFilePath, true);
      }

      var sourceFolderPaths = Directory.GetDirectories(sourceFolder);
      foreach(var sourceFolderPath in sourceFolderPaths)
      {
        var folderName = PathProvider.GetLastFolderName(sourceFolderPath);
        var targetFolderPath = Path.Combine(destinationFolder, folderName!);
        statusCallback($"Processing ${targetFolderPath}");
        this.CopyFolderRecursively(sourceFolderPath, targetFolderPath, statusCallback);
      }
    }

    private void EnsureFolderExists(string targetFolderPath)
    {
      if(!Directory.Exists(targetFolderPath))
      {
        Directory.CreateDirectory(targetFolderPath);
      }
    }

    private void EnsureMastersAreVersioned(Action<string> statusCallback)
    {
      statusCallback("Updating versions for master setups");
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
          this.setupFileProvider.CreateNewVersionFromSource(carFolderName,
            trackFolderName,
            masterSetupFilePath);
        }
      }
    }

    private void EnsureMasterSetupsFolderExists()
    {
      if(!Directory.Exists(PathProvider.MasterSetupsFolderPath))
      {
        Directory.CreateDirectory(PathProvider.MasterSetupsFolderPath);
      }
    }

    private void UpdateMasterSetupsFromSource(Action<string> statusCallback)
    {
      var sourceFolder = PathProvider.AccSetupsFolderPath;
      var destinationFolder = PathProvider.MasterSetupsFolderPath;
      this.CopyFolderRecursively(sourceFolder, destinationFolder, statusCallback);
      this.EnsureMastersAreVersioned(statusCallback);
    }
  }
}