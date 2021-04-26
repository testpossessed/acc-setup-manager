using System;
using ACCSetupManager.Enums;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public abstract class VehicleCornerViewModelBase : ObservableObject
  {
    private string title;
    public string Title
    {
      get => this.title;
      set => this.SetProperty(ref this.title, value);
    }

    internal void SetTitle(Location location)
    {
      switch(location)
      {
        case Location.RightFront:
          this.Title = "Right Front";
          break;
        case Location.LeftRear:
          this.Title = "Left Rear";
          break;
        case Location.RightRear:
          this.Title = "Right Rear";
          break;
        default:
          this.Title = "Left Front";
          break;
      }
    }

    internal int GetIndexFromLocation(Location location)
    {
      switch(location)
      {
        case Location.RightFront:
          return 1;
        case Location.LeftRear:
          return 2;
        case Location.RightRear:
          return 3;
        default:
          return 0;
      }
    }
  }
}