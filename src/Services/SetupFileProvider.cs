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

    internal static IList<SetupFileInfo> GetMasters(string vehicleIdentifier,
      string circuitIdentifier)
    {
      var mastersFolderPath = Path.Combine(PathProvider.MasterSetupsFolderPath,
        vehicleIdentifier,
        circuitIdentifier);
      var filePaths = Directory.GetFiles(mastersFolderPath, "*.json");

      return filePaths.Select(fp => new SetupFileInfo
                                    {
                                      CircuitIdentifier = circuitIdentifier,
                                      VehicleIdentifier = vehicleIdentifier,
                                      FileName = Path.GetFileNameWithoutExtension(fp),
                                      MasterSetupFilePath =
                                        Path.Combine(PathProvider.MasterSetupsFolderPath,
                                          vehicleIdentifier,
                                          circuitIdentifier,
                                          Path.GetFileName(fp)),
                                      IsVersion = false
                                    })
                      .ToList();
    }

    internal static IList<SetupFileInfo> GetVersions(string vehicleIdentifier,
      string circuitIdentifier,
      bool setupIsVersion)
    {
      var versionsFolderPath = Path.Combine(PathProvider.VersionsFolderPath,
        vehicleIdentifier,
        circuitIdentifier);
      var filePaths = Directory.GetFiles(versionsFolderPath, "*.json");

      return filePaths.Select(fp => new SetupFileInfo
                                    {
                                      CircuitIdentifier = circuitIdentifier,
                                      VehicleIdentifier = vehicleIdentifier,
                                      FileName = Path.GetFileNameWithoutExtension(fp),
                                      MasterSetupFilePath =
                                        Path.Combine(PathProvider.MasterSetupsFolderPath,
                                          vehicleIdentifier,
                                          circuitIdentifier,
                                          (setupIsVersion? $"{GetPrefix(fp)}.json": Path.GetFileName(fp))),
                                      VersionSetupFilePath = fp,
                                      IsVersion = true
                                    })
                      .ToList();
    }

    internal static string GetPrefix(string filePath)
    {
      var fileName = Path.GetFileNameWithoutExtension(filePath);
      var length = fileName!.Length - 20;
      return $"{fileName.Substring(0, length)}-";
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