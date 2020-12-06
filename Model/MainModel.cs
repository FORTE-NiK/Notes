using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ForteNotes.ViewModel;

namespace ForteNotes.Model
{
    [Serializable]
    class MainModel
    {
        private static Theme _currentTheme=Theme.Black;
        public static Theme CurrentTheme => _currentTheme;
        public static void SetTheme(Theme theme)
        {
            _currentTheme = theme;
            NoteModel.ChangeTheme(theme);
        }

        public static void SaveSettings()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "saveSettings.notes");
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, _currentTheme);
            }
        }

        public static void RestoreSettings()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "saveSettings.notes");
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                _currentTheme = (Theme)binaryFormatter.Deserialize(stream);
            }
        }
    }
}
