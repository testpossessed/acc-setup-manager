using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ACCSetupManager.Abstractions;
using ACCSetupManager.Models;

namespace ACCSetupManager.Services
{
  public class HierarchyBuilder : IHierarchyBuilder
  {
    private readonly IFolderNameMapper folderNameMapper;

    public HierarchyBuilder(IFolderNameMapper folderNameMapper)
    {
      this.folderNameMapper = folderNameMapper;
    }

    public IEnumerable<Vehicle> BuildMasterHierarchy()
    {
      return this.BuildHierarchy(PathProvider.MasterSetupsFolderPath);
    }

    public IEnumerable<Vehicle> BuildVersionsHierarchy()
    {
      return this.BuildHierarchy(PathProvider.VersionsFolderPath);
    }

    private IEnumerable<Vehicle> BuildHierarchy(string rootFolder)
    {
      var result = new List<Vehicle>();

      var masterVehicleFolderPaths = Directory.GetDirectories(rootFolder);
      foreach(var masterVehicleFolderPath in masterVehicleFolderPaths)
      {
        var vehicleIdentifier = PathProvider.GetLastFolderName(masterVehicleFolderPath);
        var vehicle = new Vehicle
                      {
                        FolderName = vehicleIdentifier,
                        Name = this.folderNameMapper.GetFriendlyCarName(vehicleIdentifier)
                      };

        var circuitFolderPaths = Directory.GetDirectories(masterVehicleFolderPath);
        foreach(var circuitFolderPath in circuitFolderPaths)
        {
          var circuitIdentifier = PathProvider.GetLastFolderName(circuitFolderPath);
          var circuit = new Circuit
                        {
                          FolderName = circuitIdentifier,
                          Name = this.folderNameMapper.GetFriendlyTrackName(circuitIdentifier)
                        };

          var setupFilePaths = Directory.GetFiles(circuitFolderPath);
          foreach(var setupFilePath in setupFilePaths)
          {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(setupFilePath);
            var setup = new Setup
                        {
                          Name = fileNameWithoutExtension,
                          FilePath = setupFilePath
                        };
            circuit.Setups.Add(setup);
          }

          vehicle.Circuits.Add(circuit);
        }

        result.Add(vehicle);
      }

      return result;
    }
  }
}