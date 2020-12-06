using ForteNotes.Model;
using ForteNotes.View.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForteNotes.ViewModel
{
	public class NoteTemplateViewModel : ViewModelBase
	{
		private string id;
		NoteTemplate noteTemplate;
		NotesStackPanelViewModel notesStack;
		public NoteTemplateViewModel()
		{
			noteTemplate = new NoteTemplate(this);
		}
		public NoteTemplateViewModel(string id, string text, string dockColor, NotesStackPanelViewModel notesStack)
		{
			this.id = id;
			if(!string.IsNullOrEmpty(text))
				NoteText = text;
			this.notesStack = notesStack;
			this.dockColor = dockColor;
			noteTemplate = new NoteTemplate(this);
		}

		private string noteText="Пусто";
		public string NoteText
		{
			get { return noteText; }
			set
			{
				noteText = value;
				OnPropertyChanged();
			}
		}
		private string dockColor;
		public string DockColor
        {
            get { return dockColor; }
            set
            {
				dockColor = value;
				OnPropertyChanged();
            }
        }


		private CommandCL deleteNote;
		public CommandCL DeleteNote
		{
			get
			{
				return deleteNote ??
					(deleteNote = new CommandCL(e => 
					{
                        NoteModel.DeleteNote(id);
						MainWindowViewModel.UpdateNotes();
						notesStack.RemoveNote(noteTemplate);
					}));
			}
		}


		private CommandCL expandNote;
		public CommandCL ExpandNote
		{
			get
			{
				return expandNote ??
					(expandNote = new CommandCL(e =>
					{
						NoteModel noteModel = new NoteModel();
						noteModel.ShowNote(id);
					}));
			}
		}

		public NoteTemplate NoteTemplate => noteTemplate;
    }
}
