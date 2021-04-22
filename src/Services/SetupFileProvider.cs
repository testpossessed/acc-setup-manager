using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ACCSetupManager.Models;
using Newtonsoft.Json;

namespace ACCSetupManager.Services
{
  internal static class SetupFileProvider
  {
    internal static string CreateNewVersionFromSource(string carFolderName,
      string trackFolderName,
      string masterSetupFilePath)
    {
      var timestamp = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
      var versionedSetupFilePrefix = Path.GetFileNameWithoutExtension(masterSetupFilePath);
      var versionedFileName = $"{versionedSetupFilePrefix}-{timestamp}.json";
      var versionedFilePath = Path.Combine(PathProvider.VersionsFolderPath,
        carFolderName,
        trackFolderName,
        versionedFileName);

      File.Copy(masterSetupFilePath, versionedFilePath);
      return versionedFilePath;
    }

    internal static SetupFile GetSetupFile(string filePath)
    {
      var json = File.ReadAllText(filePath);
      return JsonConvert.DeserializeObject<SetupFile>(json);
    }

    internal static string[] GetSourceSetupFilePaths()
    {
      return Directory.GetFiles(PathProvider.AccSetupsFolderPath,
        "*.json",
        SearchOption.AllDirectories);
    }

    internal static IList<SetupFileInfo> GetVersions(string vehicleIdentifier,
      string circuitIdentifier,
      string prefix)
    {
      var versionsFolderPath = Path.Combine(PathProvider.VersionsFolderPath,
        vehicleIdentifier,
        circuitIdentifier);
      var filePaths = Directory.GetFiles(versionsFolderPath, $"{prefix}-*.json");

      return filePaths.Select(fp => new SetupFileInfo
                                    {
                                      CircuitIdentifier = circuitIdentifier,
                                      VehicleIdentifier = vehicleIdentifier,
                                      FileName = Path.GetFileNameWithoutExtension(fp),
                                      MasterSetupFilePath =
                                        Path.Combine(PathProvider.MasterSetupsFolderPath,
                                          vehicleIdentifier,
                                          circuitIdentifier,
                                          $"{prefix}.json"),
                                      VersionSetupFilePath = fp
                                    })
                      .ToList();
    }

    internal static SetupFile LatestVersion(string carIdentifier,
      string trackIdentifier,
      string masterSetupFileName)
    {
      var versionedTrackFolderPath =
        Path.Combine(PathProvider.VersionsFolderPath, carIdentifier, trackIdentifier);

      var versionedSetupFilePrefix = Path.GetFileNameWithoutExtension(masterSetupFileName);
      var filePaths =
        Directory.GetFiles(versionedTrackFolderPath, $"{versionedSetupFilePrefix}*.json");
      if(!filePaths.Any())
      {
        return null;
      }

      var latestFileInfo = filePaths.Select(fp => new FileInfo(fp))
                                    .OrderByDescending(fp => fp.CreationTimeUtc)
                                    .Last();

      return GetSetupFile(latestFileInfo.FullName);
    }

    internal static SetupFileInfo ParseFilePath(string filePath)
    {
      var result = new SetupFileInfo();

      var elements = filePath.Split("\\", StringSplitOptions.RemoveEmptyEntries);
      var lastIndex = elements.Length - 1;

      result.FileName = elements[lastIndex];
      result.CircuitIdentifier = elements[lastIndex - 1];
      result.VehicleIdentifier = elements[lastIndex - 2];

      result.MasterSetupFilePath = Path.Combine(PathProvider.MasterSetupsFolderPath,
        result.VehicleIdentifier,
        result.CircuitIdentifier,
        result.FileName);

      return result;
    }
  }
}