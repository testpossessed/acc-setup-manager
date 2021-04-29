using System;
using System.IO;
using ACCSetupManager.Models;
using Newtonsoft.Json;

namespace ACCSetupManager.Services
{
  internal static class NotesProvider
  {
    internal static SetupFileNotes GeSetupFileNotes(string setupFileName)
    {
      var notesFilePath = GetNotesFilesPath(setupFileName);

      if(!File.Exists(notesFilePath))
      {
        return null;
      }

      var json = File.ReadAllText(notesFilePath);
      return JsonConvert.DeserializeObject<SetupFileNotes>(json);
    }

    internal static void SaveSetupFileNotes(string setupFileName, SetupFileNotes notes)
    {
      EnsureNotesFolderExists();
      var notesFilePath = GetNotesFilesPath(setupFileName);
      var json = JsonConvert.SerializeObject(notes);

      File.WriteAllText(notesFilePath, json);
    }

    private static void EnsureNotesFolderExists()
    {
      if(!Directory.Exists(PathProvider.NotesFolderPath))
      {
        Directory.CreateDirectory(PathProvider.NotesFolderPath);
      }
    }

    private static string GetNotesFilesPath(string setupFileName)
    {
      var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(setupFileName);
      return Path.Combine(PathProvider.NotesFolderPath, $"{fileNameWithoutExtension}.json");
    }
  }
}