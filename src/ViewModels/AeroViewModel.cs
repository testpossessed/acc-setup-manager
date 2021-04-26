using System;
using ACCSetupManager.Enums;
using ACCSetupManager.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public class AeroViewModel : ObservableObject
  {
    private int frontBrakeDucts;
    private int frontRideHeight;
    private int rearBrakeDucts;
    private int rearRideHeight;
    private int rearWing;
    private int splitter;

    public int FrontBrakeDucts
    {
      get => this.frontBrakeDucts;
      set => this.SetProperty(ref this.frontBrakeDucts, value);
    }

    public int FrontRideHeight
    {
      get => this.frontRideHeight;
      set => this.SetProperty(ref this.frontRideHeight, value);
    }

    public int RearBrakeDucts
    {
      get => this.rearBrakeDucts;
      set => this.SetProperty(ref this.rearBrakeDucts, value);
    }

    public int RearRideHeight
    {
      get => this.rearRideHeight;
      set => this.SetProperty(ref this.rearRideHeight, value);
    }

    public int RearWing
    {
      get => this.rearWing;
      set => this.SetProperty(ref this.rearWing, value);
    }

    public int Splitter
    {
      get => this.splitter;
      set => this.SetProperty(ref this.splitter, value);
    }

    internal void Apply(SetupFile setupFile, SetupSpec setupSpec)
    {
      var aeroBalance = setupFile.AdvancedSetup.AeroBalance;
      this.FrontBrakeDucts = aeroBalance.BrakeDuct[0];
      this.RearBrakeDucts = aeroBalance.BrakeDuct[1];
      this.FrontRideHeight = setupSpec.ToRideHeight(aeroBalance.RideHeight[0], Location.Front);
      this.RearRideHeight = setupSpec.ToRideHeight(aeroBalance.RideHeight[2], Location.Rear);
      this.RearWing = setupSpec.ToRearWing(aeroBalance.RearWing);
      this.Splitter = setupSpec.ToSplitter(aeroBalance.Splitter);
    }
  }
}