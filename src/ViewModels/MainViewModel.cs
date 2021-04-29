using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ACCSetupManager.Messages;
using ACCSetupManager.Models;
using ACCSetupManager.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Syncfusion.SfSkinManager;

namespace ACCSetupManager.ViewModels
{
  public class MainViewModel : ObservableRecipient
  {
    private Visibility comparisonSetupVisibility;
    private SetupViewModel currentSetup;
    private FileSystemWatcher fileWatcher;
    private bool hasSetupFile;
    private bool isCompareEnabled;

    private string selectedTheme;
    private VersionListItemViewModel selectedVersion;
    private string statusMessage = "Started";

    public MainViewModel()
    {
      this.Versions = new ObservableCollection<VersionListItemViewModel>();
      this.ShowSetup = new RelayCommand<SetupViewModel>(this.HandleShowSetup);
      this.Setup = new SetupFileViewerViewModel();
      this.ComparisonSetup = new SetupFileComparisonViewerViewModel();
      this.ComparisonSetupVisibility = Visibility.Hidden;
      this.HasSetupFile = false;

      this.SyncMasterSetups();
      this.LoadTreeData();
      this.InitialiseFileWatcher();
      this.SetInitialTheme();
    }

    public SetupFileComparisonViewerViewModel ComparisonSetup { get; }

    public SetupFileViewerViewModel Setup { get; }

    public ICommand ShowSetup { get; }

    public List<string> Themes { get; } = new()
                                          {
                                            "Blend",
                                            "Material Dark",
                                            "Office 2019 Dark Gray",
                                            "Office 365",
                                            "Saffron"
                                          };

    public ObservableCollection<VersionListItemViewModel> Versions { get; }

    public Visibility ComparisonSetupVisibility
    {
      get => this.comparisonSetupVisibility;
      set => this.SetProperty(ref this.comparisonSetupVisibility, value);
    }

    public bool HasSetupFile
    {
      get => this.hasSetupFile;
      set => this.SetProperty(ref this.hasSetupFile, value);
    }

    public bool IsCompareEnabled
    {
      get => this.isCompareEnabled;
      set
      {
        this.SetProperty(ref this.isCompareEnabled, value);
        this.ComparisonSetupVisibility = value? Visibility.Visible: Visibility.Hidden;
      }
    }

    public ObservableCollection<VehicleViewModel> MasterTreeHierarchy { get; private set; }

    public string SelectedTheme
    {
      get => this.selectedTheme;
      set
      {
        this.SetProperty(ref this.selectedTheme, value);
        this.HandleThemeChanged();
      }
    }

    public VersionListItemViewModel SelectedVersion
    {
      get => this.selectedVersion;
      set
      {
        this.SetProperty(ref this.selectedVersion, value);
        this.HandleSelectedVersionChanged();
      }
    }

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

    private string GetPrefix(SetupViewModel setup)
    {
      var fileName = Path.GetFileNameWithoutExtension(setup.FilePath);
      if(!setup.IsVersion)
      {
        return fileName;
      }

      var length = fileName!.Length - 20;
      return $"{fileName.Substring(0, length)}-";
    }

    private void HandleSelectedVersionChanged()
    {
      this.Messenger.Send(new SelectedVersionChanged(this.Setup.SetupFile, this.SelectedVersion));
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

      var setupVersions = SetupFileProvider.GetVersions(setup.VehicleIdentifier,
        setup.CircuitIdentifier,
        setup.IsVersion);
      var setupMasters =
        SetupFileProvider.GetMasters(setup.VehicleIdentifier, setup.CircuitIdentifier);
      this.Versions.Clear();

      foreach(var setupFileInfo in setupVersions.Concat(setupMasters)
                                                .OrderBy(f => f.FileName)
                                                .Where(v => v.FileName != setup.Name))
      {
        this.Versions.Add(new VersionListItemViewModel
                          {
                            FilePath =
                              setupFileInfo.IsVersion
                                ? setupFileInfo.VersionSetupFilePath
                                : setupFileInfo.MasterSetupFilePath,
                            Name = setupFileInfo.FileName
                          });
      }

      this.SelectedVersion = this.Versions[0];
      this.HasSetupFile = true;
      this.IsCompareEnabled = false;
    }

    private void HandleThemeChanged()
    {
      var theme = this.SelectedTheme.Replace(" ", "");
      SettingsProvider.SaveSettings(new UserSettings
                                    {
                                      Theme = this.SelectedTheme
                                    });
      var result =
        MessageBox.Show(
          $"The theme will be applied when the application next starts.{Environment.NewLine}{Environment.NewLine}Do you want to exit now?",
          "Theme Change",
          MessageBoxButton.YesNo);

      if(result == MessageBoxResult.Yes)
      {
        Application.Current.Shutdown(0);
      }
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

    private void SetInitialTheme()
    {
      var settings = SettingsProvider.GetSettings();
      this.selectedTheme = settings.Theme;
      SfSkinManager.SetTheme(Application.Current.MainWindow, new Theme(this.SelectedTheme.Replace(" ", "")));
    }

    private void SyncMasterSetups()
    {
      MasterSetupSync.SyncMasters(status => this.StatusMessage = $"{status}...");
    }
  }
}