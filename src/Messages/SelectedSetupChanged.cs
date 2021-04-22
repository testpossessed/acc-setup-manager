using System;
using ACCSetupManager.ViewModels;

namespace ACCSetupManager.Messages
{
  public class SelectedSetupChanged
  {
    public SelectedSetupChanged(SetupViewModel setup)
    {
      this.Setup = setup;
    }

    public SetupViewModel Setup { get; }
  }
}