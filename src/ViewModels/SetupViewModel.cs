using System;
using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public class SetupViewModel : ObservableObject
  {
    private bool isSelected;
    public Visibility DeleteButtonVisibility =>
      this.IsVersion? Visibility.Visible: Visibility.Hidden;

    public string CircuitIdentifier { get; set; }
    public string FilePath { get; set; }
    public bool IsSelected
    {
      get => this.isSelected;
      set => this.SetProperty(ref this.isSelected, value);
    }
    public bool IsVersion { get; set; }
    public string Name { get; set; }
    public string VehicleIdentifier { get; set; }
  }
}