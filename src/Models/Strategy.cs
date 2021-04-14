using System;

namespace ACCSetupManager.Models
{
    public class Strategy
    {
        public int Fuel { get; set; }
        public int NPitStops { get; set; }
        public int TyreSet { get; set; }
        public int FrontBrakePadCompound { get; set; }
        public int RearBreakPadCompound { get; set; }
        public PitStrategy[] PitStrategy { get; set; }
        public double FuelPerLap { get; set; }
    }
}