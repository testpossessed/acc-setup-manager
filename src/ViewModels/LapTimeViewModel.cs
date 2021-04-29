using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public class LapTimeViewModel : ObservableObject
  {
    private int minutes;
    private double seconds;

    public int Minutes
    {
      get => this.minutes;
      set => this.SetProperty(ref this.minutes, value);
    }

    public double Seconds
    {
      get => this.seconds;
      set => this.SetProperty(ref this.seconds, value);
    }
  }
}