using System;
using System.Windows.Input;

namespace MoveWindow.ViewModels.Commands {
	class MoveCommand : ICommand {
		private readonly MainWindowViewModel _vm;

		public MoveCommand(MainWindowViewModel viewmodel) {
			this._vm = viewmodel;
		}

		public bool CanExecute(object parameter) {
			return this._vm.ActiveWindow.WindowHandle != IntPtr.Zero;
		}

		public event EventHandler CanExecuteChanged {
			add {
				CommandManager.RequerySuggested += value;
			}
			remove {
				CommandManager.RequerySuggested -= value;
			}
		}

		public void Execute(object parameter) {
			this._vm.ActiveWindow.Move( 0, 0 );
		}
	}
}