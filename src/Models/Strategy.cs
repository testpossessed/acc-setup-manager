using System;
using Newtonsoft.Json;

namespace ACCSetupManager.Models
{
    public class Strategy
    {
        public int Fuel { get; set; }
        public int NPitStops { get; set; }
        public int TyreSet { get; set; }
        public int FrontBrakePadCompound { get; set; }
        public int RearBreakPadCompound { get; set; }
        [JsonProperty("pitStrategy")]
        public PitStrategy[] PitStrategies { get; set; }
        public double FuelPerLap { get; set; }
    }
}