using System;
using ACCSetupManager.Enums;
using ACCSetupManager.Models;
using ACCSetupManager.ViewModels.SetupFileViewer;

namespace ACCSetupManager.ViewModels.SetupFileComparisonViewer
{
  public class FrontTyreComparisonViewModel : TyreComparisonViewModel
  {
    private double caster;
    private bool casterDiffers;

    public double Caster
    {
      get => this.caster;
      set => this.SetProperty(ref this.caster, value);
    }

    public bool CasterDiffers
    {
      get => this.casterDiffers;
      set => this.SetProperty(ref this.casterDiffers, value);
    }

    internal void Apply(SetupFile setupFile,
      SetupSpec setupSpec,
      Location location,
      FrontTyreViewModel compareToViewModel)
    {
      base.Apply(setupFile, setupSpec, location, compareToViewModel);

      var casterSource = location == Location.LeftFront
                           ? setupFile.BasicSetup.Alignment.CasterLF
                           : setupFile.BasicSetup.Alignment.CasterRF;
      this.Caster = setupSpec.ToCaster(casterSource);
      this.CasterDiffers = !this.Caster.IsEqualTo(compareToViewModel.Caster);
    }

    internal override bool HasDifferences()
    {
      return base.HasDifferences() || this.casterDiffers;
    }
  }
}