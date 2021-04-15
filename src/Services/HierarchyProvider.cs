using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using ACCSetupManager.Abstractions;
using ACCSetupManager.Models;
using ACCSetupManager.ViewModels;

namespace ACCSetupManager.Services
{
  public class HierarchyProvider : IHierarchyProvider
  {
    private readonly Dispatcher dispatcher;
    private readonly IFolderNameMapper folderNameMapper;

    public HierarchyProvider(IFolderNameMapper folderNameMapper)
    {
      this.folderNameMapper = folderNameMapper;
      this.dispatcher = Application.Current.Dispatcher;
    }

    public void AddMasterSetupFileToHierarchy(SetupFileInfo setupFileInfo,
      ICollection<VehicleViewModel> hierarchy)
    {
      var circuit = this.GetCircuitFromHierarchy(setupFileInfo, hierarchy);

      this.dispatcher.InvokeAsync(() => circuit.Setups.Add(new SetupViewModel
                                                           {
                                                             Name = Path.GetFileName(setupFileInfo
                                                               .MasterSetupFilePath),
                                                             FilePath = setupFileInfo.MasterSetupFilePath
                                                           }));
    }

    public void AddVersionedSetupFileToHierarchy(SetupFileInfo setupFileInfo,
      ICollection<VehicleViewModel> hierarchy)
    {
      var circuit = this.GetCircuitFromHierarchy(setupFileInfo, hierarchy);

      this.dispatcher.InvokeAsync(() => circuit.Setups.Add(new SetupViewModel
                                                           {
                                                             Name = Path.GetFileName(setupFileInfo
                                                               .VersionSetupFilePath),
                                                             FilePath = setupFileInfo.VersionSetupFilePath
                                                           }));
    }

    public IEnumerable<VehicleViewModel> BuildMasterHierarchy()
    {
      return this.BuildHierarchy(PathProvider.MasterSetupsFolderPath);
    }

    public IEnumerable<VehicleViewModel> BuildVersionsHierarchy()
    {
      return this.BuildHierarchy(PathProvider.VersionsFolderPath);
    }

    public CircuitViewModel GetCircuitFromHierarchy(SetupFileInfo setupFileInfo,
      ICollection<VehicleViewModel> hierarchy)
    {
      var vehicle = hierarchy.FirstOrDefault(v => v.FolderName == setupFileInfo.VehicleIdentifier);
      if(vehicle == null)
      {
        vehicle = new VehicleViewModel
                  {
                    FolderName = setupFileInfo.VehicleIdentifier,
                    Name = this.folderNameMapper.GetFriendlyVehicleName(setupFileInfo.VehicleIdentifier)
                  };
        this.dispatcher.InvokeAsync(() => hierarchy.Add(vehicle));
      }

      var circuit = vehicle.Circuits.FirstOrDefault(c => c.FolderName == setupFileInfo.CircuitIdentifier);
      if(circuit == null)
      {
        circuit = new CircuitViewModel
                  {
                    FolderName = setupFileInfo.CircuitIdentifier,
                    Name = this.folderNameMapper.GetFriendlyCircuitName(setupFileInfo.CircuitIdentifier)
                  };

        this.dispatcher.InvokeAsync(() => vehicle.Circuits.Insert(0, circuit));
      }

      return circuit;
    }

    private IEnumerable<VehicleViewModel> BuildHierarchy(string rootFolder)
    {
      var result = new List<VehicleViewModel>();

      var masterVehicleFolderPaths = Directory.GetDirectories(rootFolder);
      foreach(var masterVehicleFolderPath in masterVehicleFolderPaths)
      {
        var vehicleIdentifier = PathProvider.GetLastFolderName(masterVehicleFolderPath);
        var vehicle = new VehicleViewModel
                      {
                        FolderName = vehicleIdentifier,
                        Name = this.folderNameMapper.GetFriendlyVehicleName(vehicleIdentifier)
                      };

        var circuitFolderPaths = Directory.GetDirectories(masterVehicleFolderPath);
        foreach(var circuitFolderPath in circuitFolderPaths)
        {
          var circuitIdentifier = PathProvider.GetLastFolderName(circuitFolderPath);
          var circuit = new CircuitViewModel
                        {
                          FolderName = circuitIdentifier,
                          Name = this.folderNameMapper.GetFriendlyCircuitName(circuitIdentifier)
                        };

          var setupFilePaths = Directory.GetFiles(circuitFolderPath);
          foreach(var setupFilePath in setupFilePaths)
          {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(setupFilePath);
            var setup = new SetupViewModel
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