using System;
using System.Collections.Generic;
using ACCSetupManager.Models;
using ACCSetupManager.ViewModels;

namespace ACCSetupManager.Abstractions
{
  public interface IHierarchyProvider
  {
    IEnumerable<VehicleViewModel> BuildMasterHierarchy();
    IEnumerable<VehicleViewModel> BuildVersionsHierarchy();
    CircuitViewModel GetCircuitFromHierarchy(SetupFileInfo setupFileInfo, ICollection<VehicleViewModel> hierarchy);
    void AddVersionedSetupFileToHierarchy(SetupFileInfo setupFileInfo, ICollection<VehicleViewModel> hierarchy);

    void AddMasterSetupFileToHierarchy(SetupFileInfo setupFileInfo,
      ICollection<VehicleViewModel> hierarchy);
  }
}