using ForteNotes.Model;
using ForteNotes.Notes;
using ForteNotes.View.MainWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.ComponentModel;
using System.Dynamic;
using System.Threading;
using ForteNotes.View.MainWindow.MainButtons;

namespace ForteNotes.ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
		static MainWindow _mainWindow;
		public MainWindowViewModel(MainWindow mainWindow)
		{
			_mainWindow = mainWindow;
			_mainWindow.StateChanged += (o, e) =>
			{
				if (_mainWindow.WindowState == (WindowState)2 || _mainWindow.WindowState == (WindowState)1)
				{
					_mainWindow.WindowState = (WindowState)0;
				}
			};
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "save.notes");
			if (NoteModel.ContainNotes()&&File.Exists(path))
			{
				_mainWindow.Hide();
			}
			UpdateNotes();
		}


		private CommandCL closeWindow;
		public CommandCL CloseWindow
		{
			get
			{
				return closeWindow ??
					(closeWindow = new CommandCL(e => 
					{
						_mainWindow.Close();
						if(NoteModel.ContainOpenedNotes())
							Application.Current.Shutdown();
					}));
			}
		}
		

		private CommandCL createNote;
		public CommandCL CreateNote
		{
			get
			{
				return createNote ??
					(createNote = new CommandCL(e =>
					{
						NoteModel noteModel = new NoteModel();
						noteModel.CreateNote(NoteType.Extended);
						UpdateNotes();
					}));
			}
		}

		private CommandCL createSimpleNote;
		public CommandCL CreateSimpleNote
		{
			get
			{
				return createSimpleNote ??
					(createSimpleNote = new CommandCL(e =>
					{
						NoteModel noteModel = new NoteModel();
						noteModel.CreateNote(NoteType.Simple);
						UpdateNotes();
					}));
			}
		}

		private CommandCL settingsButton;
		public CommandCL SettingsButton
		{
			get
			{
				return settingsButton ??
					   (settingsButton = new CommandCL(async e =>
					   {
						   await Task.Delay(200);
						   _mainWindow.UpdateGridView();

						   _mainWindow.SettingsButton.Visibility = Visibility.Hidden;
						   _mainWindow.AddNoteButton.Visibility = Visibility.Hidden;
						   _mainWindow.AddSimpleNoteButton.Visibility = Visibility.Hidden;
						   _mainWindow.BackButton.Visibility = Visibility.Visible;

						   await Task.Delay(100);
						   SettingsViewModel.SetSwitches();
						   _mainWindow.MWSettingsAndNotes.Content= new NotesAppSettings();
					   }));
			}
		}

		private CommandCL backButton;
		public CommandCL BackButton
		{
			get
			{
				return backButton ??
					(backButton = new CommandCL(async e =>
					  {
						  await Task.Delay(200);
						  _mainWindow.UpdateGridView();

						  _mainWindow.SettingsButton.Visibility = Visibility.Visible;
						  _mainWindow.AddNoteButton.Visibility = Visibility.Visible;
						  _mainWindow.AddSimpleNoteButton.Visibility = Visibility.Visible;
						  _mainWindow.BackButton.Visibility = Visibility.Hidden;

						  await Task.Delay(100);
						  _mainWindow.MWSettingsAndNotes.Content = new NotesStackPanel();
					  }));
			}
		}

		public string BorderColor =>
			"#" + SystemParameters.WindowGlassColor.R.ToString("X2") +
			SystemParameters.WindowGlassColor.G.ToString("X2") +
			SystemParameters.WindowGlassColor.B.ToString("X2");

		public static Visibility MWVisibility => _mainWindow.Visibility;

		public static void ShowMainWindow()
		{
			_mainWindow=new MainWindow();
			
            _mainWindow.Show();
            var notesStackPanel = new NotesStackPanel();
			_mainWindow.MWSettingsAndNotes.Content=notesStackPanel;
		}


		public static async void UpdateNotes()
		{
			_mainWindow.UpdateGridView();
			await Task.Delay(300);
			_mainWindow.MWSettingsAndNotes.Content = new NotesStackPanel();
		}

	}
}
