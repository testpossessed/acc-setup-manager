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

    public void SyncMasters()
    {
      this.EnsureMasterSetupsFolderExists();
      this.UpdateMasterSetupsFromSource();
    }

    private void CopyFolderRecursively(string sourceFolder, string destinationFolder)
    {
      if(!Directory.Exists(destinationFolder))
      {
        Directory.CreateDirectory(destinationFolder);
      }

      var filePaths = Directory.GetFiles(sourceFolder);
      foreach(var filePath in filePaths)
      {
        var fileName = PathProvider.GetLastFolderName(filePath);
        var destinationFilePath = Path.Combine(destinationFolder, fileName);
        File.Copy(filePath, destinationFilePath, true);
      }

      var sourceFolderPaths = Directory.GetDirectories(sourceFolder);
      foreach(var sourceFolderPath in sourceFolderPaths)
      {
        var folderName = PathProvider.GetLastFolderName(sourceFolderPath);
        var targetFolderPath = Path.Combine(destinationFolder, folderName!);
        this.CopyFolderRecursively(sourceFolderPath, targetFolderPath);
      }
    }

    private void EnsureMasterSetupsFolderExists()
    {
      if(!Directory.Exists(PathProvider.MasterSetupsFolderPath))
      {
        Directory.CreateDirectory(PathProvider.MasterSetupsFolderPath);
      }
    }

    private void UpdateMasterSetupsFromSource()
    {
      var sourceFolder = PathProvider.AccSetupsFolderPath;
      var destinationFolder = PathProvider.MasterSetupsFolderPath;
      this.CopyFolderRecursively(sourceFolder, destinationFolder);
    }
  }
}