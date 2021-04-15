using System;
using ACCSetupManager.Abstractions;
using Microsoft.Extensions.Configuration;

namespace ACCSetupManager.Services
{
  public class FolderNameMapper: IFolderNameMapper
  {
    private readonly IConfiguration configuration;

    public FolderNameMapper(IConfiguration configuration)
    {
      this.configuration = configuration;
    }

    public string GetFriendlyVehicleName(string folderName)
    {
      var carFolderConfig = this.configuration.GetSection("CarFolders");
      return carFolderConfig[folderName];
    }

    public string GetFriendlyCircuitName(string folderName)
    {
      var trackFolderConfig = this.configuration.GetSection("TrackFolders");
      return trackFolderConfig[folderName];
    }
  }
}
