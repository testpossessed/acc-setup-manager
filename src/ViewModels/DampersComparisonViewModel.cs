using System;
using ACCSetupManager.Enums;
using ACCSetupManager.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public class DampersComparisonViewModel : ObservableObject
  {
    public DamperCornerComparisonViewModel LeftFront { get; } = new();
    public DamperCornerComparisonViewModel LeftRear { get; } = new();
    public DamperCornerComparisonViewModel RightFront { get; } = new();
    public DamperCornerComparisonViewModel RightRear { get; } = new();

    internal void Apply(SetupFile setupFile, SetupFileViewModel compareToSetupFileViewModel)
    {
      this.LeftFront.Apply(setupFile, Location.LeftFront);
      this.RightFront.Apply(setupFile, Location.RightFront);
      this.LeftRear.Apply(setupFile, Location.LeftRear);
      this.RightRear.Apply(setupFile, Location.RightRear);
    }
  }
}