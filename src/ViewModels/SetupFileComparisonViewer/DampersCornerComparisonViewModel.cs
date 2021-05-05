using System;
using ACCSetupManager.Enums;
using ACCSetupManager.Models;
using ACCSetupManager.ViewModels.SetupFileViewer;

namespace ACCSetupManager.ViewModels.SetupFileComparisonViewer
{
  public class DampersCornerComparisonViewModel : VehicleCornerViewModelBase
  {
    private int bump;
    private bool bumpDiffers;
    private int fastBump;
    private bool fastBumpDiffers;
    private int fastRebound;
    private bool fastReboundDiffers;
    private int rebound;
    private bool reboundDiffers;

    public int Bump
    {
      get => this.bump;
      set => this.SetProperty(ref this.bump, value);
    }

    public bool BumpDiffers
    {
      get => this.bumpDiffers;
      set => this.SetProperty(ref this.bumpDiffers, value);
    }

    public int FastBump
    {
      get => this.fastBump;
      set => this.SetProperty(ref this.fastBump, value);
    }

    public bool FastBumpDiffers
    {
      get => this.fastBumpDiffers;
      set => this.SetProperty(ref this.fastBumpDiffers, value);
    }

    public int FastRebound
    {
      get => this.fastRebound;
      set => this.SetProperty(ref this.fastRebound, value);
    }

    public bool FastReboundDiffers
    {
      get => this.fastReboundDiffers;
      set => this.SetProperty(ref this.fastReboundDiffers, value);
    }

    public int Rebound
    {
      get => this.rebound;
      set => this.SetProperty(ref this.rebound, value);
    }

    public bool ReboundDiffers
    {
      get => this.reboundDiffers;
      set => this.SetProperty(ref this.reboundDiffers, value);
    }

    internal void Apply(SetupFile setupFile,
      Location location,
      DamperCornerViewModel comparisonCornerViewModel)
    {
      this.SetTitle(location);

      var index = this.GetIndexFromLocation(location);
      var dampers = setupFile.AdvancedSetup.Dampers;

      this.Bump = dampers.BumpSlow[index];
      this.BumpDiffers = this.Bump != comparisonCornerViewModel.Bump;
      this.FastBump = dampers.BumpFast[index];
      this.FastBumpDiffers = this.FastBump != comparisonCornerViewModel.FastBump;
      this.Rebound = dampers.ReboundSlow[index];
      this.ReboundDiffers = this.Rebound != comparisonCornerViewModel.Rebound;
      this.FastRebound = dampers.ReboundFast[index];
      this.FastReboundDiffers = this.FastRebound != comparisonCornerViewModel.FastRebound;
    }

    internal bool HasDifferences()
    {
      return this.BumpDiffers || this.FastBumpDiffers || this.ReboundDiffers
             || this.FastReboundDiffers;
    }
  }
}