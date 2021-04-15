using System;
using System.Collections.ObjectModel;
using System.IO;
using ACCSetupManager.Abstractions;
using ACCSetupManager.Models;
using ACCSetupManager.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public class MainViewModel : ObservableObject
  {
    private readonly IHierarchyProvider hierarchyProvider;
    private readonly IMasterSetupSync masterSetupSync;
    private readonly ISetupFileProvider setupFileProvider;
    private FileSystemWatcher fileWatcher;
    private string statusMessage = "Started";

    public MainViewModel(IMasterSetupSync masterSetupSync,
      IHierarchyProvider hierarchyProvider,
      ISetupFileProvider setupFileProvider)
    {
      this.masterSetupSync = masterSetupSync;
      this.hierarchyProvider = hierarchyProvider;
      this.setupFileProvider = setupFileProvider;

      this.SyncMasterSetups();
      this.LoadTreeData();
      this.InitialiseFileWatcher();
    }

    public ObservableCollection<VehicleViewModel> MasterTreeHierarchy { get; private set; }

    public string StatusMessage
    {
      get => this.statusMessage;
      set => this.SetProperty(ref this.statusMessage, value);
    }

    public ObservableCollection<VehicleViewModel> VersionTreeHierarchy { get; private set; }

    private void AddSetupFileToHierarchies(SetupFileInfo setupFileInfo)
    {
      this.hierarchyProvider.AddMasterSetupFileToHierarchy(setupFileInfo, this.MasterTreeHierarchy);
      this.hierarchyProvider.AddVersionedSetupFileToHierarchy(setupFileInfo, this.VersionTreeHierarchy);
    }

    private void HandleSetupFileChanged(object sender, FileSystemEventArgs eventArgs)
    {
      var setupFileInfo = this.ProcessFileCreateOrChangeEvent(eventArgs);
      this.hierarchyProvider.AddVersionedSetupFileToHierarchy(setupFileInfo, this.VersionTreeHierarchy);
    }

    private void HandleSetupFileCreated(object sender, FileSystemEventArgs eventArgs)
    {
      var setupFileInfo = this.ProcessFileCreateOrChangeEvent(eventArgs);
      this.AddSetupFileToHierarchies(setupFileInfo);
    }

    private void HandleSetupFileDeleted(object sender, FileSystemEventArgs eventArgs) { }

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
        new ObservableCollection<VehicleViewModel>(this.hierarchyProvider.BuildMasterHierarchy());

      this.StatusMessage = "Loading versions...";
      this.VersionTreeHierarchy =
        new ObservableCollection<VehicleViewModel>(this.hierarchyProvider.BuildVersionsHierarchy());

      this.StatusMessage = "Ready";
    }

    private SetupFileInfo ProcessFileCreateOrChangeEvent(FileSystemEventArgs eventArgs)
    {
      var setupFileInfo = this.setupFileProvider.ParseFilePath(eventArgs.FullPath);

      File.Copy(eventArgs.FullPath, setupFileInfo.MasterSetupFilePath, true);
      setupFileInfo.VersionSetupFilePath = this.setupFileProvider.CreateNewVersionFromSource(
        setupFileInfo.VehicleIdentifier,
        setupFileInfo.CircuitIdentifier,
        setupFileInfo.MasterSetupFilePath);

      return setupFileInfo;
    }

    private void SyncMasterSetups()
    {
      this.masterSetupSync.SyncMasters(status => this.StatusMessage = $"{status}...");
    }
  }
}