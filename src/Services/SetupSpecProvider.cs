using System;
using System.IO;
using ACCSetupManager.Models;
using Newtonsoft.Json;

namespace ACCSetupManager.Services
{
  internal static class SetupSpecProvider
  {
    internal static SetupSpec GetSetupSpec(string vehicleIdentifier)
    {
      var filePath = Path.Combine(PathProvider.SetupDataFolderPath, $"{vehicleIdentifier}.json");
      var json = File.ReadAllText(filePath);
      return JsonConvert.DeserializeObject<SetupSpec>(json);
    }
  }
}