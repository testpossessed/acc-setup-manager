using System;
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels.SetupFileComparisonViewer
{
  public class PitStrategyComparisonViewModel : ObservableObject
  {
    private int frontBrakeCompound;
    private bool frontBrakeCompoundDiffers;
    private int fuelToAdd;
    private bool fuelToAddDiffers;
    private double leftFrontPsi;
    private bool leftFrontPsiDiffers;
    private double leftRearPsi;
    private bool leftRearPsiDiffers;
    private int pitStopNumber;
    private int rearBrakeCompound;
    private bool rearBrakeCompoundDiffers;
    private double rightFrontPsi;
    private bool rightFrontPsiDiffers;
    private double rightRearPsi;
    private bool rightRearPsiDiffers;
    private string tyreCompound;
    private bool tyreCompoundDiffers;
    private int tyreSet;
    private bool tyreSetDiffers;
    
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

    public int FuelToAdd
    {
      get => this.fuelToAdd;
      set => this.SetProperty(ref this.fuelToAdd, value);
    }

    public bool FuelToAddDiffers
    {
      get => this.fuelToAddDiffers;
      set => this.SetProperty(ref this.fuelToAddDiffers, value);
    }
    public double LeftFrontPsi
    {
      get => this.leftFrontPsi;
      set => this.SetProperty(ref this.leftFrontPsi, value);
    }

    public bool LeftFrontPsiDiffers
    {
      get => this.leftFrontPsiDiffers;
      set => this.SetProperty(ref this.leftFrontPsiDiffers, value);
    }

    public double LeftRearPsi
    {
      get => this.leftRearPsi;
      set => this.SetProperty(ref this.leftRearPsi, value);
    }

    public bool LeftRearPsiDiffers
    {
      get => this.leftRearPsiDiffers;
      set => this.SetProperty(ref this.leftRearPsiDiffers, value);
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

    public bool RearBrakeCompoundDiffers
    {
      get => this.rearBrakeCompoundDiffers;
      set => this.SetProperty(ref this.rearBrakeCompoundDiffers, value);
    }
    public double RightFrontPsi
    {
      get => this.rightFrontPsi;
      set => this.SetProperty(ref this.rightFrontPsi, value);
    }

    public bool RightFrontPsiDiffers
    {
      get => this.rightFrontPsiDiffers;
      set => this.SetProperty(ref this.rightFrontPsiDiffers, value);
    }

    public double RightRearPsi
    {
      get => this.rightRearPsi;
      set => this.SetProperty(ref this.rightRearPsi, value);
    }

    public bool RightRearPsiDiffers
    {
      get => this.rightRearPsiDiffers;
      set => this.SetProperty(ref this.rightRearPsiDiffers, value);
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

    internal bool HasDifferences()
    {
      return this.FrontBrakeCompoundDiffers || this.FuelToAddDiffers || this.LeftFrontPsiDiffers
             || this.LeftRearPsiDiffers || this.RightFrontPsiDiffers || this.RightRearPsiDiffers
             || this.TyreCompoundDiffers || this.TyreSetDiffers;
    }
  }
}