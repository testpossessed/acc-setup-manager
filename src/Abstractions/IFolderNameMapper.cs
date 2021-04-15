using System;

namespace ACCSetupManager.Abstractions
{
  public interface IFolderNameMapper
  {
    string GetFriendlyCircuitName(string folderName);
    string GetFriendlyVehicleName(string folderName);
  }
}