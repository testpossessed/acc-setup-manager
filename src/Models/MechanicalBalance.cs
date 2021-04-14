using System;

namespace ACCSetupManager.Models
{
    public class MechanicalBalance
    {
        public int ARBFront { get; set; }
        public int ARBRear { get; set; }
        public int[] WheelRate { get; set; }
        public int[] BumpStopRateUp { get; set; }
        public int[] BumpStopRateOn { get; set; }
        public int[] BumpStopWindow { get; set; }
        public int BrakeTorque { get; set; }
        public double BrakeBias { get; set; }
    }
}