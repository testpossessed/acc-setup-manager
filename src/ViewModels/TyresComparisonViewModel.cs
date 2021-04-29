using System;
using ACCSetupManager.Enums;
using ACCSetupManager.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public class TyresComparisonViewModel : ObservableObject
  {
    public FrontTyreViewModel LeftFront { get; } = new();
    public RearTyreViewModel LeftRear { get; } = new();
    public FrontTyreViewModel RightFront { get; } = new();
    public RearTyreViewModel RightRear { get; } = new();

    public void Apply(SetupFile setupFile,
      SetupSpec setupSpec,
      SetupFileViewModel compareToSetupFileViewModel)
    {
      this.MapTyrePressures(setupFile, setupSpec);
      this.MapToe(setupFile, setupSpec);
      this.MapCamber(setupFile, setupSpec);
      this.MapCaster(setupFile, setupSpec);
    }

    private void MapCamber(SetupFile setupFile, SetupSpec setupSpec)
    {
      var camber = setupFile.BasicSetup.Alignment.Camber;
      this.LeftFront.Camber = setupSpec.ToCamber(camber[0], Location.Front);
      this.RightFront.Camber = setupSpec.ToCamber(camber[1], Location.Front);
      this.LeftRear.Camber = setupSpec.ToCamber(camber[2], Location.Rear);
      this.RightRear.Camber = setupSpec.ToCamber(camber[3], Location.Rear);
    }

    private void MapCaster(SetupFile setupFile, SetupSpec setupSpec)
    {
      this.LeftFront.Caster = setupSpec.ToCaster(setupFile.BasicSetup.Alignment.CasterLF);
      this.RightFront.Caster = setupSpec.ToCaster(setupFile.BasicSetup.Alignment.CasterRF);
    }

    private void MapToe(SetupFile setupFile, SetupSpec setupSpec)
    {
      var toe = setupFile.BasicSetup.Alignment.Toe;
      this.LeftFront.Toe = setupSpec.ToToe(toe[0], Location.Front);
      this.RightFront.Toe = setupSpec.ToToe(toe[1], Location.Front);
      this.LeftRear.Toe = setupSpec.ToToe(toe[2], Location.Rear);
      this.RightRear.Toe = setupSpec.ToToe(toe[3], Location.Rear);
    }

    private void MapTyrePressures(SetupFile setupFile, SetupSpec setupSpec)
    {
      var tyrePressures = setupFile.BasicSetup.Tyres.TyrePressure;
      this.LeftFront.PressurePsi = setupSpec.ToPressurePsi(tyrePressures[0]);
      this.RightFront.PressurePsi = setupSpec.ToPressurePsi(tyrePressures[1]);
      this.LeftRear.PressurePsi = setupSpec.ToPressurePsi(tyrePressures[2]);
      this.RightRear.PressurePsi = setupSpec.ToPressurePsi(tyrePressures[3]);
    }
  }
}