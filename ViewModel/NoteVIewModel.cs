using ForteNotes.Model;
using ForteNotes.Notes;
using System;
using System.Threading.Tasks;
using System.Windows;


namespace ForteNotes.ViewModel
{
	[Serializable]
	public class NoteViewModel : ViewModelBase, INote
	{
		[NonSerialized]
		private NoteWindowWhite noteWindowWhite;
		[NonSerialized]
		private NoteWindowBlack noteWindowBlack;
		
		public NoteType NoteType { get; set; } = NoteType.Extended;
        private NoteWindowWhite NoteWindowWhite
        {
            get
            {
	            if (noteWindowWhite == null)
		            return null;
                if (noteWindowWhite.Visibility == Visibility.Visible)
                    return noteWindowWhite;
                else
                    return null;
            }
            set => noteWindowWhite = value;
        }
        
        private NoteWindowBlack NoteWindowBlack
        {
	        get
	        {
		        if (noteWindowBlack == null)
			        return null;
		        if (noteWindowBlack.Visibility == Visibility.Visible)
			        return noteWindowBlack;
		        else
			        return null;
	        }
	        set => noteWindowBlack = value;
        }

        private NoteModel noteModel { get; set; }
		private string id;
		[NonSerialized]
		private static bool _saving=false;
		public NoteViewModel(string id)
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
						noteModel.CreateNote(NoteType.Extended);
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
                           if (string.IsNullOrEmpty(mondayText) && string.IsNullOrEmpty(tuesdayText) && string.IsNullOrEmpty(wednesdayText) 
                               && string.IsNullOrEmpty(thursdayText) && string.IsNullOrEmpty(fridayText) && string.IsNullOrEmpty(saturdayText)
                               && string.IsNullOrEmpty(sundayText) && string.IsNullOrEmpty(additionalText) && additionalHeaderText=="Еще"
                               && string.IsNullOrEmpty(additionalDate) && string.IsNullOrEmpty(sundayDate) && string.IsNullOrEmpty(saturdayDate)
                               && string.IsNullOrEmpty(fridayDate) && string.IsNullOrEmpty(thursdayDate) && string.IsNullOrEmpty(wednesdayDate)
                               && string.IsNullOrEmpty(tuesdayDate) && string.IsNullOrEmpty(mondayDate))
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
								textColorBlack = "#E6B905";
									break;
							case "Green":
								whiteThemeDockColor = "#CBF1C4";
								whiteThemeTextBoxColor = "#E4F9E0";
								textColorWhite = "#333333";

								dockColorBlack = "#6FD262";
								textColorBlack = "#6FD262";
									break;
							case "Pink":
								whiteThemeDockColor = "#FFCCE5";
								whiteThemeTextBoxColor = "#FFE4F1";
								textColorWhite = "#333333";

								dockColorBlack = "#EA86C2";
								textColorBlack = "#EA86C2";
									break;
							case "Violet":
								whiteThemeDockColor = "#E7CFFF";
								whiteThemeTextBoxColor = "#F2E6FF";
								textColorWhite = "#333333";

								dockColorBlack = "#C78EFF";
								textColorBlack = "#C78EFF";
									break;
							case "Blue":
								whiteThemeDockColor = "#CDE9FF";
								whiteThemeTextBoxColor = "#E2F1FF";
								textColorWhite = "#333333";

								dockColorBlack = "#5AC0E7";
								textColorBlack = "#5AC0E7";
									break;
							case "GrayLight":
								whiteThemeDockColor = "#E1DFDD";
								whiteThemeTextBoxColor = "#F3F2F1";
								textColorWhite = "#333333";

								dockColorBlack = "#898989";
								textColorBlack = "#898989";
									break;
							case "Gray":
								whiteThemeDockColor = "#494745";
								whiteThemeTextBoxColor = "#696969";
								textColorWhite = "#FFFFFF";

								dockColorBlack = "#505050";
								textColorBlack = "#FFFFFF";
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

