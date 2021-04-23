using System;
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

    private void MapSetupFile()
    {
      this.MapTyrePressures();
      this.MapToe();
      this.MapCamber();
      this.MapCaster();
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
  }
}