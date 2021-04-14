using System;
using System.Collections.Generic;
using ACCSetupManager.Models;

namespace ACCSetupManager.Abstractions
{
  public interface IHierarchyBuilder
  {
    IEnumerable<Vehicle> BuildMasterHierarchy();
    IEnumerable<Vehicle> BuildVersionsHierarchy();
  }
}