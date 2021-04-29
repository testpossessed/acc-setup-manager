using System;

namespace ACCSetupManager.Models
{
  public class LapTime
  {
    public LapTime(int minutes, double seconds)
    {
      this.Minutes = minutes;
      this.Seconds = seconds;
    }

    public int Minutes { get; }
    public double Seconds { get; }
  }
}