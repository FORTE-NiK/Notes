using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ForteNotes.ViewModel
{
	[Serializable]
	public class CommandCL : ICommand
	{
		private Action<object> execute;
		private Func<object, bool> canExecute;
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		public CommandCL(Action<object> execute, Func<object, bool> canExecute = null)
		{
			this.execute = execute;
			this.canExecute = canExecute;
		}
		[DebuggerHidden]
		public bool CanExecute(object parameter)
		{
			return this.canExecute == null || this.canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			this.execute(parameter);
		}
	}
}
