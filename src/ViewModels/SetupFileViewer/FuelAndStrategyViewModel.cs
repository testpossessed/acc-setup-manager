using System;
using System.Collections.ObjectModel;
using ACCSetupManager.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels.SetupFileViewer
{
  public class FuelAndStrategyViewModel : ObservableObject
  {
    private int frontBrakeCompound;
    private int fuel;
    private double fuelPerLap;
    private int pitStops;
    private int rearBrakeCompound;
    private string tyreCompound;
    private int tyreSet;

    public ObservableCollection<PitStrategyViewModel> PitStrategies { get; } = new();

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

    public string TyreCompound
    {
      get => this.tyreCompound;
      set => this.SetProperty(ref this.tyreCompound, value);
    }

    public int TyreSet
    {
      get => this.tyreSet;
      set => this.SetProperty(ref this.tyreSet, value);
    }

    public void Apply(SetupFile setupFile, SetupSpec setupSpec)
    {
      var strategy = setupFile.BasicSetup.Strategy;
      this.TyreCompound = this.ToTyreCompoundName(setupFile.BasicSetup.Tyres.TyreCompound);
      this.Fuel = setupSpec.ToFuel(strategy.Fuel);
      this.PitStops = strategy.NPitStops;
      this.FrontBrakeCompound = setupSpec.ToBrakeCompound(strategy.FrontBrakePadCompound);
      this.RearBrakeCompound = setupSpec.ToBrakeCompound(strategy.RearBrakePadCompound);
      this.FuelPerLap = Math.Round(strategy.FuelPerLap, 1);
      this.TyreSet = setupSpec.ToTyreSet(strategy.TyreSet);

      this.PitStrategies.Clear();
      for(var i = 0; i < strategy.PitStrategies.Length; i++)
      {
        var pitStrategy = strategy.PitStrategies[i];
        this.PitStrategies.Add(this.MapPitStrategy(pitStrategy, i, setupSpec));
      }
    }

    private PitStrategyViewModel MapPitStrategy(PitStrategy pitStrategy,
      int index,
      SetupSpec setupSpec)
    {
      return new()
             {
               PitStopNumber = index + 1,
               FrontBrakeCompound = setupSpec.ToBrakeCompound(pitStrategy.FrontBrakePadCompound),
               FuelToAdd = pitStrategy.FuelToAdd,
               RearBrakeCompound = setupSpec.ToBrakeCompound(pitStrategy.RearBreakPadCompound),
               TyreSet = setupSpec.ToTyreSet(pitStrategy.TyreSet),
               TyreCompound = this.ToTyreCompoundName(pitStrategy.Tyres.TyreCompound),
               LeftFrontPsi = setupSpec.ToPressurePsi(pitStrategy.Tyres.TyrePressure[0]),
               RightFrontPsi = setupSpec.ToPressurePsi(pitStrategy.Tyres.TyrePressure[1]),
               LeftRearPsi = setupSpec.ToPressurePsi(pitStrategy.Tyres.TyrePressure[2]),
               RightRearPsi = setupSpec.ToPressurePsi(pitStrategy.Tyres.TyrePressure[03])
             };
    }

    private string ToTyreCompoundName(int tyreCompound)
    {
      return tyreCompound == 0? "Dry": "Wet";
    }
  }
}