		#region MyRegion
		private string mondayText;
		public string MondayText
		{
			get => mondayText;
			set
			{
				NoteText = value;
				mondayText = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string mondayDate;
		public string MondayDate
		{
			get => mondayDate;
			set
			{
				mondayDate = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string tuesdayText;
		public string TuesdayText
		{
			get => tuesdayText;
			set
			{
				tuesdayText = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string tuesdayDate;
		public string TuesdayDate
		{
			get => tuesdayDate;
			set
			{
				tuesdayDate = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string wednesdayText;
		public string WednesdayText
		{
			get => wednesdayText;
			set
			{
				wednesdayText = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string wednesdayDate;
		public string WednesdayDate
		{
			get => wednesdayDate;
			set
			{
				wednesdayDate = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string thursdayText;
		public string ThursdayText
		{
			get => thursdayText;
			set
			{
				thursdayText = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string thursdayDate;
		public string ThursdayDate
		{
			get => thursdayDate;
			set
			{
				thursdayDate = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string fridayText;
		public string FridayText
		{
			get => fridayText;
			set
			{
				fridayText = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string fridayDate;
		public string FridayDate
		{
			get => fridayDate;
			set
			{
				fridayDate = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string saturdayText;
		public string SaturdayText
		{
			get => saturdayText;
			set
			{
				saturdayText = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string saturdayDate;
		public string SaturdayDate
		{
			get => saturdayDate;
			set
			{
				saturdayDate = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string sundayText;
		public string SundayText
		{
			get => sundayText;
			set
			{
				sundayText = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string sundayDate;
		public string SundayDate
		{
			get => sundayDate;
			set
			{
				sundayDate = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string additionalText;
		public string AdditionalText
		{
			get => additionalText;
			set
			{
				additionalText = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string additionalDate;
		public string AdditionalDate
		{
			get => additionalDate;
			set
			{
				additionalDate = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string additionalHeaderText = "Еще";
		public string AdditionalHeaderText
		{
			get => additionalHeaderText;
			set
			{
				additionalHeaderText = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}
		#endregion


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


		private string textColorBlack = "#E6B905";//#333333
		public string TextColorBlack
		{
			get => textColorBlack;
			set
			{
				textColorBlack = value;
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

		private string noteTextBackgroundColorWhite = "#FFF2AB";
		public string NoteTextBackgroundColorWhite
		{
			get => noteTextBackgroundColorWhite;
			set
			{
				noteTextBackgroundColorWhite = value;
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}

		private string noteTextBackgroundColor = "#FFF7D1";
		public string NoteTextBackgroundColor
		{
			get => noteTextBackgroundColor;
			set
			{
				noteTextBackgroundColor = value;
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
			set => noteText = value;
		}




		private int height= 260;
		public int Height
		{
			get => height;
			set
			{
				height = value;
				//ResizeLastRowAsync();
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}


		private int width=196;
		public int Width
		{
			get => width;
			set
			{
				width = value;
				//ResizeLastRowAsync();
				if (!_saving)
				{
					SaveNoteAsync();
				}
				OnPropertyChanged();
			}
		}


		private int windowX=100;
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


		private int windowY=100;
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
			if (noteWindowWhite != null|| noteWindowBlack!=null)
			{
				UpdateRowHeight();
				await Task.Delay(1000);
				opened = false;
				if (!_saving)
				{
					SaveNoteAsync();
				}

				if (Theme.White == MainModel.CurrentTheme)
				{
					noteWindowWhite.Close();
					noteWindowWhite = null;
				}
				else
				{
					noteWindowBlack.Close();
					noteWindowBlack = null;
				}
				noteModel.NotesOverCheck();
			}
		}

		public void ShowExitingNote()
		{
			if (NoteWindowWhite == null && NoteWindowBlack == null)
			{
				NoteCreator();
			}
		}


		private void NoteCreator()
		{
			if (Theme.White == MainModel.CurrentTheme)
			{
				this.noteWindowWhite = new NoteWindowWhite(this);
				noteWindowWhite.MondayNoteText.TextChanged += (o, s) =>
				{
					if(noteWindowWhite!=null)
						MondayText = noteWindowWhite.MondayNoteText.Text;
				};
				noteWindowWhite.MondayBox.TextChanged += (o, s) =>
				{
					if(noteWindowWhite!=null)
						MondayDate = noteWindowWhite.MondayBox.Text;
				}; 
				noteWindowWhite.TuesdayNoteText.TextChanged += (o, s) =>
				{
					if(noteWindowWhite!=null)
						TuesdayText = noteWindowWhite.TuesdayNoteText.Text;
				};
				noteWindowWhite.TuesdayBox.TextChanged += (o, s) =>
				{
					if(noteWindowWhite!=null)
						TuesdayDate = noteWindowWhite.TuesdayBox.Text;
				};
				noteWindowWhite.WednesdayNoteText.TextChanged += (o, s) =>
				{
					if(noteWindowWhite!=null)
						WednesdayText = noteWindowWhite.WednesdayNoteText.Text;
				};
				noteWindowWhite.WednesdayBox.TextChanged += (o, s) =>
				{
					if(noteWindowWhite!=null)
						WednesdayDate = noteWindowWhite.WednesdayBox.Text;
				};
				noteWindowWhite.ThursdayNoteText.TextChanged += (o, s) =>
				{
					if(noteWindowWhite!=null)
						ThursdayText = noteWindowWhite.ThursdayNoteText.Text;
				};
				noteWindowWhite.ThursdayBox.TextChanged += (o, s) =>
				{
					if(noteWindowWhite!=null)
						ThursdayDate = noteWindowWhite.ThursdayBox.Text;
				};
				noteWindowWhite.FridayNoteText.TextChanged += (o, s) =>
				{
					if(noteWindowWhite!=null)	
						FridayText = noteWindowWhite.FridayNoteText.Text;
				};
				noteWindowWhite.FridayBox.TextChanged += (o, s) =>
				{
					if(noteWindowWhite!=null)
						FridayDate = noteWindowWhite.FridayBox.Text;
				};
				noteWindowWhite.SaturdayNoteText.TextChanged += (o, s) =>
				{
					if(noteWindowWhite!=null)
						SaturdayText = noteWindowWhite.SaturdayNoteText.Text;
				};
				noteWindowWhite.SaturdayBox.TextChanged += (o, s) =>
				{
					if(noteWindowWhite!=null)
						SaturdayDate = noteWindowWhite.SaturdayBox.Text;
				};
				noteWindowWhite.SundayNoteText.TextChanged += (o, s) =>
				{
					if(noteWindowWhite!=null)
						SundayText = noteWindowWhite.SundayNoteText.Text;
				};
				noteWindowWhite.SundayBox.TextChanged += (o, s) =>
				{
					if(noteWindowWhite!=null)
						SundayDate = noteWindowWhite.SundayBox.Text;
				};
				noteWindowWhite.AdditionalNoteText.TextChanged += (o, s) =>
				{
					if(noteWindowWhite!=null)
						AdditionalText = noteWindowWhite.AdditionalNoteText.Text;
				};
				noteWindowWhite.AdditionalBox.TextChanged += (o, s) =>
				{
					if(noteWindowWhite!=null)
						AdditionalDate = noteWindowWhite.AdditionalBox.Text;
				};
				noteWindowWhite.AdditionalNameBox.TextChanged += (o, s) =>
				{
					if(noteWindowWhite!=null)
						AdditionalHeaderText = noteWindowWhite.AdditionalNameBox.Text;
				};
				RestoreRowsHeight();
				noteWindowWhite.StateChanged += (o, s) => 
				{
					if (noteWindowWhite.WindowState == (WindowState)2 || noteWindowWhite.WindowState == (WindowState)1)
					{
						noteWindowWhite.WindowState = (WindowState)0;
					}
				};
				noteWindowWhite.Show();
				opened = true;
			}
			else
			{
				this.noteWindowBlack = new NoteWindowBlack(this);
				noteWindowBlack.MondayNoteText.TextChanged += (o, s) => 
				{ 
					if(noteWindowBlack!=null)
						MondayText = noteWindowBlack.MondayNoteText.Text;
				};
				noteWindowBlack.MondayBox.TextChanged += (o, s) =>
				{
					if(noteWindowBlack!=null)
						mondayDate = noteWindowBlack.MondayBox.Text;
				};
				noteWindowBlack.TuesdayNoteText.TextChanged += (o, s) =>
				{
					if(noteWindowBlack!=null)
						TuesdayText = noteWindowBlack.TuesdayNoteText.Text;
				};
				noteWindowBlack.TuesdayBox.TextChanged += (o, s) =>
				{
					if(noteWindowBlack!=null)
						TuesdayDate = noteWindowBlack.TuesdayBox.Text;
				};
				noteWindowBlack.WednesdayNoteText.TextChanged += (o, s) =>
				{
					if(noteWindowBlack!=null)
						WednesdayText = noteWindowBlack.WednesdayNoteText.Text;
				};
				noteWindowBlack.WednesdayBox.TextChanged += (o, s) =>
				{
					if(noteWindowBlack!=null)
						WednesdayDate = noteWindowBlack.WednesdayBox.Text;
				};
				noteWindowBlack.ThursdayNoteText.TextChanged += (o, s) =>
				{
					if(noteWindowBlack!=null)
						ThursdayText = noteWindowBlack.ThursdayNoteText.Text;
				};
				noteWindowBlack.ThursdayBox.TextChanged += (o, s) =>
				{
					if(noteWindowBlack!=null)
						ThursdayDate = noteWindowBlack.ThursdayBox.Text;
				};
				noteWindowBlack.FridayNoteText.TextChanged += (o, s) =>
				{
					if(noteWindowBlack!=null)
						FridayText = noteWindowBlack.FridayNoteText.Text;
				};
				noteWindowBlack.FridayBox.TextChanged += (o, s) =>
				{
					if(noteWindowBlack!=null)
						FridayDate = noteWindowBlack.FridayBox.Text;
				};
				noteWindowBlack.SaturdayNoteText.TextChanged += (o, s) =>
				{
					if(noteWindowBlack!=null)
						SaturdayText = noteWindowBlack.SaturdayNoteText.Text;
				};
				noteWindowBlack.SaturdayBox.TextChanged += (o, s) =>
				{
					if(noteWindowBlack!=null)
						SaturdayDate = noteWindowBlack.SaturdayBox.Text;
				};
				noteWindowBlack.SundayNoteText.TextChanged += (o, s) =>
				{
					if(noteWindowBlack!=null)
						SundayText = noteWindowBlack.SundayNoteText.Text;
				};
				noteWindowBlack.SundayBox.TextChanged += (o, s) =>
				{
					if(noteWindowBlack!=null)
						SundayDate = noteWindowBlack.SundayBox.Text;
				};
				noteWindowBlack.AdditionalNoteText.TextChanged += (o, s) =>
				{
					if(noteWindowBlack!=null)
						AdditionalText = noteWindowBlack.AdditionalNoteText.Text;
				};
				noteWindowBlack.AdditionalBox.TextChanged += (o, s) =>
				{
					if(noteWindowBlack!=null)
						AdditionalDate = noteWindowBlack.AdditionalBox.Text;
				};
				noteWindowBlack.Additional.TextChanged += (o, s) =>
				{
					if(noteWindowBlack!=null)
						AdditionalHeaderText = noteWindowBlack.Additional.Text;
				};

				RestoreRowsHeight();
				noteWindowBlack.StateChanged += (o, s) => 
				{
					if (noteWindowBlack.WindowState == (WindowState)2 || noteWindowBlack.WindowState == (WindowState)1)
					{
						noteWindowBlack.WindowState = (WindowState)0;
					}
				};
				noteWindowBlack.Show();
				opened = true;
			}
		}

		private async void SaveNoteAsync()
		{
			_saving = true;
			UpdateRowHeight();
			await Task.Delay(20000);
			noteModel.SaveNotes();
			_saving = false;
		}

		private double[] rowsHeigths={ 29 ,40, 40, 40, 40, 40, 40, 40, 40};
		private void RestoreRowsHeight()
		{
			if (Theme.White == MainModel.CurrentTheme)
			{
				noteWindowWhite.MondayRow.Height = new GridLength(rowsHeigths[0], GridUnitType.Star);
				noteWindowWhite.TuesdayRow.Height = new GridLength(rowsHeigths[1], GridUnitType.Star);
				noteWindowWhite.WednesdayRow.Height = new GridLength(rowsHeigths[2], GridUnitType.Star);
				noteWindowWhite.ThursdayRow.Height = new GridLength(rowsHeigths[3], GridUnitType.Star);
				noteWindowWhite.FridayRow.Height = new GridLength(rowsHeigths[4], GridUnitType.Star);
				noteWindowWhite.SaturdayRow.Height = new GridLength(rowsHeigths[5], GridUnitType.Star);
				noteWindowWhite.SundayRow.Height = new GridLength(rowsHeigths[6], GridUnitType.Star);
				noteWindowWhite.AdditionalTextRow.Height = new GridLength(rowsHeigths[7], GridUnitType.Star);
			}
			else
			{
				noteWindowBlack.MondayRow.Height = new GridLength(rowsHeigths[0], GridUnitType.Star);
				noteWindowBlack.TuesdayRow.Height = new GridLength(rowsHeigths[1], GridUnitType.Star);
				noteWindowBlack.WednesdayRow.Height = new GridLength(rowsHeigths[2], GridUnitType.Star);
				noteWindowBlack.ThursdayRow.Height = new GridLength(rowsHeigths[3], GridUnitType.Star);
				noteWindowBlack.FridayRow.Height = new GridLength(rowsHeigths[4], GridUnitType.Star);
				noteWindowBlack.SaturdayRow.Height = new GridLength(rowsHeigths[5], GridUnitType.Star);
				noteWindowBlack.SundayRow.Height = new GridLength(rowsHeigths[6], GridUnitType.Star);
				noteWindowBlack.AdditionalTextRow.Height = new GridLength(rowsHeigths[7], GridUnitType.Star);
			}

		}
		private void UpdateRowHeight()
		{
			if(noteWindowWhite == null && noteWindowBlack == null)
				return;
			if (Theme.White == MainModel.CurrentTheme)
			{
				rowsHeigths[0] = noteWindowWhite.MondayRow.ActualHeight;
				rowsHeigths[1] = noteWindowWhite.TuesdayRow.ActualHeight;
				rowsHeigths[2] = noteWindowWhite.WednesdayRow.ActualHeight;
				rowsHeigths[3] = noteWindowWhite.ThursdayRow.ActualHeight;
				rowsHeigths[4] = noteWindowWhite.FridayRow.ActualHeight;
				rowsHeigths[5] = noteWindowWhite.SaturdayRow.ActualHeight;
				rowsHeigths[6] = noteWindowWhite.SundayRow.ActualHeight;
				rowsHeigths[7] = noteWindowWhite.AdditionalTextRow.ActualHeight;
			}
			else
			{
				rowsHeigths[0] = noteWindowBlack.MondayRow.ActualHeight;
				rowsHeigths[1] = noteWindowBlack.TuesdayRow.ActualHeight;
				rowsHeigths[2] = noteWindowBlack.WednesdayRow.ActualHeight;
				rowsHeigths[3] = noteWindowBlack.ThursdayRow.ActualHeight;
				rowsHeigths[4] = noteWindowBlack.FridayRow.ActualHeight;
				rowsHeigths[5] = noteWindowBlack.SaturdayRow.ActualHeight;
				rowsHeigths[6] = noteWindowBlack.SundayRow.ActualHeight;
				rowsHeigths[7] = noteWindowBlack.AdditionalTextRow.ActualHeight;
			}
		}

		public void SetTheme(Theme theme)
		{
			switch (theme)
				{
					case Theme.White:
						if (opened)
							noteWindowBlack.Close();
                        else
                            break;
						noteWindowBlack = null;
						ShowExitingNote();
						break;
					case Theme.Black:
						if (opened)
							noteWindowWhite.Close();
                        else
                            break;
                        noteWindowWhite = null;
						ShowExitingNote();
						break;
				}
		}
		
	}
}