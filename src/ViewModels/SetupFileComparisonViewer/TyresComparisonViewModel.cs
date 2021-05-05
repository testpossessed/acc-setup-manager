using System;
using ACCSetupManager.Enums;
using ACCSetupManager.Models;
using ACCSetupManager.ViewModels.SetupFileViewer;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels.SetupFileComparisonViewer
{
  public class TyresComparisonViewModel : ObservableObject
  {
    public FrontTyreComparisonViewModel LeftFront { get; } = new();
    public TyreComparisonViewModel LeftRear { get; } = new();
    public FrontTyreComparisonViewModel RightFront { get; } = new();
    public TyreComparisonViewModel RightRear { get; } = new();

    public void Apply(SetupFile setupFile,
      SetupSpec setupSpec,
      SetupFileViewModel compareToSetupFileViewModel)
    {
      var tyresViewModel = compareToSetupFileViewModel.Tyres;
      this.LeftFront.Apply(setupFile, setupSpec, Location.LeftFront, tyresViewModel.LeftFront);
      this.RightFront.Apply(setupFile, setupSpec, Location.RightFront, tyresViewModel.RightFront);
      this.LeftRear.Apply(setupFile, setupSpec, Location.LeftRear, tyresViewModel.LeftRear);
      this.RightRear.Apply(setupFile, setupSpec, Location.RightRear, tyresViewModel.RightRear);
    }

    public bool HasDifferences()
    {
      return this.LeftFront.HasDifferences() || this.RightFront.HasDifferences() || this.LeftRear.HasDifferences() || this.RightRear.HasDifferences();
    }
  }
}