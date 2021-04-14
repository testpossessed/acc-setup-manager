using System;
using ACCSetupManager.Models;

namespace ACCSetupManager.Abstractions
{
  public interface ISetupFileProvider
  {
    void CreateNewVersionFromSource(string carFolderName, string trackFolderName, string? masterSetupFileName);
    SetupFile GetSetupFile(string filePath);
    string[] GetSourceSetupFilePaths();
    SetupFile LatestVersion(string carIdentifier, string trackIdentifier, string masterSetupFileName);
  }
}