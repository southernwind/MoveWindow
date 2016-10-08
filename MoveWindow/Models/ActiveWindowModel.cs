using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Threading;
using MoveWindow.Win32;

namespace MoveWindow.Models {
	/// <summary>
	/// このアプリケーション以外で最後にアクティブだったウィンドウ
	/// </summary>
	class ActiveWindowModel : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		private IntPtr _myWindowHandle;

		private readonly DispatcherTimer _dispatcherTimer;
		public ActiveWindowModel() {
			this._myWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
			this._dispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal);
			this._dispatcherTimer.Interval = new TimeSpan( 0, 0, 0, 0, 100 ); //0.1秒毎
			this._dispatcherTimer.Tick += UpdateActiveWindowHandle;
			this._dispatcherTimer.Start();
		}

		~ActiveWindowModel() {
			this._dispatcherTimer.Stop();
		}

		private string _name;
		public string Name {
			get {
				return this._name;
			}
			set {
				this._name = value;
				OnPropertyChanged( nameof( this.Name ) );
			}
		}

		/// <summary>
		/// ウィンドウハンドル
		/// </summary>
		private IntPtr _windowHandle;
		public IntPtr WindowHandle {
			get {
				return this._windowHandle;
			}
			private set {
				this._windowHandle = value;

				int processId;
				Window.GetWindowThreadProcessId( this.WindowHandle, out processId );
				this._process = Process.GetProcessById( processId );
				this.Name = this._process.ProcessName;
				OnPropertyChanged( nameof( this.WindowHandle ) );
			}
		}

		private Process _process;

		public void Move(int x, int y) {

			Window.RECT rect;
			Window.GetWindowRect( this.WindowHandle, out rect);
			var w = rect.right - rect.left;
			var h = rect.bottom - rect.top;
			Window.MoveWindow( this.WindowHandle, x, y, w, h, 1 );

		}

		private void UpdateActiveWindowHandle(object sender, EventArgs e) {
			if( this._myWindowHandle == IntPtr.Zero ) {
				this._myWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
				if( this._myWindowHandle == IntPtr.Zero ) {
					return;
				}
			}
			var fgWindow = Window.GetForegroundWindow();
			if(fgWindow != this._myWindowHandle && fgWindow != this.WindowHandle) {
				this.WindowHandle = fgWindow;
			}
		}

		private void OnPropertyChanged(string name) {
			var handler = this.PropertyChanged;
			handler?.Invoke( this, new PropertyChangedEventArgs( name ) );
		}
	}
}
