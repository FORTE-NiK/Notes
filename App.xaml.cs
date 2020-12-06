using ForteNotes.Model;
using System;
using System.IO;
using System.Windows;

namespace ForteNotes
{
    public partial class App : Application
	{
		public App()
		{
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;
			string pathNotes = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "save.notes");
			string pathSettings = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "saveSettings.notes");

            if (File.Exists(pathSettings))
				MainModel.RestoreSettings();
			else
				MainModel.SaveSettings();

			if (File.Exists(pathNotes))
			{
				NoteModel noteModel = new NoteModel();
				noteModel.RestoreNotes();
			}
			RegistryAdd.AddToAutorun();
		}
	}
}
