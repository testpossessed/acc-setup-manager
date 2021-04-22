using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using ACCSetupManager.Models;
using ACCSetupManager.ViewModels;

namespace ACCSetupManager.Services
{
  internal static class HierarchyProvider
  {
    private static readonly Dispatcher dispatcher;

    static HierarchyProvider()
    {
      dispatcher = Application.Current.Dispatcher;
    }

    internal static void AddMasterSetupFileToHierarchy(SetupFileInfo setupFileInfo,
      ICollection<VehicleViewModel> hierarchy)
    {
      var circuit = GetCircuitFromHierarchy(setupFileInfo, hierarchy);

      dispatcher.InvokeAsync(() => circuit.Setups.Add(new SetupViewModel
                                                      {
                                                        Name = Path.GetFileName(setupFileInfo
                                                          .MasterSetupFilePath),
                                                        FilePath = setupFileInfo.MasterSetupFilePath
                                                      }));
    }

    internal static void AddVersionedSetupFileToHierarchy(SetupFileInfo setupFileInfo,
      ICollection<VehicleViewModel> hierarchy)
    {
      var circuit = GetCircuitFromHierarchy(setupFileInfo, hierarchy);

      dispatcher.InvokeAsync(() => circuit.Setups.Add(new SetupViewModel
                                                      {
                                                        Name = Path.GetFileName(setupFileInfo
                                                          .VersionSetupFilePath),
                                                        FilePath =
                                                          setupFileInfo.VersionSetupFilePath
                                                      }));
    }

    internal static IEnumerable<VehicleViewModel> BuildMasterHierarchy()
    {
      return BuildHierarchy(PathProvider.MasterSetupsFolderPath, false);
    }

    internal static IEnumerable<VehicleViewModel> BuildVersionsHierarchy()
    {
      return BuildHierarchy(PathProvider.VersionsFolderPath, true);
    }

    internal static void DeleteSetupFromHierarchy(SetupFileInfo setupFileInfo,
      ICollection<VehicleViewModel> hierarchy)
    {
      var vehicle = hierarchy.FirstOrDefault(v => v.Identifier == setupFileInfo.VehicleIdentifier);
      if(vehicle != null)
      {
        var circuit =
          vehicle.Circuits.FirstOrDefault(c => c.Identifier == setupFileInfo.CircuitIdentifier);
        if(circuit == null)
        {
          return;
        }

        var setup =
          circuit.Setups.FirstOrDefault(s => s.FilePath == setupFileInfo.MasterSetupFilePath);
        if(setup != null)
        {
          dispatcher.InvokeAsync(() => circuit.Setups.Remove(setup));
        }
      }
    }

    internal static CircuitViewModel GetCircuitFromHierarchy(SetupFileInfo setupFileInfo,
      ICollection<VehicleViewModel> hierarchy)
    {
      var vehicle = hierarchy.FirstOrDefault(v => v.Identifier == setupFileInfo.VehicleIdentifier);
      if(vehicle == null)
      {
        vehicle = new VehicleViewModel
                  {
                    Identifier = setupFileInfo.VehicleIdentifier,
                    Name = FolderNameMapper.GetFriendlyVehicleName(setupFileInfo.VehicleIdentifier)
                  };
        dispatcher.InvokeAsync(() => hierarchy.Add(vehicle));
      }

      var circuit =
        vehicle.Circuits.FirstOrDefault(c => c.Identifier == setupFileInfo.CircuitIdentifier);
      if(circuit == null)
      {
        circuit = new CircuitViewModel
                  {
                    Identifier = setupFileInfo.CircuitIdentifier,
                    Name = FolderNameMapper.GetFriendlyCircuitName(setupFileInfo.CircuitIdentifier)
                  };

        dispatcher.InvokeAsync(() => vehicle.Circuits.Insert(0, circuit));
      }

      return circuit;
    }

    private static IEnumerable<VehicleViewModel> BuildHierarchy(string rootFolder, bool isVersion)
    {
      var result = new List<VehicleViewModel>();

      var vehicleFolderPaths = Directory.GetDirectories(rootFolder);
      foreach(var vehicleFolderPath in vehicleFolderPaths)
      {
        var vehicleIdentifier = PathProvider.GetLastFolderName(vehicleFolderPath);
        var vehicle = new VehicleViewModel
                      {
                        Identifier = vehicleIdentifier,
                        Name = FolderNameMapper.GetFriendlyVehicleName(vehicleIdentifier)
                      };

        var circuitFolderPaths = Directory.GetDirectories(vehicleFolderPath);
        foreach(var circuitFolderPath in circuitFolderPaths)
        {
          var circuitIdentifier = PathProvider.GetLastFolderName(circuitFolderPath);
          var circuit = new CircuitViewModel
                        {
                          Identifier = circuitIdentifier,
                          Name = FolderNameMapper.GetFriendlyCircuitName(circuitIdentifier)
                        };

          var setupFilePaths = Directory.GetFiles(circuitFolderPath);
          foreach(var setupFilePath in setupFilePaths)
          {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(setupFilePath);
            var setup = new SetupViewModel
                        {
                          Name = fileNameWithoutExtension,
                          FilePath = setupFilePath,
                          VehicleIdentifier = vehicleIdentifier,
                          CircuitIdentifier = circuitIdentifier,
                          IsVersion = isVersion
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