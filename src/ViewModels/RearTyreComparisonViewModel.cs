using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public class RearTyreComparisonViewModel : ObservableObject
  {
    private double camber;
    private double pressurePsi;
    private double toe;

    public double Camber
    {
      get => this.camber;
      set => this.SetProperty(ref this.camber, value);
    }

    public double PressurePsi
    {
      get => this.pressurePsi;
      set => this.SetProperty(ref this.pressurePsi, value);
    }

    public double Toe
    {
      get => this.toe;
      set => this.SetProperty(ref this.toe, value);
    }
  }
}