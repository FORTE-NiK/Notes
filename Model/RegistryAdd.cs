using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ForteNotes.Model
{
    sealed class RegistryAdd
    {
        public static void AddToAutorun()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            try
            {
                key.SetValue("ForteNotes", Assembly.GetExecutingAssembly().Location);
                key.Flush();
                key.Close();
            }
            catch { }
        }
    }
}
