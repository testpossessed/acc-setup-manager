using System;

namespace ACCSetupManager.Models
{
  public class SetupFileInfo
  {
    public string CircuitIdentifier { get; set; }
    public string FileName { get; set; }
    public string MasterSetupFilePath { get; set; }
    public string VehicleIdentifier { get; set; }
    public string VersionSetupFilePath { get; set; }
  }
}