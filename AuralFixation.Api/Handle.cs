using System;
using System.Runtime.InteropServices;

namespace AuralFixation.Api
{
	public class Handle
	{
		public const int WM_COMMAND = 0x111;
		public const int WM_USER = 0x400;

		[DllImport("user32")] public static extern ushort FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32")] public static extern ushort SendMessage(int hwnd, int wMsg, int wParam, int lParam);

		[DllImport("user32")] public static extern int GetDoubleClickTime();

		private const int ALT = 0xA4;
		private const int EXTENDEDKEY = 0x1;
		private const int KEYUP = 0x2;
		private const int SHOW_MAXIMIZED = 3;
		private const int SW_SHOW = 5;
		private const int SW_RESTORE = 9;

		[DllImport("user32.dll")]
		private static extern bool BringWindowToTop(IntPtr hWnd);
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();
		[DllImport("user32.dll")]
		private static extern IntPtr SetActiveWindow(IntPtr hWnd);
		[DllImport("user32.dll")]
		private static extern bool SetForegroundWindow(IntPtr hWnd);
		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		public static void ActivateWindow(IntPtr mainWindowHandle)
		{
			if (mainWindowHandle == GetForegroundWindow()) return;
			ShowWindow(mainWindowHandle, SW_RESTORE);
			ShowWindow(mainWindowHandle, SW_SHOW);
			SetForegroundWindow(mainWindowHandle);
			BringWindowToTop(mainWindowHandle);
			SetActiveWindow(mainWindowHandle);
		}
	}
}
