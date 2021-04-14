using System;

namespace ACCSetupManager.Abstractions
{
  public interface IMasterSetupSync
  {
    void SyncMasters(Action<string> statusCallback);
  }
}