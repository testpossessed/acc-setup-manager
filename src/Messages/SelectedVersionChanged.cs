using System;
using ACCSetupManager.ViewModels;
using ACCSetupManager.ViewModels.SetupFileViewer;

namespace ACCSetupManager.Messages
{
  public class SelectedVersionChanged
  {
    public SelectedVersionChanged(SetupFileViewModel setupFile,
      VersionListItemViewModel versionToCompare)
    {
      this.SetupFile = setupFile;
      this.VersionToCompare = versionToCompare;
    }

    public SetupFileViewModel SetupFile { get; }
    public VersionListItemViewModel VersionToCompare { get; }
  }
}