using ForteNotes.Model;
using ForteNotes.View.MainWindow.MainButtons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForteNotes.ViewModel
{
	public class SettingsViewModel : ViewModelBase
	{
		private NotesAppSettings notesAppSettings;
		public SettingsViewModel(NotesAppSettings notesAppSettings)
		{
			this.notesAppSettings = notesAppSettings;
		}

		private static bool _whiteThemeButton;
		public bool WhiteThemeButton
		{
			get => _whiteThemeButton;
			set
			{
				if (_whiteThemeButton == value)
					return;
				_whiteThemeButton = value;
				_blackThemeButton = false;
				MainModel.SetTheme(Theme.White);
				MainModel.SaveSettings();
				OnPropertyChanged();
			}
		}

		private static bool _blackThemeButton;
		public bool BlackThemeButton
		{
			get=> _blackThemeButton;
			set
			{
				if (_blackThemeButton == value)
					return;
				_blackThemeButton = value;
				_whiteThemeButton = false;
				MainModel.SetTheme(Theme.Black);
				MainModel.SaveSettings();
				OnPropertyChanged();
			}
		}

		public static void SetSwitches()
        {
            if (MainModel.CurrentTheme == Theme.White)
            {
				_whiteThemeButton = true;
				_blackThemeButton = false;
            }
            else
            {
				_whiteThemeButton = false;
				_blackThemeButton = true;
            }
        }
	}
}
