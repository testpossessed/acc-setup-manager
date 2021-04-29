using System;
using System.Collections.Generic;
using ACCSetupManager.Models;

namespace ACCSetupManager.Services
{
  public class DifferenceCategory
  {
    public DifferenceCategory(string category, IList<Difference> differences)
    {
      this.Category = category;
      this.Differences = differences;
    }

    public DifferenceCategory(string category, IList<DifferenceCategory> subCategories)
    {
      this.Category = category;
      this.SubCategories = subCategories;
    }

    public IList<Difference> Differences { get; }
    public IList<DifferenceCategory> SubCategories { get; }
    public string Category { get; set; }
  }
}