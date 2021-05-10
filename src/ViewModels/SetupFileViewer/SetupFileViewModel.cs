using System;
using ACCSetupManager.Enums;
using ACCSetupManager.Models;
using ACCSetupManager.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels.SetupFileViewer
{
  public class SetupFileViewModel : ObservableObject
  {
    private readonly SetupFile setupFile;
    private readonly SetupSpec setupSpec;

    public SetupFileViewModel(string setupFilePath)
    {
      this.setupFile = SetupFileProvider.GetSetupFile(setupFilePath);
      this.setupSpec = SetupSpecProvider.GetSetupSpec(this.setupFile.VehicleIdentifier);
      this.Notes.Load(setupFilePath);
      this.MapSetupFile();
    }

    public AeroViewModel Aero { get; } = new();
    public DampersViewModel Dampers { get; } = new();
    public ElectronicsViewModel Electronics { get; } = new();
    public FuelAndStrategyViewModel FuelAndStrategy { get; } = new();
    public MechanicalBalanceViewModel MechanicalGrip { get; } = new();
    public SetupFileNotesViewModel Notes { get; } = new();
    public TyresViewModel Tyres { get; } = new();

    private void MapSetupFile()
    {
      this.Tyres.Apply(this.setupFile, this.setupSpec);
      this.Electronics.Apply(this.setupFile, this.setupSpec);
      this.FuelAndStrategy.Apply(this.setupFile, this.setupSpec);
      this.MechanicalGrip.Apply(this.setupFile, this.setupSpec);
      this.Dampers.Apply(this.setupFile);
      this.Aero.Apply(this.setupFile, this.setupSpec);
    }

    internal SetupFile ToSetupFile()
    {
      var result = new SetupFile();
      this.setupFile.TrackBopType = this.setupFile.TrackBopType;
      this.setupFile.VehicleIdentifier = this.setupFile.VehicleIdentifier;
      this.setupFile.FriendlyName = this.setupFile.FriendlyName;

      this.setupFile.BasicSetup = this.MapBasicSetup();
      

      return result;
    }

    private BasicSetup MapBasicSetup()
    {
      var result = new BasicSetup
                   {
                     Alignment = this.Tyres.ToAlignment(this.setupSpec)
                   };
      return result;
    }
  }
}