using System;
using ACCSetupManager.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public class ElectronicsViewModel : ObservableObject
  {
    private int abs;
    private int ecuMap;
    private int fuelMix;
    private int tc1;
    private int tc2;
    private int telemetryLaps;

    public int Abs
    {
      get => this.abs;
      set => this.SetProperty(ref this.abs, value);
    }

    public int EcuMap
    {
      get => this.ecuMap;
      set => this.SetProperty(ref this.ecuMap, value);
    }

    public int FuelMix
    {
      get => this.fuelMix;
      set => this.SetProperty(ref this.fuelMix, value);
    }

    public int Tc1
    {
      get => this.tc1;
      set => this.SetProperty(ref this.tc1, value);
    }

    public int Tc2
    {
      get => this.tc2;
      set => this.SetProperty(ref this.tc2, value);
    }

    public int TelemetryLaps
    {
      get => this.telemetryLaps;
      set => this.SetProperty(ref this.telemetryLaps, value);
    }

    public void Apply(SetupFile setupFile)
    {
      var electronics = setupFile.BasicSetup.Electronics;
      this.Abs = electronics.Abs;
      this.EcuMap = electronics.ECUMap;
      this.FuelMix = electronics.FuelMix;
      this.Tc1 = electronics.TC1;
      this.Tc2 = electronics.TC2;
      this.TelemetryLaps = electronics.TelemetryLaps;
    }
  }
}