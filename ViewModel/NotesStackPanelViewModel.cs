using ForteNotes.Model;
using ForteNotes.View.MainWindow;
using ForteNotes.View.Notes;

namespace ForteNotes.ViewModel
{
    public class NotesStackPanelViewModel : ViewModelBase
    {
        private readonly NotesStackPanel notesStackPanel;

        public NotesStackPanelViewModel(NotesStackPanel notesStackPanel)
        {
            this.notesStackPanel = notesStackPanel;
            NoteModel noteModel = new NoteModel();
            foreach (var note in NoteModel.NoteData)
            {
                NoteTemplateViewModel noteTemplate = new NoteTemplateViewModel(note.Id, note.NoteText, note.DockColorBlack, this);
                notesStackPanel.NotesList.Children.Add(noteTemplate.NoteTemplate);
            }
        }
        internal void RemoveNote(NoteTemplate noteTemplate)
        {
            notesStackPanel.NotesList.Children.Remove(noteTemplate);
        }
    }   
}
