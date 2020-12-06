using ForteNotes.Model;
using ForteNotes.View.Notes;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ForteNotes.ViewModel
{
	[Serializable]
	public class SimpleNoteViewModel : ViewModelBase, INote
	{
		[NonSerialized]
		private SimpleNoteWindowBlack simpleNoteWindowBlack;
		[NonSerialized]
		private SimpleNoteWindowWhite simpleNoteWindowWhite;

		private NoteModel noteModel { get; set; }
		private string id;
		[NonSerialized]
		private static bool _saving = false;

		public NoteType NoteType { get; set; } = NoteType.Simple;
		public SimpleNoteViewModel(string id)
		{
			this.id = id;
			noteModel = new NoteModel();
			ShowExitingNote();
			if (!_saving)
			{
				SaveNoteAsync();
			}
		}
		private bool opened = false;
		public bool Opened => opened;

		private CommandCL createNote;
		public CommandCL CreateNote
		{
			get
			{
				return createNote ??
					(createNote = new CommandCL(e =>
					{
						noteModel.CreateNote(NoteType.Simple);
						MainWindowViewModel.UpdateNotes();
					}));
			}
		}

		private CommandCL closeNoteButton;
		public CommandCL CloseNoteButton
        {
            get
            {
                return closeNoteButton ??
                    (closeNoteButton = new CommandCL(e =>
                    {
						if(string.IsNullOrEmpty(noteText))
                            NoteModel.DeleteNote(id);
                        else
                         CloseNote();
					}));
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
						if (!_saving)
						{
							SaveNoteAsync();
						}
						noteModel.NotesOverCheck();
					}));
			}
		}

		private CommandCL showMainWindow;
		public CommandCL ShowMainWindow
		{
			get
			{
				return showMainWindow ??
					(showMainWindow = new CommandCL(e =>
					{
						MainWindowViewModel.ShowMainWindow();
					}));
			}
		}

		private CommandCL changeDockColor;
		public CommandCL ChangeDockColor
		{
			get
			{
				return changeDockColor ??
					(changeDockColor = new CommandCL(e =>
					{
						switch (e)
						{
							case "Yellow":
								whiteThemeDockColor = "#FFF2AB";
								whiteThemeTextBoxColor = "#FFF7D1";
								textColorWhite = "#333333";

								dockColorBlack = "#E6B905";
								break;
							case "Green":
								whiteThemeDockColor = "#CBF1C4";
								whiteThemeTextBoxColor = "#E4F9E0";
								textColorWhite = "#333333";

								dockColorBlack = "#6FD262";
								break;
							case "Pink":
								whiteThemeDockColor = "#FFCCE5";
								whiteThemeTextBoxColor = "#FFE4F1";
								textColorWhite = "#333333";

								dockColorBlack = "#EA86C2";
								break;
							case "Violet":
								whiteThemeDockColor = "#E7CFFF";
								whiteThemeTextBoxColor = "#F2E6FF";
								textColorWhite = "#333333";

								dockColorBlack = "#C78EFF";
								break;
							case "Blue":
								whiteThemeDockColor = "#CDE9FF";
								whiteThemeTextBoxColor = "#E2F1FF";
								textColorWhite = "#333333";

								dockColorBlack = "#5AC0E7";
								break;
							case "GrayLight":
								whiteThemeDockColor = "#E1DFDD";
								whiteThemeTextBoxColor = "#F3F2F1";
								textColorWhite = "#333333";

								dockColorBlack = "#898989";
								break;
							case "Gray":
								whiteThemeDockColor = "#494745";
								whiteThemeTextBoxColor = "#696969";
								textColorWhite = "#FFFFFF";

								dockColorBlack = "#505050";
								break;
						}
						if (!_saving)
						{
							SaveNoteAsync();
						}
					}));
			}
		}


		public string BorderColor =>
			"#" + SystemParameters.WindowGlassColor.R.ToString("X2") +
			SystemParameters.WindowGlassColor.G.ToString("X2") +
			SystemParameters.WindowGlassColor.B.ToString("X2");

		private string dockColorBlack = "#E6B905";
		public string DockColorBlack
		{
			get => dockColorBlack;
			set
			{
				dockColorBlack = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}
		

		private string textColorWhite = "#333333";
		public string TextColorWhite
		{
			get => textColorWhite;
			set
			{
				textColorWhite = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string whiteThemeDockColor = "#FFF2AB";
		public string WhiteThemeDockColor
		{
			get => whiteThemeDockColor;
			set
			{
				whiteThemeDockColor = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string whiteThemeTextBoxColor = "#FFF7D1";
		public string WhiteThemeTextBoxColor
		{
			get => whiteThemeTextBoxColor;
			set
			{
				whiteThemeTextBoxColor = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string noteText;
		public string NoteText
		{
			get => noteText;
			set
			{
				noteText = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private int height = 260;
		public int Height
		{
			get => height;
			set
			{
				height = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}


		private int width = 196;
		public int Width
		{
			get => width;
			set
			{
				width = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}


		private int windowX = 100;
		public int WindowX
		{
			get => windowX;
			set
			{
				windowX = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}


		private int windowY = 100;
		public int WindowY
		{
			get => windowY;
			set
			{
				windowY = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		public void RestoreWindowAfterLaunch()
		{
			if (opened)
			{
				NoteCreator();
				opened = true;
			}
		}

		public async void CloseNote()
		{
			if (simpleNoteWindowBlack != null || simpleNoteWindowWhite != null)
			{
				await Task.Delay(1000);
				opened = false;
				if (!_saving)
				{
					SaveNoteAsync();
				}

				if (Theme.White == MainModel.CurrentTheme)
				{
					simpleNoteWindowWhite.Close();
					simpleNoteWindowWhite = null;
				}
				else
				{
					simpleNoteWindowBlack.Close();
					simpleNoteWindowBlack = null;
				}
				noteModel.NotesOverCheck();
			}
		}

		public void ShowExitingNote()
		{
			if (simpleNoteWindowBlack == null && simpleNoteWindowWhite == null)
			{
				NoteCreator();
			}
		}

		private async void SaveNoteAsync()
		{
			_saving = true;
			await Task.Delay(20000);
			noteModel.SaveNotes();
			_saving = false;
		}

		public void SetTheme(Theme theme)
		{
			switch (theme)
			{
				case Theme.White:
					if (opened)
						simpleNoteWindowBlack.Close();
					else
					    break;
					simpleNoteWindowBlack = null;
					ShowExitingNote();
					break;
				case Theme.Black:
					if (opened)
						simpleNoteWindowWhite.Close();
					else
					    break;
					simpleNoteWindowWhite = null;
					ShowExitingNote();
					break;
			}
		}

		private void NoteCreator()
		{
			if (Theme.White == MainModel.CurrentTheme)
			{
				this.simpleNoteWindowWhite = new SimpleNoteWindowWhite(this);
				simpleNoteWindowWhite.Note.TextChanged += (o, s) =>
				{
					if (simpleNoteWindowWhite != null)
						noteText = simpleNoteWindowWhite.Note.Text;
				};
				simpleNoteWindowWhite.StateChanged += (o, s) => 
				{
					if (simpleNoteWindowWhite.WindowState == (WindowState)2 || simpleNoteWindowWhite.WindowState == (WindowState)1)
					{
						simpleNoteWindowWhite.WindowState = (WindowState)0;
					}
				};
				simpleNoteWindowWhite.Show();
				opened = true;
			}
			else
			{
				this.simpleNoteWindowBlack = new SimpleNoteWindowBlack(this);
				simpleNoteWindowBlack.Note.TextChanged += (o, s) =>
				{
					if(simpleNoteWindowBlack!=null)
						noteText = simpleNoteWindowBlack.Note.Text;
				};
				simpleNoteWindowBlack.StateChanged += (o, s) => 
				{
					if (simpleNoteWindowBlack.WindowState == (WindowState)2 || simpleNoteWindowBlack.WindowState == (WindowState)1)
					{
						simpleNoteWindowBlack.WindowState = (WindowState)0;
					}
				};
				simpleNoteWindowBlack.Show();
				opened = true;
			}
		}
	}
}
