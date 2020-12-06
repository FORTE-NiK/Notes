using ForteNotes.Model;
using ForteNotes.Notes;
using ForteNotes.Properties;
using ForteNotes.View.MainWindow;
using ForteNotes.View.MainWindow.MainButtons;
using ForteNotes.View.Notes;
using ForteNotes.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Shell;

namespace ForteNotes
{
    public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = new MainWindowViewModel(this);
			WindowChrome windowChrome = new WindowChrome();
            windowChrome.CaptionHeight = 0;
            WindowChrome.SetWindowChrome(this, windowChrome);

		}
		public static readonly RoutedEvent NotesUpdateEvent = EventManager.RegisterRoutedEvent("notesUpdate", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MainWindow));
		public event RoutedEventHandler notesUpdate
		{
			add => AddHandler(NotesUpdateEvent, value);
            remove => RemoveHandler(NotesUpdateEvent, value);
        }

		[DebuggerHidden]
		private void MoveWindow(object sender, MouseEventArgs e)
		{
			if (Mouse.LeftButton == MouseButtonState.Pressed)
				this.DragMove();
		}

        internal void UpdateGridView()
        {
			MWSettingsAndNotes.RaiseEvent(new RoutedEventArgs(NotesUpdateEvent, this));
		}
    }   
}
