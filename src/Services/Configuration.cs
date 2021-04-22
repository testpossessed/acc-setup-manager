using System;
using Microsoft.Extensions.Configuration;

namespace ACCSetupManager.Services
{
  public static class Configuration
  {
    private static IConfigurationRoot configuration;

    internal static void Init()
    {
      var configurationBuilder = new ConfigurationBuilder();
      configurationBuilder.SetBasePath(PathProvider.AppDataFolderPath)
                          .AddJsonFile(PathProvider.AppSettingsFileName, false, true)
                          .AddEnvironmentVariables();
      configuration = configurationBuilder.Build();
    }

    internal static IConfiguration CurrentConfig => configuration;

    public static IConfigurationSection GetSection(string key)
    {
      return configuration.GetSection(key);
    }
  }
}