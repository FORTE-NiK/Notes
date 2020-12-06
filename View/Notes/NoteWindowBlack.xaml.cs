using System;
using System.Collections.Generic;
using ForteNotes;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Threading;
using ForteNotes.ViewModel;
using ForteNotes.Model;
using System.Diagnostics;
using System.Windows.Shell;
using ForteNotes.View.Notes;

namespace ForteNotes.Notes
{

	
	public partial class NoteWindowBlack : Window ,  IDisposable
	{
		public int NoteId { get; set; }
        public NoteWindowBlack(NoteViewModel noteViewModel)
		{
			InitializeComponent();
			this.DataContext = noteViewModel;
            WindowChrome windowChrome = new WindowChrome();
            windowChrome.CaptionHeight = 0;
            WindowChrome.SetWindowChrome(this, windowChrome);
        }
		[DebuggerHidden]
		private void MoveNote(object sender, MouseEventArgs e)
		{
			if (Mouse.LeftButton == MouseButtonState.Pressed)
				this.DragMove();
		}
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}

}
