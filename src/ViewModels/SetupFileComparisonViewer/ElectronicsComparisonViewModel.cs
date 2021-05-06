using System;
using ACCSetupManager.Models;
using ACCSetupManager.ViewModels.SetupFileViewer;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels.SetupFileComparisonViewer
{
  public class ElectronicsComparisonViewModel : ObservableObject
  {
    private int abs;
    private bool absDiffers;
    private bool ecuDiffers;
    private int ecuMap;
    private int tc1;
    private bool tc1Differs;
    private int tc2;
    private bool tc2Differs;
    private int telemetryLaps;
    private bool telemetryLapsDiffers;

    public int Abs
    {
      get => this.abs;
      set => this.SetProperty(ref this.abs, value);
    }

    public bool AbsDiffers
    {
      get => this.absDiffers;
      set => this.SetProperty(ref this.absDiffers, value);
    }

    public bool EcuDiffers
    {
      get => this.ecuDiffers;
      set => this.SetProperty(ref this.ecuDiffers, value);
    }

    public int EcuMap
    {
      get => this.ecuMap;
      set => this.SetProperty(ref this.ecuMap, value);
    }

    public int Tc1
    {
      get => this.tc1;
      set => this.SetProperty(ref this.tc1, value);
    }

    public bool Tc1Differs
    {
      get => this.tc1Differs;
      set => this.SetProperty(ref this.tc1Differs, value);
    }

    public int Tc2
    {
      get => this.tc2;
      set => this.SetProperty(ref this.tc2, value);
    }

    public bool Tc2Differs
    {
      get => this.tc2Differs;
      set => this.SetProperty(ref this.tc2Differs, value);
    }

    public int TelemetryLaps
    {
      get => this.telemetryLaps;
      set => this.SetProperty(ref this.telemetryLaps, value);
    }

    public bool TelemetryLapsDiffers
    {
      get => this.telemetryLapsDiffers;
      set => this.SetProperty(ref this.telemetryLapsDiffers, value);
    }

    internal void Apply(SetupFile setupFile,
      SetupSpec setupSpec,
      SetupFileViewModel compareToSetupFileViewModel)
    {
      var electronics = setupFile.BasicSetup.Electronics;
      var electronicsComparison = compareToSetupFileViewModel.Electronics;
      this.Abs = electronics.Abs;
      this.AbsDiffers = this.Abs != electronicsComparison.Abs;
      this.EcuMap = setupSpec.ToEcu(electronics.ECUMap);
      this.EcuDiffers = this.EcuMap != electronicsComparison.EcuMap;
      this.Tc1 = electronics.TC1;
      this.Tc1Differs = this.Tc1 != electronicsComparison.Tc1;
      this.Tc2 = electronics.TC2;
      this.Tc2Differs = this.Tc2 != electronicsComparison.Tc2;
      this.TelemetryLaps = electronics.TelemetryLaps;
      this.TelemetryLapsDiffers = this.TelemetryLaps != electronicsComparison.TelemetryLaps;
    }

    internal bool HasDifferences()
    {
      return this.AbsDiffers || this.EcuDiffers || this.Tc1Differs
             || this.Tc2Differs || this.TelemetryLapsDiffers;
    }
  }
}