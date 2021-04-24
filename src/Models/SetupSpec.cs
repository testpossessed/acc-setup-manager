using System;
using ACCSetupManager.Enums;

namespace ACCSetupManager.Models
{
  public class SetupSpec
  {
    public int AntiRollBarAdjustment { get; set; }
    public double BrakeBiasAdjustment { get; set; }
    public double BrakeBiasFactor { get; set; }
    public double BrakePowerAdjustment { get; set; }
    public double BumpSlowAdjustment { get; set; }
    public double BumpStopRateAdjustment { get; set; }
    public double BumpStopRateFactor { get; set; }
    public double CamberFactor { get; set; }
    public double CamberFrontAdjustment { get; set; }
    public double CamberRearAdjustment { get; set; }
    public double CasterAdjustment { get; set; }
    public double[] CasterFactor { get; set; }
    public double CasterFixed { get; set; }
    public int EcuAdjustment { get; set; }
    public int EcuFixed { get; set; }
    public double PreloadAdjustment { get; set; }
    public double PreloadFactor { get; set; }
    public double PreloadFixed { get; set; }
    public double ReboundSlowAdjustment { get; set; }
    public double RideHeightFrontAdjustment { get; set; }
    public double RideHeightRearAdjustment { get; set; }
    public int SplitterAdjustment { get; set; }
    public int SplitterFixed { get; set; }
    public double SteerRatioAdjustment { get; set; }
    public double ToeFactor { get; set; }
    public double ToeFrontAdjustment { get; set; }
    public double ToeRearAdjustment { get; set; }
    public double TyrePressureAdjustment { get; set; }
    public double TyrePressureFactor { get; set; }
    public double WheelRateFixed { get; set; }
    public double WheelRateFrontAdjustment { get; set; }
    public double WheelRateFrontFactor { get; set; }
    public int WheelRateMode { get; set; }
    public double WheelRateRearAdjustment { get; set; }
    public double WheelRateRearFactor { get; set; }
    public double[] WheelRatesFront { get; set; }
    public double[] WheelRatesRear { get; set; }
    public int WingAdjustment { get; set; }

    private double CasterFactorValue => this.CasterFactor[0] / this.CasterFactor[1];

    internal double ToCamber(double rawValue, Location location)
    {
      return rawValue * this.CamberFactor + (location == Location.Front
                                                          ? this.CamberFrontAdjustment
                                                          : this.CamberRearAdjustment);
    }

    internal double ToCamberRaw(double camber, Location location)
    {
      return (camber - (location == Location.Front
                          ? this.CamberFrontAdjustment
                          : this.CamberRearAdjustment)) / this.CamberFactor;
    }

    internal double ToCaster(double rawValue)
    {
      return Math.Round(rawValue * this.CasterFactorValue + this.CasterAdjustment,
        1,
        MidpointRounding.ToPositiveInfinity);
    }

    internal double ToCasterRaw(double caster)
    {
      return (caster - this.CasterAdjustment) / this.CasterFactorValue;
    }

    internal double ToPressurePsi(double rawValue)
    {
      return Math.Round(rawValue * this.TyrePressureFactor + this.TyrePressureAdjustment,
        1,
        MidpointRounding.ToPositiveInfinity);
    }

    internal double ToPressureRaw(double pressurePsi)
    {
      return (pressurePsi - this.TyrePressureAdjustment) / this.TyrePressureFactor;
    }

    internal double ToToe(double rawValue, Location location)
    {
      return rawValue * this.ToeFactor + (location == Location.Front
                                            ? this.ToeFrontAdjustment
                                            : this.ToeRearAdjustment);
    }

    internal double ToToeRaw(double toe, Location location)
    {
      return (toe - (location == Location.Front? this.ToeFrontAdjustment: this.ToeRearAdjustment))
             / this.ToeFactor;
    }
  }
}