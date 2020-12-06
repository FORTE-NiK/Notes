
using ForteNotes.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace ForteNotes.Model
{
	[Serializable]
	public class NoteModel
	{
		public NoteModel()
        {
			
        }

        private static Dictionary<string, INote> notes = new Dictionary<string, INote>();

        private static NoteDataModel[] noteData;
        public static NoteDataModel[] NoteData
        {
            get
            {
				noteData=new NoteDataModel[notes.Count];
                int counter = 0;
                foreach (var note in notes)
                {
                    noteData[counter]= new NoteDataModel(){Id=note.Key, DockColorBlack = note.Value.DockColorBlack, NoteText = note.Value.NoteText};
                    counter++;
                }

                return noteData;
            }
        }


        internal void CreateNote(NoteType type)
        {
			string id = Guid.NewGuid().ToString();
			switch (type)
			{
				case NoteType.Simple:
					try
					{
						notes.Add(id, new SimpleNoteViewModel(id));
					}
					catch
					{
						id = Guid.NewGuid().ToString();
						notes.Add(id, new SimpleNoteViewModel(id));
					}
					break;
				case NoteType.Extended:
					try
					{
						notes.Add(id, new NoteViewModel(id));
					}
					catch
					{
						id = Guid.NewGuid().ToString();
						notes.Add(id, new NoteViewModel(id));
					}
					break;
			}
			
			
        }

		internal static void DeleteNote(string id)
        {
			notes[id].CloseNote();
			notes.Remove(id);
        }

		internal void ShowNote(string id)
        {
			notes[id].ShowExitingNote();
        }

		internal void SaveNotes()
        {
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "save.notes");
			using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(stream, notes);
            }
        }

		internal void RestoreNotes()
        {
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "save.notes");
			using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				notes =binaryFormatter.Deserialize(stream) as Dictionary<string,INote>;
				foreach (var note in notes)
                {
					note.Value.RestoreWindowAfterLaunch();
                }
            }
        }

		internal static bool ContainNotes()
        {
            if (notes.Count == 0)
                return false;
            else
                return true;
        }

		internal void NotesOverCheck()
		{
			foreach (var i in notes)
			{
				if (i.Value.Opened|| MainWindowViewModel.MWVisibility==Visibility.Visible)
					return;
            }
            Application.Current.Shutdown();
		}

        internal static bool ContainOpenedNotes()
        {
            foreach (var i in notes)
            {
                if (i.Value.Opened)
                    return false;
            }
            return true;
        }

        public static void ChangeTheme(Theme theme)
        {
            foreach (var i in notes)
            {
                i.Value.SetTheme(theme);
            }
        }
	}
}
