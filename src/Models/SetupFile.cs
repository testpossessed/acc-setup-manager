using System;
using Newtonsoft.Json;

namespace ACCSetupManager.Models
{
    public class SetupFile
    {
        public string FriendlyName { get; set; }
        [JsonProperty("carName")]
        public string VehicleIdentifier { get; set; }
        public BasicSetup BasicSetup { get; set; }
        public AdvancedSetup AdvancedSetup { get; set; }
        public int TrackBopType { get; set; }
    }
}
