using System;
using ACCSetupManager.Enums;
using ACCSetupManager.Models;
using ACCSetupManager.ViewModels.SetupFileViewer;

namespace ACCSetupManager.ViewModels.SetupFileComparisonViewer
{
  public class TyreComparisonViewModel : VehicleCornerViewModelBase
  {
    private double camber;
    private bool camberDiffers;
    private double pressurePsi;
    private bool pressurePsiDiffers;
    private double toe;
    private bool toeDiffers;

    public double Camber
    {
      get => this.camber;
      set => this.SetProperty(ref this.camber, value);
    }

    public bool CamberDiffers
    {
      get => this.camberDiffers;
      set => this.SetProperty(ref this.camberDiffers, value);
    }
    public double PressurePsi
    {
      get => this.pressurePsi;
      set => this.SetProperty(ref this.pressurePsi, value);
    }

    public bool PressurePsiDiffers
    {
      get => this.pressurePsiDiffers;
      set => this.SetProperty(ref this.pressurePsiDiffers, value);
    }

    public double Toe
    {
      get => this.toe;
      set => this.SetProperty(ref this.toe, value);
    }

    public bool ToeDiffers
    {
      get => this.toeDiffers;
      set => this.SetProperty(ref this.toeDiffers, value);
    }

    internal void Apply(SetupFile setupFile,
      SetupSpec setupSpec,
      Location location,
      RearTyreViewModel compareToViewModel)
    {
      this.SetTitle(location);

      var index = this.GetIndexFromLocation(location);

      this.Camber =
        Math.Round(setupSpec.ToCamber(setupFile.BasicSetup.Alignment.Camber[index], location),
          1,
          MidpointRounding.ToPositiveInfinity);
      this.CamberDiffers = !this.Camber.IsEqualTo(compareToViewModel.Camber);
      this.PressurePsi = setupSpec.ToPressurePsi(setupFile.BasicSetup.Tyres.TyrePressure[index]);
      this.PressurePsiDiffers = !this.PressurePsi.IsEqualTo(compareToViewModel.PressurePsi);
      this.Toe = setupSpec.ToToe(setupFile.BasicSetup.Alignment.Toe[index], location);
      this.ToeDiffers = !this.Toe.IsEqualTo(compareToViewModel.Toe);
    }

    internal virtual bool HasDifferences()
    {
      return this.CamberDiffers || this.PressurePsiDiffers || this.ToeDiffers;
    }
  }
}