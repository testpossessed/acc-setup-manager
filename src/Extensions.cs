using System;

namespace ACCSetupManager
{
  public static class Extensions
  {
    public static bool IsEqualTo(this double val, double compareTo)
    {
      return val <= compareTo + double.Epsilon && val >= compareTo - double.Epsilon;
    }
  }
}