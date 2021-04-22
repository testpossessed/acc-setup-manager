using System;
using System.IO;

namespace ACCSetupManager.Services
{
  internal static class MasterSetupSync
  {
    internal static void SyncMasters(Action<string> statusCallback)
    {
      statusCallback("Synchronising master setups");
      EnsureMasterSetupsFolderExists();
      UpdateMasterSetupsFromSource(statusCallback);
    }

    private static void CopyFolderRecursively(string sourceFolder,
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
        CopyFolderRecursively(sourceFolderPath, targetFolderPath, statusCallback);
      }
    }

    private static void EnsureFolderExists(string targetFolderPath)
    {
      if(!Directory.Exists(targetFolderPath))
      {
        Directory.CreateDirectory(targetFolderPath);
      }
    }

    private static void EnsureMastersAreVersioned(Action<string> statusCallback)
    {
      statusCallback("Updating versions for master setups");
      EnsureFolderExists(PathProvider.VersionsFolderPath);
      var masterCarFolderPaths = Directory.GetDirectories(PathProvider.MasterSetupsFolderPath);
      foreach(var masterCarFolderPath in masterCarFolderPaths)
      {
        EnsureMastersAreVersionedForCar(masterCarFolderPath);
      }
    }

    private static void EnsureMastersAreVersionedForCar(string masterCarFolderPath)
    {
      var folderName = PathProvider.GetLastFolderName(masterCarFolderPath);
      var versionedCarFolderPath = Path.Combine(PathProvider.VersionsFolderPath, folderName);
      EnsureFolderExists(versionedCarFolderPath);

      var masterTrackFolderPaths = Directory.GetDirectories(masterCarFolderPath);
      foreach(var masterTrackFolderPath in masterTrackFolderPaths)
      {
        EnsureMastersAreVersionedForTrack(versionedCarFolderPath, masterTrackFolderPath);
      }
    }

    private static void EnsureMastersAreVersionedForTrack(string versionedCarFolderPath,
      string masterTrackFolderPath)
    {
      var trackFolderName = PathProvider.GetLastFolderName(masterTrackFolderPath);
      var carFolderName = PathProvider.GetLastFolderName(versionedCarFolderPath);
      var versionedTrackFolderPath = Path.Combine(versionedCarFolderPath, trackFolderName);
      EnsureFolderExists(versionedTrackFolderPath);

      var masterSetupFilePaths = Directory.GetFiles(masterTrackFolderPath, "*.json");
      foreach(var masterSetupFilePath in masterSetupFilePaths)
      {
        var masterSetupFileName = Path.GetFileName(masterSetupFilePath);
        var latestVersion =
          SetupFileProvider.LatestVersion(carFolderName, trackFolderName, masterSetupFileName);
        if(latestVersion == null)
        {
          SetupFileProvider.CreateNewVersionFromSource(carFolderName,
            trackFolderName,
            masterSetupFilePath);
        }
      }
    }

    private static void EnsureMasterSetupsFolderExists()
    {
      if(!Directory.Exists(PathProvider.MasterSetupsFolderPath))
      {
        Directory.CreateDirectory(PathProvider.MasterSetupsFolderPath);
      }
    }

    private static void UpdateMasterSetupsFromSource(Action<string> statusCallback)
    {
      var sourceFolder = PathProvider.AccSetupsFolderPath;
      var destinationFolder = PathProvider.MasterSetupsFolderPath;
      CopyFolderRecursively(sourceFolder, destinationFolder, statusCallback);
      EnsureMastersAreVersioned(statusCallback);
    }
  }
}