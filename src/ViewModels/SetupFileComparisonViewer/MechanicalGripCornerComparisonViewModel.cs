using System;
using ACCSetupManager.Enums;
using ACCSetupManager.Models;
using ACCSetupManager.ViewModels.SetupFileViewer;

namespace ACCSetupManager.ViewModels.SetupFileComparisonViewer
{
  public class MechanicalGripCornerComparisonViewModel : VehicleCornerViewModelBase
  {
    private double bumpStopRange;

    private bool bumpStopRangeDiffers;
    private double bumpStopRateDown;
    private bool bumpStopRateDownDiffers;
    private double bumpStopRateUp;
    private bool bumpStopRateUpDiffers;
    private double wheelRate;
    private bool wheelRateDiffers;

    public double BumpStopRange
    {
      get => this.bumpStopRange;
      set => this.SetProperty(ref this.bumpStopRange, value);
    }

    public bool BumpStopRangeDiffers
    {
      get => this.bumpStopRangeDiffers;
      set => this.SetProperty(ref this.bumpStopRangeDiffers, value);
    }

    public double BumpStopRateDown
    {
      get => this.bumpStopRateDown;
      set => this.SetProperty(ref this.bumpStopRateDown, value);
    }

    public bool BumpStopRateDownDiffers
    {
      get => this.bumpStopRateDownDiffers;
      set => this.SetProperty(ref this.bumpStopRateDownDiffers, value);
    }

    public double BumpStopRateUp
    {
      get => this.bumpStopRateUp;
      set => this.SetProperty(ref this.bumpStopRateUp, value);
    }

    public bool BumpStopRateUpDiffers
    {
      get => this.bumpStopRateUpDiffers;
      set => this.SetProperty(ref this.bumpStopRateUpDiffers, value);
    }

    public double WheelRate
    {
      get => this.wheelRate;
      set => this.SetProperty(ref this.wheelRate, value);
    }

    public bool WheelRateDiffers
    {
      get => this.wheelRateDiffers;
      set => this.SetProperty(ref this.wheelRateDiffers, value);
    }

    internal void Apply(SetupFile setupFile,
      SetupSpec setupSpec,
      Location location,
      MechanicalBalanceViewModel comparisonMechanicalBalanceViewModel)
    {
      this.SetTitle(location);
      var mechanicalBalance = setupFile.AdvancedSetup.MechanicalBalance;
      var index = this.GetIndexFromLocation(location);
      var comparisonGroup =
        this.GetGroupFromLocation(location, comparisonMechanicalBalanceViewModel);
      this.BumpStopRange = mechanicalBalance.BumpStopWindow[index];
      this.BumpStopRangeDiffers = !this.BumpStopRange.IsEqualTo(comparisonGroup.BumpStopRange);
      this.BumpStopRateDown = setupSpec.ToBumpStopRate(mechanicalBalance.BumpStopRateDn[index]);
      this.BumpStopRateDownDiffers =
        !this.BumpStopRateDown.IsEqualTo(comparisonGroup.BumpStopRateDown);
      this.BumpStopRateUp = setupSpec.ToBumpStopRate(mechanicalBalance.BumpStopRateUp[index]);
      this.BumpStopRateUpDiffers = !this.BumpStopRateUp.IsEqualTo(comparisonGroup.BumpStopRateUp);
      this.WheelRate = setupSpec.ToWheelRate(mechanicalBalance.WheelRate[index], location);
      this.WheelRateDiffers = !this.WheelRate.IsEqualTo(comparisonGroup.WheelRate);
    }

    private MechanicalGripCornerViewModel GetGroupFromLocation(Location location,
      MechanicalBalanceViewModel mechanicalBalanceViewModel)
    {
      switch(location)
      {
        case Location.LeftFront:
          return mechanicalBalanceViewModel.LeftFrontMechanicalGroup;
        case Location.LeftRear:
          return mechanicalBalanceViewModel.LeftRearMechanicalGroup;
        case Location.RightFront:
          return mechanicalBalanceViewModel.RightFrontMechanicalGroup;
        default:
          return mechanicalBalanceViewModel.RightRearMechanicalGroup;
      }
    }

    internal bool HasDifferences()
    {
      return this.BumpStopRangeDiffers || this.BumpStopRateDownDiffers || this.BumpStopRateUpDiffers
             || this.WheelRateDiffers;
    }
  }
}