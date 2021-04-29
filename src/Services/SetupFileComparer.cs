using System;
using System.Collections.Generic;
using System.Linq;
using ACCSetupManager.Models;

namespace ACCSetupManager.Services
{
  public static class SetupFileComparer
  {
    internal static IEnumerable<DifferenceCategory> Differences(SetupFile source,
      SetupFile target,
      SetupSpec setupSpec)
    {
      var result = new List<DifferenceCategory>();

      PopulateTyreDifferences(result, source, target, setupSpec);

      PopulateFuelAndStrategyDifferences(result, source, target, setupSpec);
      return result;
    }

    private static string FormatDouble(double value)
    {
      return value.ToString("F1");
    }

    private static string FormatInt(double value)
    {
      return value.ToString("F0");
    }

    private static void PopulateFuelAndStrategyDifferences(List<DifferenceCategory> result,
      SetupFile source,
      SetupFile target,
      SetupSpec setupSpec)
    {

    }

    private static void PopulateTyreDifferences(List<DifferenceCategory> differenceCategories,
      SetupFile source,
      SetupFile target,
      SetupSpec setupSpec)
    {
      var differences = new List<Difference>();
      var sourceTyres = source.BasicSetup.Tyres;
      var targetTyres = target.BasicSetup.Tyres;

      PopulateTyrePressureDifferences(setupSpec, sourceTyres, targetTyres, differences);

      if(differences.Any())
      {
        differenceCategories.Add(new DifferenceCategory("Tyres", differences));
      }
    }

    private static void PopulateTyrePressureDifferences(SetupSpec setupSpec,
      Tyres sourceTyres,
      Tyres targetTyres,
      ICollection<Difference> differences)
    {
      if(!sourceTyres.TyrePressure[0]
                     .Equals(targetTyres.TyrePressure[0]))
      {
        differences.Add(new Difference("Front Left PSI",
          FormatDouble(setupSpec.ToPressurePsi(targetTyres.TyrePressure[0]))));
      }

      if(!sourceTyres.TyrePressure[1]
                     .Equals(targetTyres.TyrePressure[1]))
      {
        differences.Add(new Difference("Front Right PSI",
          FormatDouble(setupSpec.ToPressurePsi(targetTyres.TyrePressure[1]))));
      }

      if(!sourceTyres.TyrePressure[2]
                     .Equals(targetTyres.TyrePressure[2]))
      {
        differences.Add(new Difference("Rear Left PSI",
          FormatDouble(setupSpec.ToPressurePsi(targetTyres.TyrePressure[2]))));
      }

      if(!sourceTyres.TyrePressure[3]
                     .Equals(targetTyres.TyrePressure[3]))
      {
        differences.Add(new Difference("Rear Right PSI",
          FormatDouble(setupSpec.ToPressurePsi(targetTyres.TyrePressure[3]))));
      }
    }
  }
}