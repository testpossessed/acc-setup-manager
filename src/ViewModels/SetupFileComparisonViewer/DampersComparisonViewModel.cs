using System;
using ACCSetupManager.Enums;
using ACCSetupManager.Models;
using ACCSetupManager.ViewModels.SetupFileViewer;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels.SetupFileComparisonViewer
{
  public class DampersComparisonViewModel : ObservableObject
  {
    public DampersCornerComparisonViewModel LeftFront { get; } = new();
    public DampersCornerComparisonViewModel LeftRear { get; } = new();
    public DampersCornerComparisonViewModel RightFront { get; } = new();
    public DampersCornerComparisonViewModel RightRear { get; } = new();

    internal void Apply(SetupFile setupFile, SetupFileViewModel compareToSetupFileViewModel)
    {
      this.LeftFront.Apply(setupFile, Location.LeftFront, compareToSetupFileViewModel.Dampers.LeftFront);
      this.RightFront.Apply(setupFile, Location.RightFront, compareToSetupFileViewModel.Dampers.RightFront);
      this.LeftRear.Apply(setupFile, Location.LeftRear, compareToSetupFileViewModel.Dampers.LeftRear);
      this.RightRear.Apply(setupFile, Location.RightRear, compareToSetupFileViewModel.Dampers.RightRear);
    }

    internal bool HasDifferences()
    {
      return this.LeftFront.HasDifferences() || this.LeftRear.HasDifferences()
                                             || this.RightFront.HasDifferences()
                                             || this.RightRear.HasDifferences();
    }
  }
}