using System;
using ACCSetupManager.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public class SetupFileViewModel : ObservableObject
  {
    private readonly SetupFile setupFile;

    public SetupFileViewModel(SetupFile setupFile)
    {
      this.setupFile = setupFile;
      this.MapSetupFile();
    }

    private void MapSetupFile()
    {
      
    }

    public TyresViewModel Tyres { get; } = new();
  }
}