using System;

namespace ACCSetupManager.Models
{
    public class Dampers
    {
        public int[] BumpSlow { get; set; }
        public int[] BumpFast { get; set; }
        public int[] ReboundSlow { get; set; }
        public int[] ReboundFast { get; set; }
    }
}