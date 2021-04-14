using System;
using System.Collections.Generic;

namespace ACCSetupManager.Models
{
  public class Vehicle
  {
    public Vehicle()
    {
      this.Circuits = new List<Circuit>();
    }

    public IList<Circuit> Circuits { get; set; }
    public string FolderName { get; set; }
    public string Name { get; set; }
  }
}