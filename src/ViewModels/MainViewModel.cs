using System;
using System.Collections.ObjectModel;
using ACCSetupManager.Abstractions;
using ACCSetupManager.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
  public class MainViewModel : ObservableObject
  {
    private readonly IHierarchyBuilder hierarchyBuilder;
    private readonly IMasterSetupSync masterSetupSync;
    private readonly ISetupFileProvider setupFileProvider;
    private string statusMessage = "Started";

    public MainViewModel(IMasterSetupSync masterSetupSync,
      IHierarchyBuilder hierarchyBuilder,
      ISetupFileProvider setupFileProvider)
    {
      this.masterSetupSync = masterSetupSync;
      this.hierarchyBuilder = hierarchyBuilder;
      this.setupFileProvider = setupFileProvider;

      this.SyncMasterSetups();
      this.LoadTreeData();
    }

    public ObservableCollection<Vehicle> MasterTreeHierarchy { get; private set; }

    public string StatusMessage
    {
      get => this.statusMessage;
      set => this.SetProperty(ref this.statusMessage, value);
    }

    public ObservableCollection<Vehicle> VersionTreeHierarchy { get; private set; }

    private void LoadTreeData()
    {
      this.StatusMessage = "Loading master setups...";
      this.MasterTreeHierarchy =
        new ObservableCollection<Vehicle>(this.hierarchyBuilder.BuildMasterHierarchy());

      this.StatusMessage = "Loading versions...";
      this.VersionTreeHierarchy =
        new ObservableCollection<Vehicle>(this.hierarchyBuilder.BuildVersionsHierarchy());

      this.StatusMessage = "Ready";
    }

    private void SyncMasterSetups()
    {
      this.masterSetupSync.SyncMasters(status => this.StatusMessage = $"{status}...");
    }
  }
}