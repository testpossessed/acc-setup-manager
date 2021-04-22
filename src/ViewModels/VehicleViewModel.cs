using System;
using System.Collections.ObjectModel;

namespace ACCSetupManager.ViewModels
{
  public class VehicleViewModel
  {
    public VehicleViewModel()
    {
      this.Circuits = new ObservableCollection<CircuitViewModel>();
    }

    public ObservableCollection<CircuitViewModel> Circuits { get; set; }
    public string Identifier { get; set; }
    public string Name { get; set; }
  }
}