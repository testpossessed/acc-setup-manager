using System;
using ACCSetupManager.Enums;
using ACCSetupManager.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels.SetupFileViewer
{
  public class DampersViewModel : ObservableObject
  {
    public DamperCornerViewModel LeftFront { get; } = new();
    public DamperCornerViewModel LeftRear { get; } = new();
    public DamperCornerViewModel RightFront { get; } = new();
    public DamperCornerViewModel RightRear { get; } = new();

    internal void Apply(SetupFile setupFile)
    {
      this.LeftFront.Apply(setupFile, Location.LeftFront);
      this.RightFront.Apply(setupFile, Location.RightFront);
      this.LeftRear.Apply(setupFile, Location.LeftRear);
      this.RightRear.Apply(setupFile, Location.RightRear);
    }
  }
}