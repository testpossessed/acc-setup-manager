using System;

namespace ACCSetupManager.Abstractions
{
  public interface IFolderNameMapper
  {
    string GetFriendlyCarName(string folderName);
    string GetFriendlyTrackName(string folderName);
  }
}