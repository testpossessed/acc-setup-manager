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
    public int RideHeightFrontAdjustment { get; set; }
    public int RideHeightRearAdjustment { get; set; }
    public int SplitterAdjustment { get; set; }
    public int SplitterFixed { get; set; }
    public int SteerRatioAdjustment { get; set; }
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

    internal int ToArb(int rawValue)
    {
      return rawValue + this.AntiRollBarAdjustment;
    }

    internal int ToArbRaw(int arb)
    {
      return arb - this.AntiRollBarAdjustment;
    }

    internal double ToBrakeBias(double rawValue)
    {
      return rawValue * this.BrakeBiasFactor + this.BrakeBiasAdjustment;
    }

    internal double ToBrakeBiasRaw(double brakeBias)
    {
      return (brakeBias - this.BrakeBiasAdjustment) / this.BrakeBiasFactor;
    }

    internal double ToBrakePower(double rawValue)
    {
      return rawValue + this.BrakePowerAdjustment;
    }

    internal double ToBrakePowerRaw(double brakePower)
    {
      return brakePower - this.BrakeBiasAdjustment;
    }

    internal double ToBumpStopRate(double rawValue)
    {
      return rawValue * this.BumpStopRateFactor + this.BumpStopRateAdjustment;
    }

    internal double ToBumpStopRateRaw(double bumpStopRate)
    {
      return (bumpStopRate - this.BumpStopRateAdjustment) / this.BumpStopRateFactor;
    }

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

    internal double ToDifferentialPreload(double rawValue)
    {
      if(this.PreloadFixed >= 0)
      {
        return this.PreloadFixed;
      }

      return rawValue * this.PreloadAdjustment + this.PreloadAdjustment;
    }

    internal double ToDifferentialPreloadRaw(double differentialPreload)
    {
      if(this.PreloadFixed >= 0)
      {
        return this.PreloadFixed;
      }

      return (differentialPreload - this.PreloadAdjustment) / this.PreloadFactor;
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

    internal int ToRearWing(int rawValue)
    {
      return rawValue + this.WingAdjustment;
    }

    internal int ToRearWingRaw(int rearWing)
    {
      return rearWing - this.WingAdjustment;
    }

    internal int ToRideHeight(int rawValue, Location location)
    {
      return rawValue + (location == Location.Front
                           ? this.RideHeightFrontAdjustment
                           : this.RideHeightRearAdjustment);
    }

    internal int ToRideHeightRaw(int rideHeight, Location location)
    {
      return rideHeight - (location == Location.Front
                             ? this.RideHeightFrontAdjustment
                             : this.RideHeightRearAdjustment);
    }

    internal int ToSplitter(int rawValue)
    {
      if(this.SplitterFixed >= 0)
      {
        return this.SplitterFixed;
      }

      return rawValue + this.SplitterAdjustment;
    }

    internal int ToSplitterRaw(int splitter)
    {
      if(this.SplitterFixed >= 0)
      {
        return this.SplitterFixed;
      }

      return splitter - this.SplitterAdjustment;
    }

    internal int ToSteerRatio(int rawValue)
    {
      return rawValue + this.SteerRatioAdjustment;
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

    internal double ToWheelRate(int rawValue, Location location)
    {
      var wheelRateMode = (WheelRateMode)this.WheelRateMode;
      if(wheelRateMode == Enums.WheelRateMode.Fixed)
      {
        return this.WheelRateFixed;
      }

      if(wheelRateMode == Enums.WheelRateMode.List)
      {
        if(location == Location.LeftFront || location == Location.RightFront)
        {
          return this.WheelRatesFront[rawValue];
        }

        return this.WheelRatesRear[rawValue];
      }

      if(location == Location.LeftFront || location == Location.RightFront)
      {
        return rawValue * this.WheelRateFrontFactor + this.WheelRateFrontAdjustment;
      }

      return rawValue * this.WheelRateRearFactor + this.WheelRateRearAdjustment;
    }
  }
}