using System;
using ACCSetupManager.Enums;
using ACCSetupManager.Models;
using ACCSetupManager.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public class SetupFileViewModel : ObservableObject
  {
    private readonly SetupFile setupFile;
    private readonly SetupSpec setupSpec;

    public SetupFileViewModel(string setupFilePath)
    {
      this.setupFile = SetupFileProvider.GetSetupFile(setupFilePath);
      this.setupSpec = SetupSpecProvider.GetSetupSpec(this.setupFile.VehicleIdentifier);
      this.MapSetupFile();
    }

    public ElectronicsViewModel Electronics { get; } = new();
    public FuelAndStrategyViewModel FuelAndStrategy { get; } = new();
    public TyresViewModel Tyres { get; } = new();

    private void MapCamber()
    {
      var camber = this.setupFile.BasicSetup.Alignment.Camber;
      this.Tyres.LeftFront.Camber = this.setupSpec.ToCamber(camber[0], Location.Front);
      this.Tyres.RightFront.Camber = this.setupSpec.ToCamber(camber[1], Location.Front);
      this.Tyres.LeftRear.Camber = this.setupSpec.ToCamber(camber[2], Location.Rear);
      this.Tyres.RightRear.Camber = this.setupSpec.ToCamber(camber[3], Location.Rear);
    }

    private void MapCaster()
    {
      this.Tyres.LeftFront.Caster =
        this.setupSpec.ToCaster(this.setupFile.BasicSetup.Alignment.CasterLF);
      this.Tyres.RightFront.Caster =
        this.setupSpec.ToCaster(this.setupFile.BasicSetup.Alignment.CasterRF);
    }

    private void MapElectronics()
    {
      var electronics = this.setupFile.BasicSetup.Electronics;
      this.Electronics.Abs = electronics.Abs;
      this.Electronics.EcuMap = electronics.ECUMap;
      this.Electronics.FuelMix = electronics.FuelMix;
      this.Electronics.Tc1 = electronics.TC1;
      this.Electronics.Tc2 = electronics.TC2;
      this.Electronics.TelemetryLaps = electronics.TelemetryLaps;
    }

    private void MapFuelAndStrategy()
    {
      var strategy = this.setupFile.BasicSetup.Strategy;
      this.FuelAndStrategy.TyreCompound = this.setupFile.BasicSetup.Tyres.TyreCompound;
      this.FuelAndStrategy.Fuel = strategy.Fuel;
      this.FuelAndStrategy.PitStops = strategy.NPitStops;
      this.FuelAndStrategy.FrontBrakeCompound = strategy.FrontBrakePadCompound;
      this.FuelAndStrategy.RearBrakeCompound = strategy.RearBreakPadCompound;
      this.FuelAndStrategy.FuelPerLap = Math.Round(strategy.FuelPerLap, 1);

      this.FuelAndStrategy.PitStrategies.Clear();
      foreach(var pitStrategy in strategy.PitStrategies)
      {
        this.FuelAndStrategy.PitStrategies.Add(this.MapPitStrategy(pitStrategy));
      }
    }

    private PitStrategyViewModel MapPitStrategy(PitStrategy pitStrategy)
    {
      return new()
             {
               FrontBrakeCompound = pitStrategy.FrontBrakePadCompound,
               FuelToAdd = pitStrategy.FuelToAdd,
               RearBrakeCompound = pitStrategy.RearBreakPadCompound,
               TyreSet = pitStrategy.TyreSet,
               TyreCompound = pitStrategy.Tyres.TyreCompound,
               LeftFrontPsi = this.setupSpec.ToPressurePsi(pitStrategy.Tyres.TyrePressure[0]),
               RightFrontPsi = this.setupSpec.ToPressurePsi(pitStrategy.Tyres.TyrePressure[1]),
               LeftRearPsi = this.setupSpec.ToPressurePsi(pitStrategy.Tyres.TyrePressure[2]),
               RightRearPsi = this.setupSpec.ToPressurePsi(pitStrategy.Tyres.TyrePressure[03])
             };
    }

    private void MapSetupFile()
    {
      this.MapTyres();
      this.MapElectronics();
      this.MapFuelAndStrategy();
    }

    private void MapToe()
    {
      var toe = this.setupFile.BasicSetup.Alignment.Toe;
      this.Tyres.LeftFront.Toe = this.setupSpec.ToToe(toe[0], Location.Front);
      this.Tyres.RightFront.Toe = this.setupSpec.ToToe(toe[1], Location.Front);
      this.Tyres.LeftRear.Toe = this.setupSpec.ToToe(toe[2], Location.Rear);
      this.Tyres.RightRear.Toe = this.setupSpec.ToToe(toe[3], Location.Rear);
    }

    private void MapTyrePressures()
    {
      var tyrePressures = this.setupFile.BasicSetup.Tyres.TyrePressure;
      this.Tyres.LeftFront.PressurePsi = this.setupSpec.ToPressurePsi(tyrePressures[0]);
      this.Tyres.RightFront.PressurePsi = this.setupSpec.ToPressurePsi(tyrePressures[1]);
      this.Tyres.LeftRear.PressurePsi = this.setupSpec.ToPressurePsi(tyrePressures[2]);
      this.Tyres.RightRear.PressurePsi = this.setupSpec.ToPressurePsi(tyrePressures[3]);
    }

    private void MapTyres()
    {
      this.MapTyrePressures();
      this.MapToe();
      this.MapCamber();
      this.MapCaster();
    }
  }
}