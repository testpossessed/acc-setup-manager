using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public class TyresViewModel : ObservableObject
  {
    public FrontTyreViewModel LeftFront { get; } = new();
    public RearTyreViewModel LeftRear { get; } = new();
    public FrontTyreViewModel RightFront { get; } = new();
    public RearTyreViewModel RightRear { get; } = new();
  }
}