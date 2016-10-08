using System.ComponentModel;
using System.Windows.Input;
using MoveWindow.Models;
using MoveWindow.ViewModels.Commands;

namespace MoveWindow.ViewModels {
	class MainWindowViewModel : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		private ICommand _move;
		public ICommand Move {
			get {
				return this._move ?? ( this._move = new MoveCommand( this ) );
			}
		}

		public MainWindowViewModel() {
			this.ActiveWindow = new ActiveWindowModel();
		}

		#region ActiveWindowModel 変更通知プロパティ

		private ActiveWindowModel _activeWindow;
		public ActiveWindowModel ActiveWindow {
			get {
				return this._activeWindow;
			}
			set {
				this._activeWindow = value;
				OnPropertyChanged( nameof( this.ActiveWindow ) );
			}
		}
		#endregion

		private void OnPropertyChanged(string name) {
			var handler = this.PropertyChanged;
			handler?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
