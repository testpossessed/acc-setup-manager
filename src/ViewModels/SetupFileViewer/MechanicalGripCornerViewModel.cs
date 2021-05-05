using System;
using ACCSetupManager.Enums;
using ACCSetupManager.Models;

namespace ACCSetupManager.ViewModels.SetupFileViewer
{
  public class MechanicalGripCornerViewModel : VehicleCornerViewModelBase
  {
    private double bumpStopRange;
    private double bumpStopRateDown;
    private double bumpStopRateUp;
    private double wheelRate;

    public double BumpStopRange
    {
      get => this.bumpStopRange;
      set => this.SetProperty(ref this.bumpStopRange, value);
    }

    public double BumpStopRateDown
    {
      get => this.bumpStopRateDown;
      set => this.SetProperty(ref this.bumpStopRateDown, value);
    }

    public double BumpStopRateUp
    {
      get => this.bumpStopRateUp;
      set => this.SetProperty(ref this.bumpStopRateUp, value);
    }

    public double WheelRate
    {
      get => this.wheelRate;
      set => this.SetProperty(ref this.wheelRate, value);
    }

    internal void Apply(SetupFile setupFile, SetupSpec setupSpec, Location location)
    {
      this.SetTitle(location);
      var mechanicalBalance = setupFile.AdvancedSetup.MechanicalBalance;
      var index = this.GetIndexFromLocation(location);

      this.BumpStopRange = mechanicalBalance.BumpStopWindow[index];
      this.BumpStopRateDown = setupSpec.ToBumpStopRate(mechanicalBalance.BumpStopRateDn[index]);
      this.BumpStopRateUp = setupSpec.ToBumpStopRate(mechanicalBalance.BumpStopRateUp[index]);
      this.WheelRate = setupSpec.ToWheelRate(mechanicalBalance.WheelRate[index], location);
    }
  }
}