using System;
using System.Windows;
using ACCSetupManager.Messages;
using ACCSetupManager.Models;
using ACCSetupManager.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace ACCSetupManager.ViewModels.SetupFileComparisonViewer
{
  public class SetupFileComparisonViewerViewModel : ObservableRecipient,
    IRecipient<SelectedVersionChanged>
  {
    private Visibility aeroDifferenceIndicatorVisibility;
    private Visibility dampersDifferenceIndicatorVisibility;
    private Visibility electronicsDifferenceIndicatorVisibility;
    private Visibility fuelAndStrategyDifferenceIndicatorVisibility;
    private bool hasSetupFile;
    private Visibility mechanicalGripDifferenceIndicatorVisibility;
    private SetupFileNotesViewModel notes;
    private SetupFile setup;
    private SetupFileComparisonViewModel setupFile;
    private Visibility tyresDifferenceIndicatorVisibility;

    public SetupFileComparisonViewerViewModel()
    {
      this.IsActive = true;
    }

    public Visibility AeroDifferenceIndicatorVisibility
    {
      get => this.aeroDifferenceIndicatorVisibility;
      set => this.SetProperty(ref this.aeroDifferenceIndicatorVisibility, value);
    }

    public Visibility DampersDifferenceIndicatorVisibility
    {
      get => this.dampersDifferenceIndicatorVisibility;
      set => this.SetProperty(ref this.dampersDifferenceIndicatorVisibility, value);
    }

    public Visibility ElectronicsDifferenceIndicatorVisibility
    {
      get => this.electronicsDifferenceIndicatorVisibility;
      set => this.SetProperty(ref this.electronicsDifferenceIndicatorVisibility, value);
    }

    public Visibility FuelAndStrategyDifferenceIndicatorVisibility
    {
      get => this.fuelAndStrategyDifferenceIndicatorVisibility;
      set => this.SetProperty(ref this.fuelAndStrategyDifferenceIndicatorVisibility, value);
    }

    public bool HasSetupFile
    {
      get => this.hasSetupFile;
      set => this.SetProperty(ref this.hasSetupFile, value);
    }

    public Visibility MechanicalGripDifferenceIndicatorVisibility
    {
      get => this.mechanicalGripDifferenceIndicatorVisibility;
      set => this.SetProperty(ref this.mechanicalGripDifferenceIndicatorVisibility, value);
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

    public Visibility TyresDifferenceIndicatorVisibility
    {
      get => this.tyresDifferenceIndicatorVisibility;
      set => this.SetProperty(ref this.tyresDifferenceIndicatorVisibility, value);
    }

    public void Receive(SelectedVersionChanged message)
    {
      this.setup = SetupFileProvider.GetSetupFile(message.VersionToCompare.FilePath);
      this.SetupFile =
        new SetupFileComparisonViewModel(message.VersionToCompare.FilePath, message.SetupFile);
      this.HasSetupFile = true;
      this.TyresDifferenceIndicatorVisibility =
        this.SetupFile.Tyres.HasDifferences()? Visibility.Visible: Visibility.Hidden;
      this.ElectronicsDifferenceIndicatorVisibility =
        this.SetupFile.Electronics.HasDifferences()? Visibility.Visible: Visibility.Hidden;
      this.FuelAndStrategyDifferenceIndicatorVisibility =
        this.SetupFile.FuelAndStrategy.HasDifferences()? Visibility.Visible: Visibility.Hidden;
      this.MechanicalGripDifferenceIndicatorVisibility =
        this.SetupFile.MechanicalGrip.HasDifferences()? Visibility.Visible: Visibility.Hidden;
      this.DampersDifferenceIndicatorVisibility = this.SetupFile.Dampers.HasDifferences()
                                                    ? Visibility.Visible
                                                    : Visibility.Hidden;
      this.AeroDifferenceIndicatorVisibility = this.SetupFile.Aero.HasDifferences()
                                                 ? Visibility.Visible
                                                 : Visibility.Hidden;
    }
  }
}