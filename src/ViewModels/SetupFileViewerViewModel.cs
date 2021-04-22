using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using ACCSetupManager.Messages;
using ACCSetupManager.Models;
using ACCSetupManager.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace ACCSetupManager.ViewModels
{
  public class SetupFileViewerViewModel : ObservableRecipient, IRecipient<SelectedSetupChanged>
  {
    private VersionListItemViewModel selectedVersion;
    private SetupViewModel setup;
    private SetupFile setupFile;
    private SetupFileViewModel setupFileViewModel;

    public SetupFileViewerViewModel()
    {
      this.Versions = new ObservableCollection<VersionListItemViewModel>();
    }

    public ObservableCollection<VersionListItemViewModel> Versions { get; }

    public VersionListItemViewModel SelectedVersion
    {
      get => this.selectedVersion;
      set => this.SetProperty(ref this.selectedVersion, value);
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
    }

    private string GetPrefix()
    {
      var fileName = Path.GetFileNameWithoutExtension(this.setup.FilePath);
      if(!this.setup.IsVersion)
      {
        return fileName;
      }

      var length = fileName!.Length - 20;
      return fileName.Substring(0, length);
    }

    private void LoadSetupFile()
    {
      this.setupFile = SetupFileProvider.GetSetupFile(this.setup.FilePath);
      this.SetupFile = new SetupFileViewModel(this.setupFile);

      var prefix = this.GetPrefix();
      var setupVersions = SetupFileProvider.GetVersions(this.setup.VehicleIdentifier,
        this.setup.CircuitIdentifier,
        prefix);
      this.Versions.Clear();

      if(this.setup.IsVersion)
      {
        this.Versions.Add(new VersionListItemViewModel
                          {
                            FilePath = setupVersions[0]
                              .MasterSetupFilePath,
                            Name = $"{prefix} (Master)"
                          });
      }

      foreach(var setupFileInfo in setupVersions.Where(v => v.FileName != this.setup.Name))
      {
        this.Versions.Add(new VersionListItemViewModel
                          {
                            FilePath = setupFileInfo.VersionSetupFilePath,
                            Name = setupFileInfo.FileName
                          });
      }

      this.SelectedVersion = this.Versions[0];
    }
  }
}