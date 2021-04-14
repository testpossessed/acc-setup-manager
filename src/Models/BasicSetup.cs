using System;

namespace ACCSetupManager.Models
{
    public class BasicSetup
    {
        public Tyres Tyres { get; set; }
        public Alignment Alignment { get; set; }

        public Electronics Electronics { get; set; }

        public Strategy Strategy { get; set; }
    }
}
