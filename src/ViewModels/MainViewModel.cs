using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using ACCSetupManager.Messages;
using ACCSetupManager.Models;
using ACCSetupManager.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace ACCSetupManager.ViewModels
{
  public class MainViewModel : ObservableRecipient
  {
    private SetupViewModel currentSetup;
    private FileSystemWatcher fileWatcher;
    private string statusMessage = "Started";

    public MainViewModel()
    {
      this.ShowSetup = new RelayCommand<SetupViewModel>(this.HandleShowSetup);
      this.Setup = new SetupFileViewerViewModel
                   {
                     IsActive = true
                   };

      this.SyncMasterSetups();
      this.LoadTreeData();
      this.InitialiseFileWatcher();
    }

    public SetupFileViewerViewModel Setup { get; }

    public ICommand ShowSetup { get; }

    public ObservableCollection<VehicleViewModel> MasterTreeHierarchy { get; private set; }

    public string StatusMessage
    {
      get => this.statusMessage;
      set => this.SetProperty(ref this.statusMessage, value);
    }

    public ObservableCollection<VehicleViewModel> VersionTreeHierarchy { get; private set; }

    private void AddSetupFileToHierarchies(SetupFileInfo setupFileInfo)
    {
      HierarchyProvider.AddMasterSetupFileToHierarchy(setupFileInfo, this.MasterTreeHierarchy);
      HierarchyProvider.AddVersionedSetupFileToHierarchy(setupFileInfo, this.VersionTreeHierarchy);
    }

    private void HandleSetupFileChanged(object sender, FileSystemEventArgs eventArgs)
    {
      var setupFileInfo = this.ProcessFileCreateOrChangeEvent(eventArgs);
      HierarchyProvider.AddVersionedSetupFileToHierarchy(setupFileInfo, this.VersionTreeHierarchy);
    }

    private void HandleSetupFileCreated(object sender, FileSystemEventArgs eventArgs)
    {
      var setupFileInfo = this.ProcessFileCreateOrChangeEvent(eventArgs);
      this.AddSetupFileToHierarchies(setupFileInfo);
    }

    private void HandleSetupFileDeleted(object sender, FileSystemEventArgs eventArgs)
    {
      var setupFileInfo = SetupFileProvider.ParseFilePath(eventArgs.FullPath);
      if(File.Exists(setupFileInfo.MasterSetupFilePath))
      {
        File.Delete(setupFileInfo.MasterSetupFilePath);
      }

      HierarchyProvider.DeleteSetupFromHierarchy(setupFileInfo, this.MasterTreeHierarchy);
    }

    private void HandleShowSetup(SetupViewModel setup)
    {
      if(this.currentSetup != null)
      {
        this.currentSetup.IsSelected = false;
      }

      setup.IsSelected = true;
      this.currentSetup = setup;
      this.Messenger.Send(new SelectedSetupChanged(setup));
    }

    private void InitialiseFileWatcher()
    {
      this.fileWatcher = new FileSystemWatcher(PathProvider.AccSetupsFolderPath, "*.json")
                         {
                           IncludeSubdirectories = true,
                           EnableRaisingEvents = true
                         };
      this.fileWatcher.Created += this.HandleSetupFileCreated;
      this.fileWatcher.Deleted += this.HandleSetupFileDeleted;
      this.fileWatcher.Changed += this.HandleSetupFileChanged;
    }

    private void LoadTreeData()
    {
      this.StatusMessage = "Loading master setups...";
      this.MasterTreeHierarchy =
        new ObservableCollection<VehicleViewModel>(HierarchyProvider.BuildMasterHierarchy());

      this.StatusMessage = "Loading versions...";
      this.VersionTreeHierarchy =
        new ObservableCollection<VehicleViewModel>(HierarchyProvider.BuildVersionsHierarchy());

      this.StatusMessage = "Ready";
    }

    private SetupFileInfo ProcessFileCreateOrChangeEvent(FileSystemEventArgs eventArgs)
    {
      var setupFileInfo = SetupFileProvider.ParseFilePath(eventArgs.FullPath);

      File.Copy(eventArgs.FullPath, setupFileInfo.MasterSetupFilePath, true);
      setupFileInfo.VersionSetupFilePath = SetupFileProvider.CreateNewVersionFromSource(
        setupFileInfo.VehicleIdentifier,
        setupFileInfo.CircuitIdentifier,
        setupFileInfo.MasterSetupFilePath);

      return setupFileInfo;
    }

    private void SyncMasterSetups()
    {
      MasterSetupSync.SyncMasters(status => this.StatusMessage = $"{status}...");
    }
  }
}