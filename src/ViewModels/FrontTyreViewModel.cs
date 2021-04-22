using System;

namespace ACCSetupManager.ViewModels
{
  public class FrontTyreViewModel : RearTyreViewModel
  {
    private double caster;

    public double Caster
    {
      get => this.caster;
      set => this.SetProperty(ref this.caster, value);
    }
  }
}