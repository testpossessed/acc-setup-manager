using System;
using ACCSetupManager.Models;
using ACCSetupManager.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public class SetupFileComparisonViewModel : ObservableObject
  {
    private readonly SetupFileViewModel compareToSetupFileViewModel;
    private readonly SetupFile setupFile;
    private readonly SetupSpec setupSpec;

    public SetupFileComparisonViewModel(string setupFilePath, SetupFileViewModel compareToSetupFileViewModel)
    {
      this.compareToSetupFileViewModel = compareToSetupFileViewModel;
      this.setupFile = SetupFileProvider.GetSetupFile(setupFilePath);
      this.setupSpec = SetupSpecProvider.GetSetupSpec(this.setupFile.VehicleIdentifier);
      this.Notes.Load(setupFilePath);
      this.MapSetupFile();
    }

    public AeroComparisonViewModel Aero { get; } = new();
    public DampersComparisonViewModel Dampers { get; } = new();
    public ElectronicsComparisonViewModel Electronics { get; } = new();
    public FuelAndStrategyComparisonViewModel FuelAndStrategy { get; } = new();
    public MechanicalBalanceComparisonViewModel MechanicalGrip { get; } = new();
    public SetupFileNotesViewModel Notes { get; } = new();
    public TyresComparisonViewModel Tyres { get; } = new();

    private void MapSetupFile()
    {
      this.Tyres.Apply(this.setupFile, this.setupSpec, this.compareToSetupFileViewModel);
      this.Electronics.Apply(this.setupFile, this.compareToSetupFileViewModel);
      this.FuelAndStrategy.Apply(this.setupFile, this.setupSpec, this.compareToSetupFileViewModel);
      this.MechanicalGrip.Apply(this.setupFile, this.setupSpec, this.compareToSetupFileViewModel);
      this.Dampers.Apply(this.setupFile, this.compareToSetupFileViewModel);
      this.Aero.Apply(this.setupFile, this.setupSpec, this.compareToSetupFileViewModel);
    }
  }
}