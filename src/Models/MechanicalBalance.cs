using System;

namespace ACCSetupManager.Models
{
    public class MechanicalBalance
    {
        public int ARBFront { get; set; }
        public int ARBRear { get; set; }
        public int[] WheelRate { get; set; }
        public double[] BumpStopRateUp { get; set; }
        public double[] BumpStopRateDn { get; set; }
        public double[] BumpStopWindow { get; set; }
        public double BrakeTorque { get; set; }
        public double BrakeBias { get; set; }
    }
}