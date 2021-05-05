using System;
using ACCSetupManager.Enums;
using ACCSetupManager.Models;
using ACCSetupManager.ViewModels.SetupFileViewer;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels.SetupFileComparisonViewer
{
  public class AeroComparisonViewModel : ObservableObject
  {
    private int frontBrakeDucts;
    private bool frontBrakeDuctsDiffers;
    private int frontRideHeight;
    private bool frontRideHeightDiffers;
    private int rearBrakeDucts;
    private bool rearBrakeDuctsDiffers;
    private int rearRideHeight;
    private bool rearRideHeightDiffers;
    private int rearWing;
    private bool rearWingDiffers;
    private int splitter;
    private bool splitterDiffers;

    public int FrontBrakeDucts
    {
      get => this.frontBrakeDucts;
      set => this.SetProperty(ref this.frontBrakeDucts, value);
    }

    public bool FrontBrakeDuctsDiffers
    {
      get => this.frontBrakeDuctsDiffers;
      set => this.SetProperty(ref this.frontBrakeDuctsDiffers, value);
    }

    public int FrontRideHeight
    {
      get => this.frontRideHeight;
      set => this.SetProperty(ref this.frontRideHeight, value);
    }

    public bool FrontRideHeightDiffers
    {
      get => this.frontRideHeightDiffers;
      set => this.SetProperty(ref this.frontRideHeightDiffers, value);
    }

    public int RearBrakeDucts
    {
      get => this.rearBrakeDucts;
      set => this.SetProperty(ref this.rearBrakeDucts, value);
    }

    public bool RearBrakeDuctsDiffers
    {
      get => this.rearBrakeDuctsDiffers;
      set => this.SetProperty(ref this.rearBrakeDuctsDiffers, value);
    }

    public int RearRideHeight
    {
      get => this.rearRideHeight;
      set => this.SetProperty(ref this.rearRideHeight, value);
    }

    public bool RearRideHeightDiffers
    {
      get => this.rearRideHeightDiffers;
      set => this.SetProperty(ref this.rearRideHeightDiffers, value);
    }

    public int RearWing
    {
      get => this.rearWing;
      set => this.SetProperty(ref this.rearWing, value);
    }

    public bool RearWingDiffers
    {
      get => this.rearWingDiffers;
      set => this.SetProperty(ref this.rearWingDiffers, value);
    }

    public int Splitter
    {
      get => this.splitter;
      set => this.SetProperty(ref this.splitter, value);
    }

    public bool SplitterDiffers
    {
      get => this.splitterDiffers;
      set => this.SetProperty(ref this.splitterDiffers, value);
    }

    internal void Apply(SetupFile setupFile,
      SetupSpec setupSpec,
      SetupFileViewModel comparisonViewModel)
    {
      var aeroBalance = setupFile.AdvancedSetup.AeroBalance;
      var comparisonAeroBalance = comparisonViewModel.Aero;
      this.FrontBrakeDucts = aeroBalance.BrakeDuct[0];
      this.FrontBrakeDuctsDiffers = this.FrontBrakeDucts != comparisonAeroBalance.FrontBrakeDucts;
      this.RearBrakeDucts = aeroBalance.BrakeDuct[1];
      this.RearBrakeDuctsDiffers = this.RearBrakeDucts != comparisonAeroBalance.RearBrakeDucts;
      this.FrontRideHeight = setupSpec.ToRideHeight(aeroBalance.RideHeight[0], Location.Front);
      this.FrontRideHeightDiffers = this.FrontRideHeight != comparisonAeroBalance.FrontRideHeight;
      this.RearRideHeight = setupSpec.ToRideHeight(aeroBalance.RideHeight[2], Location.Rear);
      this.RearRideHeightDiffers = this.RearRideHeight != comparisonAeroBalance.RearRideHeight;
      this.RearWing = setupSpec.ToRearWing(aeroBalance.RearWing);
      this.RearWingDiffers = this.RearWing != comparisonAeroBalance.RearWing;
      this.Splitter = setupSpec.ToSplitter(aeroBalance.Splitter);
      this.SplitterDiffers = this.Splitter != comparisonAeroBalance.Splitter;
    }

    internal bool HasDifferences()
    {
      return this.FrontBrakeDuctsDiffers || this.RearBrakeDuctsDiffers
                                         || this.FrontRideHeightDiffers
                                         || this.RearRideHeightDiffers || this.RearWingDiffers
                                         || this.SplitterDiffers;
    }
  }
}