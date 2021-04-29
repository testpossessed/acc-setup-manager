using System;

namespace ACCSetupManager.ViewModels
{
  public class FrontTyreComparisonViewModel : RearTyreComparisonViewModel
  {
    private double caster;

    public double Caster
    {
      get => this.caster;
      set => this.SetProperty(ref this.caster, value);
    }
  }
}