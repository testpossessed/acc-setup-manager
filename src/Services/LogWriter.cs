using System;
using NLog;
using NLog.Extensions.Logging;

namespace ACCSetupManager.Services
{
  internal static class LogWriter
  {
    private static Logger logger;

    internal static void Init()
    {
      logger = LogManager.Setup()
                         .LoadConfigurationFromSection(Configuration.CurrentConfig)
                         .GetCurrentClassLogger();
      LogManager.Configuration.Variables["appDataFolder"] = PathProvider.AppDataFolderPath;
    }

    internal static void Log(LogLevel level, string message)
    {
      logger.Log(level, message);
    }
  }
}