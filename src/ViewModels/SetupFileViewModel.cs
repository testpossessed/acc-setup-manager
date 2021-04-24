﻿using System;
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
    public MechanicalBalanceViewModel MechanicalGrip { get; } = new();
  
    private void MapSetupFile()
    {
      this.Tyres.Apply(this.setupFile, this.setupSpec);
      this.Electronics.Apply(this.setupFile);
      this.FuelAndStrategy.Apply(this.setupFile, this.setupSpec);
      this.MechanicalGrip.Apply(this.setupFile, this.setupSpec);
    }
  }
}