using System;
using ACCSetupManager.ViewModels;

namespace ACCSetupManager.Models
{
  public class SetupFileNotes
  {
    public string Comments { get; set; }
    public LapTime LapTimeAfter { get; set; }
    public LapTime LapTimeBefore { get; set; }
    public string ReasonForChange { get; set; }
  }
}