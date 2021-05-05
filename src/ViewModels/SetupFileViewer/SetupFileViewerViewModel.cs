using System;
using ACCSetupManager.Messages;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace ACCSetupManager.ViewModels.SetupFileViewer
{
  public class SetupFileViewerViewModel : ObservableRecipient, IRecipient<SelectedSetupChanged>
  {
    private bool hasSetupFile;
    private SetupViewModel setup;
    private SetupFileViewModel setupFileViewModel;

    public SetupFileViewerViewModel()
    {
      this.IsActive = true;
      this.HasSetupFile = false;
    }

    public SetupFileNotesViewModel Notes { get; } = new();

    public bool HasSetupFile
    {
      get => this.hasSetupFile;
      set => this.SetProperty(ref this.hasSetupFile, value);
    }

    public SetupFileViewModel SetupFile
    {
      get => this.setupFileViewModel;
      set => this.SetProperty(ref this.setupFileViewModel, value);
    }

    public void Receive(SelectedSetupChanged message)
    {
      this.setup = message.Setup;
      this.LoadSetupFile();
      this.Notes.Load(this.setup.FilePath);
    }

    private void LoadSetupFile()
    {
      this.SetupFile = new SetupFileViewModel(this.setup.FilePath);
      this.HasSetupFile = true;
    }
  }
}