using System;
using System.IO;
using System.Linq;
using ACCSetupManager.Abstractions;
using ACCSetupManager.Models;
using Newtonsoft.Json;

namespace ACCSetupManager.Services
{
  public class SetupFileProvider: ISetupFileProvider
    {
      public string[] GetSourceSetupFilePaths()
      {
        return Directory.GetFiles(PathProvider.AccSetupsFolderPath, "*.json", SearchOption.AllDirectories);
      }

      public SetupFile LatestVersion(string carIdentifier, string trackIdentifier, string masterSetupFileName)
      {
        var versionedTrackFolderPath =
          Path.Combine(PathProvider.VersionsFolderPath, carIdentifier, trackIdentifier);

        var versionedSetupFilePrefix = Path.GetFileNameWithoutExtension(masterSetupFileName);
        var filePaths = Directory.GetFiles(versionedTrackFolderPath, $"{versionedSetupFilePrefix}*.json");
        if(!filePaths.Any())
        {
          return null;
        }

        var latestFileInfo = filePaths.Select(fp => new FileInfo(fp)).OrderByDescending(fp => fp.CreationTimeUtc).Last();

        return this.GetSetupFile(latestFileInfo.FullName);
      }

      public SetupFile GetSetupFile(string filePath)
      {
        var json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<SetupFile>(json);
      }

      public void CreateNewVersionFromSource(string carFolderName, string trackFolderName, string masterSetupFilePath)
      {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
        var versionedSetupFilePrefix = Path.GetFileNameWithoutExtension(masterSetupFilePath);
        var versionedFileName = $"{versionedSetupFilePrefix}-{timestamp}.json";
        var versionedFilePath = Path.Combine(PathProvider.VersionsFolderPath,
          carFolderName,
          trackFolderName,
          versionedFileName);

        File.Copy(masterSetupFilePath, versionedFilePath);
      }
    }
}
