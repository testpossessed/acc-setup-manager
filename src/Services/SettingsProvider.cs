using System;
using System.IO;
using ACCSetupManager.Models;
using Newtonsoft.Json;

namespace ACCSetupManager.Services
{
  internal static class SettingsProvider
  {
    internal static UserSettings GetSettings()
    {
      if(!File.Exists(PathProvider.UserSettingsFilePath))
      {
        return new UserSettings
               {
                 Theme = "Blend"
               };
      }

      var json = File.ReadAllText(PathProvider.UserSettingsFilePath);
      return JsonConvert.DeserializeObject<UserSettings>(json);
    }

    internal static void SaveSettings(UserSettings settings)
    {
      var json = JsonConvert.SerializeObject(settings);
      File.WriteAllText(PathProvider.UserSettingsFilePath, json);
    }
  }
}