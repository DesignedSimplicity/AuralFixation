using System.Runtime.InteropServices;

namespace AuralFixation.Api
{
	public class Handle
	{
		public const int WM_COMMAND = 0x111;
		public const int WM_USER = 0x400;

		[DllImport("user32")] public static extern ushort FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32")] public static extern ushort SendMessage(int hwnd, int wMsg, int wParam, int lParam);
	}
}
