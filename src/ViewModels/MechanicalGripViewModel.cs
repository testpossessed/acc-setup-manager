using System;
using ACCSetupManager.Enums;
using ACCSetupManager.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public class MechanicalGripViewModel : ObservableObject
  {
    private double bumpStopRange;
    private double bumpStopRateDown;
    private double bumpStopRateUp;
    private string title;
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

    public string Title
    {
      get => this.title;
      set => this.SetProperty(ref this.title, value);
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

    private void SetTitle(Location location)
    {
      switch(location)
      {
        case Location.RightFront:
          this.Title = "Right Front";
          break;
        case Location.LeftRear:
          this.Title = "Left Rear";
          break;
        case Location.RightRear:
          this.Title = "Right Rear";
          break;
        default:
          this.Title = "Left Front";
          break;
      }
    }

    private int GetIndexFromLocation(Location location)
    {
      switch(location)
      {
        case Location.RightFront:
          return 1;
        case Location.LeftRear:
          return 2;
        case Location.RightRear:
          return 3;
        default:
          return 0;
      }
    }
  }
}