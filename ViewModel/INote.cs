using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForteNotes.ViewModel
{
    interface INote
    {
        void RestoreWindowAfterLaunch();
        void CloseNote();
        void ShowExitingNote();
        void SetTheme(Theme theme);
        bool Opened { get; }
        string NoteText { get; set; }
        string DockColorBlack { get; set; }
        NoteType NoteType { get; set; }
    }
}
