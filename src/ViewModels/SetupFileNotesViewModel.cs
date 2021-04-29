using System;
using System.IO;
using System.Windows.Input;
using ACCSetupManager.Models;
using ACCSetupManager.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace ACCSetupManager.ViewModels
{
  public class SetupFileNotesViewModel : ObservableObject
  {
    private string comments;
    private string fileName;
    public LapTimeViewModel LapTimeAfter { get; } = new();
    public LapTimeViewModel LapTimeBefore { get; } = new();
    private string reasonForChange;

    public SetupFileNotesViewModel()
    {
      this.Save = new RelayCommand(this.HandleSaveCommand);
    }

    public string Comments
    {
      get => this.comments;
      set => this.SetProperty(ref this.comments, value);
    }

    public string ReasonForChange
    {
      get => this.reasonForChange;
      set => this.SetProperty(ref this.reasonForChange, value);
    }

    public ICommand Save { get; set; }

    public void Load(string setupFilePath)
    {
      this.fileName = Path.GetFileName(setupFilePath);
      var notes = NotesProvider.GeSetupFileNotes(this.fileName);

      if(notes == null)
      {
        this.Comments = null;
        this.LapTimeBefore.Minutes = 0;
        this.LapTimeBefore.Seconds = 0;
        this.LapTimeAfter.Minutes = 0;
        this.LapTimeAfter.Seconds = 0;
        this.ReasonForChange = null;
        return;
      }

      this.Comments = notes.Comments;
      this.LapTimeBefore.Minutes = notes.LapTimeBefore.Minutes;
      this.LapTimeBefore.Seconds = notes.LapTimeBefore.Seconds;
      this.LapTimeAfter.Minutes = notes.LapTimeAfter.Minutes;
      this.LapTimeAfter.Seconds = notes.LapTimeAfter.Seconds;
      this.ReasonForChange = notes.ReasonForChange;
    }

    private void HandleSaveCommand()
    {
      var notes = new SetupFileNotes
                  {
                    Comments = this.Comments,
                    LapTimeAfter = new LapTime(this.LapTimeAfter.Minutes, this.LapTimeAfter.Seconds),
                    LapTimeBefore = new LapTime(this.LapTimeBefore.Minutes, this.LapTimeBefore.Seconds),
                    ReasonForChange = this.ReasonForChange
                  };

      NotesProvider.SaveSetupFileNotes(this.fileName, notes);
    }
  }
}