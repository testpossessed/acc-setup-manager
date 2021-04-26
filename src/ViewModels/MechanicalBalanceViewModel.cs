using System;
using ACCSetupManager.Enums;
using ACCSetupManager.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public class MechanicalBalanceViewModel : ObservableObject
  {
    private int arbFront;
    private int arbRear;
    private double brakeBias;
    private double brakePower;
    private double differentialPreload;
    private int steerRatio;

    public MechanicalGripCornerViewModel LeftFrontMechanicalGroup { get; } = new();
    public MechanicalGripCornerViewModel LeftRearMechanicalGroup { get; } = new();
    public MechanicalGripCornerViewModel RightFrontMechanicalGroup { get; } = new();
    public MechanicalGripCornerViewModel RightRearMechanicalGroup { get; } = new();

    public int ArbFront
    {
      get => this.arbFront;
      set => this.SetProperty(ref this.arbFront, value);
    }

    public int ArbRear
    {
      get => this.arbRear;
      set => this.SetProperty(ref this.arbRear, value);
    }

    public double BrakeBias
    {
      get => this.brakeBias;
      set => this.SetProperty(ref this.brakeBias, value);
    }

    public double BrakePower
    {
      get => this.brakePower;
      set => this.SetProperty(ref this.brakePower, value);
    }

    public double DifferentialPreload
    {
      get => this.differentialPreload;
      set => this.SetProperty(ref this.differentialPreload, value);
    }

    public int SteerRatio
    {
      get => this.steerRatio;
      set => this.SetProperty(ref this.steerRatio, value);
    }

    internal void Apply(SetupFile setupFile, SetupSpec setupSpec)
    {
      var mechanicalBalance = setupFile.AdvancedSetup.MechanicalBalance;
      this.ArbFront = setupSpec.ToArb(mechanicalBalance.ARBFront);
      this.ArbRear = setupSpec.ToArb(mechanicalBalance.ARBRear);
      this.BrakeBias = setupSpec.ToBrakeBias(mechanicalBalance.BrakeBias);
      this.BrakePower = setupSpec.ToBrakePower(mechanicalBalance.BrakeTorque);
      this.DifferentialPreload =
        setupSpec.ToDifferentialPreload(setupFile.AdvancedSetup.DriveTrain.Preload);
      this.SteerRatio = setupSpec.ToSteerRatio(setupFile.BasicSetup.Alignment.SteerRatio);

      this.LeftFrontMechanicalGroup.Apply(setupFile, setupSpec, Location.LeftFront);
      this.LeftRearMechanicalGroup.Apply(setupFile, setupSpec, Location.LeftRear);
      this.RightFrontMechanicalGroup.Apply(setupFile, setupSpec, Location.RightFront);
      this.RightRearMechanicalGroup.Apply(setupFile, setupSpec, Location.RightRear);
    }
  }
}