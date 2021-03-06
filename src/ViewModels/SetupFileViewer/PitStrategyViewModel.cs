using System;
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels.SetupFileViewer
{
  public class PitStrategyViewModel : ObservableObject
  {
    private int frontBrakeCompound;
    private int fuelToAdd;
    private double leftFrontPsi;
    private double leftRearPsi;
    private int pitStopNumber;
    private int rearBrakeCompound;
    private double rightFrontPsi;
    private double rightRearPsi;
    private string tyreCompound;
    private int tyreSet;

    public int FrontBrakeCompound
    {
      get => this.frontBrakeCompound;
      set => this.SetProperty(ref this.frontBrakeCompound, value);
    }

    public int FuelToAdd
    {
      get => this.fuelToAdd;
      set => this.SetProperty(ref this.fuelToAdd, value);
    }

    public double LeftFrontPsi
    {
      get => this.leftFrontPsi;
      set => this.SetProperty(ref this.leftFrontPsi, value);
    }

    public double LeftRearPsi
    {
      get => this.leftRearPsi;
      set => this.SetProperty(ref this.leftRearPsi, value);
    }

    public int PitStopNumber
    {
      get => this.pitStopNumber;
      set => this.SetProperty(ref this.pitStopNumber, value);
    }

    public int RearBrakeCompound
    {
      get => this.rearBrakeCompound;
      set => this.SetProperty(ref this.rearBrakeCompound, value);
    }

    public double RightFrontPsi
    {
      get => this.rightFrontPsi;
      set => this.SetProperty(ref this.rightFrontPsi, value);
    }

    public double RightRearPsi
    {
      get => this.rightRearPsi;
      set => this.SetProperty(ref this.rightRearPsi, value);
    }

    public string TyreCompound
    {
      get => this.tyreCompound;
      set => this.SetProperty(ref this.tyreCompound, value);
    }

    public int TyreSet
    {
      get => this.tyreSet;
      set => this.SetProperty(ref this.tyreSet, value);
    }
  }
}