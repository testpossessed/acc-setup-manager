using System;
using ACCSetupManager.Messages;
using ACCSetupManager.Models;
using ACCSetupManager.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace ACCSetupManager.ViewModels
{
  public class SetupFileComparisonViewerViewModel : ObservableRecipient,
    IRecipient<SelectedVersionChanged>
  {
    private bool hasSetupFile;
    private SetupFileNotesViewModel notes;
    private SetupFile setup;
    private SetupFileComparisonViewModel setupFile;

    public SetupFileComparisonViewerViewModel()
    {
      this.IsActive = true;
    }

    public bool HasSetupFile
    {
      get => this.hasSetupFile;
      set => this.SetProperty(ref this.hasSetupFile, value);
    }

    public SetupFileNotesViewModel Notes
    {
      get => this.notes;
      set => this.SetProperty(ref this.notes, value);
    }

    public SetupFileComparisonViewModel SetupFile
    {
      get => this.setupFile;
      set => this.SetProperty(ref this.setupFile, value);
    }

    public void Receive(SelectedVersionChanged message)
    {
      this.setup = SetupFileProvider.GetSetupFile(message.VersionToCompare.FilePath);
      this.SetupFile =
        new SetupFileComparisonViewModel(message.VersionToCompare.FilePath, message.SetupFile);
      this.HasSetupFile = true;
    }
  }
}