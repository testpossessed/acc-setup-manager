using System;

namespace ACCSetupManager.Models
{
    public class AdvancedSetup
    {
        public MechanicalBalance MechanicalBalance { get; set; }
        public Dampers Dampers { get; set; }
        public AeroBalance AeroBalance { get; set; }
        public DriveTrain DriveTrain { get; set; }
    }
}