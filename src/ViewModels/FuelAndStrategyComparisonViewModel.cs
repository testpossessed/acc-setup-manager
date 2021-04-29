using System;
using System.Collections.ObjectModel;
using ACCSetupManager.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public class FuelAndStrategyComparisonViewModel : ObservableObject
  {
    private int frontBrakeCompound;
    private int fuel;
    private double fuelPerLap;
    private int pitStops;
    private int rearBrakeCompound;
    private int tyreCompound;
    private int tyreSet;

    public ObservableCollection<PitStrategyComparisonViewModel> PitStrategies { get; } = new();

    public ObservableCollection<TyreCompoundListItem> TyreCompounds { get; } = new()
      {
        new TyreCompoundListItem
        {
          Text = "Dry",
          Value = 0
        },
        new TyreCompoundListItem
        {
          Text = "Wet",
          Value = 1
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

    public void Apply(SetupFile setupFile,
      SetupSpec setupSpec,
      SetupFileViewModel compareToSetupFileViewModel)
    {
      var strategy = setupFile.BasicSetup.Strategy;
      this.TyreCompound = setupFile.BasicSetup.Tyres.TyreCompound;
      this.Fuel = strategy.Fuel;
      this.PitStops = strategy.NPitStops;
      this.FrontBrakeCompound = strategy.FrontBrakePadCompound;
      this.RearBrakeCompound = strategy.RearBreakPadCompound;
      this.FuelPerLap = Math.Round(strategy.FuelPerLap, 1);

      this.PitStrategies.Clear();
      for(var i = 0; i < strategy.PitStrategies.Length; i++)
      {
        var pitStrategy = strategy.PitStrategies[i];
        this.PitStrategies.Add(this.MapPitStrategy(pitStrategy, i, setupSpec));
      }
    }

    private PitStrategyComparisonViewModel MapPitStrategy(PitStrategy pitStrategy,
      int index,
      SetupSpec setupSpec)
    {
      return new()
             {
               PitStopNumber = index + 1,
               FrontBrakeCompound = pitStrategy.FrontBrakePadCompound,
               FuelToAdd = pitStrategy.FuelToAdd,
               RearBrakeCompound = pitStrategy.RearBreakPadCompound,
               TyreSet = pitStrategy.TyreSet,
               TyreCompound = pitStrategy.Tyres.TyreCompound,
               LeftFrontPsi = setupSpec.ToPressurePsi(pitStrategy.Tyres.TyrePressure[0]),
               RightFrontPsi = setupSpec.ToPressurePsi(pitStrategy.Tyres.TyrePressure[1]),
               LeftRearPsi = setupSpec.ToPressurePsi(pitStrategy.Tyres.TyrePressure[2]),
               RightRearPsi = setupSpec.ToPressurePsi(pitStrategy.Tyres.TyrePressure[03])
             };
    }
  }
}