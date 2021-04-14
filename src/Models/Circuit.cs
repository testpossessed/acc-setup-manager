using System.Collections.Generic;

namespace ACCSetupManager.Models
{
  public class Circuit
  {
    public Circuit()
    {
      this.Setups = new List<Setup>();
    }
    public string Name { get; set; }
    public string FolderName { get; set; }
    public IList<Setup> Setups { get; set; }
  }
}