using System;
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public class FuelAndStrategyViewModel : ObservableObject
  {
    private int frontBrakeCompound;
    private int fuel;
    private double fuelPerLap;
    private int pitStops;
    private int rearBrakeCompound;
    private int tyreCompound;
    private int tyreSet;

    public ObservableCollection<PitStrategyViewModel> PitStrategies { get; } = new();

    public ObservableCollection<TyreCompoundListItem> TyreCompounds { get; } = new()
      {
        new TyreCompoundListItem
        {
          Text = "Dry",
          Value = 1
        },
        new TyreCompoundListItem
        {
          Text = "Wet",
          Value = 2
        }
      };

    public int FrontBrakeCompound
    {
      get => this.frontBrakeCompound;
      set => this.SetProperty(ref this.frontBrakeCompound, value);
    }

    public int Fuel
    {
      get => this.fuel;
      set => this.SetProperty(ref this.fuel, value);
    }

    public double FuelPerLap
    {
      get => this.fuelPerLap;
      set => this.SetProperty(ref this.fuelPerLap, value);
    }

    public int PitStops
    {
      get => this.pitStops;
      set => this.SetProperty(ref this.pitStops, value);
    }

    public int RearBrakeCompound
    {
      get => this.rearBrakeCompound;
      set => this.SetProperty(ref this.rearBrakeCompound, value);
    }

    public int TyreCompound
    {
      get => this.tyreCompound;
      set => this.SetProperty(ref this.tyreCompound, value);
    }

    public int TyreSet
    {
      get => this.tyreSet;
      set => this.SetProperty(ref this.tyreSet, value);
    }
  }
}