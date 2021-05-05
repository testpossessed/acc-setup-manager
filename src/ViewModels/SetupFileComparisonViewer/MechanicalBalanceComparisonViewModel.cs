using System;
using ACCSetupManager.Enums;
using ACCSetupManager.Models;
using ACCSetupManager.ViewModels.SetupFileViewer;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels.SetupFileComparisonViewer
{
  public class MechanicalBalanceComparisonViewModel : ObservableObject
  {
    private bool arbRearDiffers;
    private int arbFront;
    private bool arbFrontDiffers;
    private int arbRear;
    private double brakeBias;
    private bool brakeBiasDiffers;
    private double brakePower;
    private bool brakePowerDiffers;
    private double differentialPreload;
    private bool differentialPreloadDiffers;
    private int steerRatio;
    private bool steerRatioDiffers;

    public MechanicalGripCornerComparisonViewModel LeftFrontMechanicalGroup { get; } = new();
    public MechanicalGripCornerComparisonViewModel LeftRearMechanicalGroup { get; } = new();
    public MechanicalGripCornerComparisonViewModel RightFrontMechanicalGroup { get; } = new();
    public MechanicalGripCornerComparisonViewModel RightRearMechanicalGroup { get; } = new();

    public bool ArbRearDiffers
    {
      get => this.arbRearDiffers;
      set => this.SetProperty(ref this.arbRearDiffers, value);
    }

    public int ArbFront
    {
      get => this.arbFront;
      set => this.SetProperty(ref this.arbFront, value);
    }

    public bool ArbFrontDiffers
    {
      get => this.arbFrontDiffers;
      set => this.SetProperty(ref this.arbFrontDiffers, value);
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

    public bool BrakeBiasDiffers
    {
      get => this.brakeBiasDiffers;
      set => this.SetProperty(ref this.brakeBiasDiffers, value);
    }

    public double BrakePower
    {
      get => this.brakePower;
      set => this.SetProperty(ref this.brakePower, value);
    }

    public bool BrakePowerDiffers
    {
      get => this.brakePowerDiffers;
      set => this.SetProperty(ref this.brakePowerDiffers, value);
    }

    public double DifferentialPreload
    {
      get => this.differentialPreload;
      set => this.SetProperty(ref this.differentialPreload, value);
    }

    public bool DifferentialPreloadDiffers
    {
      get => this.differentialPreloadDiffers;
      set => this.SetProperty(ref this.differentialPreloadDiffers, value);
    }

    public int SteerRatio
    {
      get => this.steerRatio;
      set => this.SetProperty(ref this.steerRatio, value);
    }

    public bool SteerRatioDiffers
    {
      get => this.steerRatioDiffers;
      set => this.SetProperty(ref this.steerRatioDiffers, value);
    }

    internal void Apply(SetupFile setupFile,
      SetupSpec setupSpec,
      SetupFileViewModel compareToSetupFileViewModel)
    {
      var mechanicalBalance = setupFile.AdvancedSetup.MechanicalBalance;
      var comparisonMechanicalGrip = compareToSetupFileViewModel.MechanicalGrip;
      this.ArbFront = setupSpec.ToArb(mechanicalBalance.ARBFront);
      this.ArbFrontDiffers = this.ArbFront != comparisonMechanicalGrip.ArbFront;
      this.ArbRear = setupSpec.ToArb(mechanicalBalance.ARBRear);
      this.ArbRearDiffers = this.ArbRear != comparisonMechanicalGrip.ArbRear;
      this.BrakeBias = setupSpec.ToBrakeBias(mechanicalBalance.BrakeBias);
      this.BrakeBiasDiffers = !this.BrakeBias.IsEqualTo(comparisonMechanicalGrip.BrakeBias);
      this.BrakePower = setupSpec.ToBrakePower(mechanicalBalance.BrakeTorque);
      this.BrakePowerDiffers = !this.BrakePower.IsEqualTo(comparisonMechanicalGrip.BrakePower);
      this.DifferentialPreload =
        setupSpec.ToDifferentialPreload(setupFile.AdvancedSetup.DriveTrain.Preload);
      this.DifferentialPreloadDiffers =
        !this.DifferentialPreload.IsEqualTo(comparisonMechanicalGrip.DifferentialPreload);
      this.SteerRatio = setupSpec.ToSteerRatio(setupFile.BasicSetup.Alignment.SteerRatio);
      this.SteerRatioDiffers =
        this.SteerRatio != comparisonMechanicalGrip.SteerRatio;

      this.LeftFrontMechanicalGroup.Apply(setupFile,
        setupSpec,
        Location.LeftFront,
        compareToSetupFileViewModel.MechanicalGrip);
      this.LeftRearMechanicalGroup.Apply(setupFile,
        setupSpec,
        Location.LeftRear,
        compareToSetupFileViewModel.MechanicalGrip);
      this.RightFrontMechanicalGroup.Apply(setupFile,
        setupSpec,
        Location.RightFront,
        compareToSetupFileViewModel.MechanicalGrip);
      this.RightRearMechanicalGroup.Apply(setupFile,
        setupSpec,
        Location.RightRear,
        compareToSetupFileViewModel.MechanicalGrip);
    }

    internal bool HasDifferences()
    {
      return this.ArbFrontDiffers || this.ArbRearDiffers || this.BrakeBiasDiffers
             || this.BrakePowerDiffers || this.DifferentialPreloadDiffers || this.SteerRatioDiffers
             || this.LeftFrontMechanicalGroup.HasDifferences()
             || this.LeftRearMechanicalGroup.HasDifferences()
             || this.RightFrontMechanicalGroup.HasDifferences()
             || this.RightRearMechanicalGroup.HasDifferences();
    }
  }
}