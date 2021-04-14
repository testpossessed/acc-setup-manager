using System;

namespace ACCSetupManager.Models
{
    public class Alignment
    {
        public double[] Camber { get; set; }
        public double[] Toe { get; set; }
        public double[] StaticCamber { get; set; }
        public double[] ToeOutLinear { get; set; }
        public double CasterLF { get; set; }
        public double CasterRF { get; set; }
        public int SteerRatio { get; set; }
    }
}