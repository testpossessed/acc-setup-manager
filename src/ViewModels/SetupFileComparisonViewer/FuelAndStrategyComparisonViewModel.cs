using System;
using System.Collections.ObjectModel;
using System.Linq;
using ACCSetupManager.Models;
using ACCSetupManager.ViewModels.SetupFileViewer;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels.SetupFileComparisonViewer
{
  public class FuelAndStrategyComparisonViewModel : ObservableObject
  {
    private int frontBrakeCompound;

    private bool frontBrakeCompoundDiffers;
    private int fuel;
    private bool fuelDiffers;
    private double fuelPerLap;
    private bool fuelPerLapDiffers;
    private int pitStops;
    private bool pitStopsDiffers;
    private bool rearBakeCompoundDiffers;
    private int rearBrakeCompound;
    private string tyreCompound;
    private bool tyreCompoundDiffers;
    private int tyreSet;
    private bool tyreSetDiffers;
    public ObservableCollection<PitStrategyComparisonViewModel> PitStrategies { get; } = new();

    public int FrontBrakeCompound
    {
      get => this.frontBrakeCompound;
      set => this.SetProperty(ref this.frontBrakeCompound, value);
    }

    public bool FrontBrakeCompoundDiffers
    {
      get => this.frontBrakeCompoundDiffers;
      set => this.SetProperty(ref this.frontBrakeCompoundDiffers, value);
    }

    public int Fuel
    {
      get => this.fuel;
      set => this.SetProperty(ref this.fuel, value);
    }

    public bool FuelDiffers
    {
      get => this.fuelDiffers;
      set => this.SetProperty(ref this.fuelDiffers, value);
    }

    public double FuelPerLap
    {
      get => this.fuelPerLap;
      set => this.SetProperty(ref this.fuelPerLap, value);
    }

    public bool FuelPerLapDiffers
    {
      get => this.fuelPerLapDiffers;
      set => this.SetProperty(ref this.fuelPerLapDiffers, value);
    }

    public int PitStops
    {
      get => this.pitStops;
      set => this.SetProperty(ref this.pitStops, value);
    }

    public bool PitStopsDiffers
    {
      get => this.pitStopsDiffers;
      set => this.SetProperty(ref this.pitStopsDiffers, value);
    }

    public bool RearBakeCompoundDiffers
    {
      get => this.rearBakeCompoundDiffers;
      set => this.SetProperty(ref this.rearBakeCompoundDiffers, value);
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

    public bool TyreCompoundDiffers
    {
      get => this.tyreCompoundDiffers;
      set => this.SetProperty(ref this.tyreCompoundDiffers, value);
    }
    public int TyreSet
    {
      get => this.tyreSet;
      set => this.SetProperty(ref this.tyreSet, value);
    }

    public bool TyreSetDiffers
    {
      get => this.tyreSetDiffers;
      set => this.SetProperty(ref this.tyreSetDiffers, value);
    }

    public void Apply(SetupFile setupFile,
      SetupSpec setupSpec,
      SetupFileViewModel compareToSetupFileViewModel)
    {
      var strategy = setupFile.BasicSetup.Strategy;
      var comparisonStrategy = compareToSetupFileViewModel.FuelAndStrategy;
      var tyreCompoundValue = setupFile.BasicSetup.Tyres.TyreCompound;
      this.TyreCompound = this.GetTyreCompoundName(tyreCompoundValue);
      this.TyreCompoundDiffers = tyreCompoundValue != comparisonStrategy.TyreCompound;
      this.Fuel = strategy.Fuel;
      this.FuelDiffers = this.Fuel != comparisonStrategy.Fuel;
      this.PitStops = strategy.NPitStops;
      this.PitStopsDiffers = this.PitStops != comparisonStrategy.PitStops;
      this.FrontBrakeCompound = setupSpec.ToBrakeCompound(strategy.FrontBrakePadCompound);
      this.FrontBrakeCompoundDiffers =
        this.FrontBrakeCompound != setupSpec.ToBrakeCompound(comparisonStrategy.FrontBrakeCompound);
      this.RearBrakeCompound = strategy.RearBrakePadCompound;
      this.RearBakeCompoundDiffers = this.RearBrakeCompound != comparisonStrategy.RearBrakeCompound;
      this.FuelPerLap = Math.Round(strategy.FuelPerLap, 1);
      this.FuelPerLapDiffers = !this.FuelPerLap.IsEqualTo(comparisonStrategy.FuelPerLap);
      this.TyreSet = setupSpec.ToTyreSet(setupFile.BasicSetup.Strategy.TyreSet);
      this.TyreSetDiffers = this.TyreSet != comparisonStrategy.TyreSet;

      this.PitStrategies.Clear();
      for(var i = 0; i < strategy.PitStrategies.Length; i++)
      {
        var pitStrategy = strategy.PitStrategies[i];
        this.PitStrategies.Add(this.MapPitStrategy(pitStrategy,
          i,
          setupSpec,
          comparisonStrategy.PitStrategies[i]));
      }
    }

    internal bool HasDifferences()
    {
      return this.FrontBrakeCompoundDiffers || this.FuelDiffers || this.FuelPerLapDiffers
             || this.PitStopsDiffers || this.RearBakeCompoundDiffers || this.TyreCompoundDiffers
             || this.TyreSetDiffers || this.PitStrategies.Any(s => s.HasDifferences());
    }

    private string GetTyreCompoundName(int tyreCompoundValue)
    {
      return tyreCompoundValue == 0? "Dry": "Wet";
    }

    private PitStrategyComparisonViewModel MapPitStrategy(PitStrategy pitStrategy,
      int index,
      SetupSpec setupSpec,
      PitStrategyViewModel comparisonPitStrategy)
    {
      var tyreCompoundValue = pitStrategy.Tyres.TyreCompound;
      var result = new PitStrategyComparisonViewModel
                   {
                     PitStopNumber = index + 1,
                     FrontBrakeCompound = setupSpec.ToBrakeCompound(pitStrategy.FrontBrakePadCompound),
                     FrontBrakeCompoundDiffers =
                       pitStrategy.FrontBrakePadCompound
                       != comparisonPitStrategy.FrontBrakeCompound,
                     FuelToAdd = pitStrategy.FuelToAdd,
                     FuelToAddDiffers = pitStrategy.FuelToAdd != comparisonPitStrategy.FuelToAdd,
                     RearBrakeCompound = setupSpec.ToBrakeCompound(pitStrategy.RearBreakPadCompound),
                     RearBrakeCompoundDiffers =
                       pitStrategy.RearBreakPadCompound != comparisonPitStrategy.RearBrakeCompound,
                     TyreSet = setupSpec.ToTyreSet(pitStrategy.TyreSet),
                     TyreSetDiffers = pitStrategy.TyreSet != comparisonPitStrategy.TyreSet,
                     TyreCompound = this.GetTyreCompoundName(tyreCompoundValue),
                     TyreCompoundDiffers = tyreCompoundValue != comparisonPitStrategy.TyreCompound,
                     LeftFrontPsi = setupSpec.ToPressurePsi(pitStrategy.Tyres.TyrePressure[0]),
                     RightFrontPsi = setupSpec.ToPressurePsi(pitStrategy.Tyres.TyrePressure[1]),
                     LeftRearPsi = setupSpec.ToPressurePsi(pitStrategy.Tyres.TyrePressure[2]),
                     RightRearPsi = setupSpec.ToPressurePsi(pitStrategy.Tyres.TyrePressure[03])
                   };

      result.LeftFrontPsiDiffers =
        !result.LeftFrontPsi.IsEqualTo(comparisonPitStrategy.LeftFrontPsi);
      result.LeftRearPsiDiffers = !result.LeftRearPsi.IsEqualTo(comparisonPitStrategy.LeftRearPsi);
      result.RightFrontPsiDiffers =
        !result.RightFrontPsi.IsEqualTo(comparisonPitStrategy.RightFrontPsi);
      result.RightRearPsiDiffers =
        !result.RightRearPsi.IsEqualTo(comparisonPitStrategy.RightRearPsi);

      return result;
    }
  }
}