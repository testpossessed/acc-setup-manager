using System;

namespace ACCSetupManager.Models
{
    public class SetupFile
    {
        public string FriendlyName { get; set; }
        public string CarName { get; set; }
        public BasicSetup BasicSetup { get; set; }
        public AdvancedSetup AdvancedSetup { get; set; }
        public int TrackBopType { get; set; }
    }
}
