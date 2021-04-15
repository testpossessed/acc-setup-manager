using System;
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public class CircuitViewModel : ObservableObject
  {
    public CircuitViewModel()
    {
      this.Setups = new ObservableCollection<SetupViewModel>();
    }

    public string FolderName { get; set; }
    public string Name { get; set; }
    public ObservableCollection<SetupViewModel> Setups { get; set; }
  }
}