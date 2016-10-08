using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MoveWindow.Win32 {
	static class Window {

		//Get Active Window
		[DllImport("user32.dll")]
		internal static extern IntPtr GetForegroundWindow();
		[DllImport("user32.dll")]
		internal static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

		/// <summary>
		/// ウィンドウタイトルの取得
		/// </summary>
		/// <param name="hwnd"></param>
		/// <param name="lpString"></param>
		/// <param name="cch"></param>
		/// <returns></returns>
		[DllImport( "user32.dll" )]
		internal static extern int GetWindowText( IntPtr hwnd, out StringBuilder lpString, int cch );

		[DllImport("user32.dll")]
		internal static extern int GetWindowTextLength(IntPtr hwnd);

		//ウィンドウの移動
		[DllImport("user32.dll")]
		public static extern int MoveWindow(IntPtr hwnd, int x, int y, int w, int h, int bRepaint);

		[DllImport("user32.dll")]
		internal static extern int GetWindowRect(IntPtr hWnd, out RECT rect);

		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct RECT {
			internal readonly int left;
			internal readonly int top;
			internal readonly int right;
			internal readonly int bottom;
		}



	}
}