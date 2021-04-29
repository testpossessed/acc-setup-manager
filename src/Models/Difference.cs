using System;

namespace ACCSetupManager.Models
{
  public class Difference
  {
    public Difference(string setting, string formattedValue)
    {
      this.Setting = setting;
      this.FormattedValue = formattedValue;
    }

    public string FormattedValue { get; }
    public string Setting { get; }
  }
}