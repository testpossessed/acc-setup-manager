using System;
using ACCSetupManager.Enums;
using ACCSetupManager.Models;

namespace ACCSetupManager.ViewModels.SetupFileViewer
{
  public class DamperCornerViewModel : VehicleCornerViewModelBase
  {
    private int bump;
    private int fastBump;
    private int fastRebound;
    private int rebound;

    public int Bump
    {
      get => this.bump;
      set => this.SetProperty(ref this.bump, value);
    }

    public int FastBump
    {
      get => this.fastBump;
      set => this.SetProperty(ref this.fastBump, value);
    }

    public int FastRebound
    {
      get => this.fastRebound;
      set => this.SetProperty(ref this.fastRebound, value);
    }

    public int Rebound
    {
      get => this.rebound;
      set => this.SetProperty(ref this.rebound, value);
    }

    internal void Apply(SetupFile setupFile, Location location)
    {
      this.SetTitle(location);

      var index = this.GetIndexFromLocation(location);
      var dampers = setupFile.AdvancedSetup.Dampers;

      this.Bump = dampers.BumpSlow[index];
      this.FastBump = dampers.BumpFast[index];
      this.Rebound = dampers.ReboundSlow[index];
      this.FastRebound = dampers.ReboundFast[index];
    }
  }
}