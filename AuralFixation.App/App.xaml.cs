using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using AuralFixation.Api;

namespace AuralFixation.App
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private System.Windows.Forms.NotifyIcon _tray;

		protected override void OnStartup(StartupEventArgs e)
		{
			Process self = Process.GetCurrentProcess();

			var others = Process.GetProcessesByName(self.ProcessName);

			if (others.Length > 1)
			{
				var other = others.First(x => x.Id != self.Id);
				//Handle.ActivateWindow(other.MainWindowHandle);
				var o = System.Windows.Interop.HwndSource.FromHwnd(other.MainWindowHandle);
				Window window = (Window)o.RootVisual;
				Show(window);

				Application.Current.Shutdown();

				return;
			}

			CreateTrayIcon();

			base.OnStartup(e);
		}

		public void Cleanup(object sender, ExitEventArgs e)
		{
			if (_tray != null) _tray.Icon = null;
		}

		public void CreateTrayIcon()
		{
			var icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetEntryAssembly().Location);

			_tray = new System.Windows.Forms.NotifyIcon
			{
				Icon = icon,
				Visible = true
			};

			_tray.Click += Tray_Click;
		}

		private void Tray_Click(object sender, EventArgs e)
		{
			Show(Application.Current.MainWindow);
			/*
			Application.Current.MainWindow.WindowState = WindowState.Normal;
			Application.Current.MainWindow.Visibility = Visibility.Visible;
			SystemCommands.RestoreWindow(Application.Current.MainWindow);
			Application.Current.MainWindow.Activate();
			*/
		}

		private void Show(Window w)
		{
			w.WindowState = WindowState.Normal;
			w.Visibility = Visibility.Visible;
			SystemCommands.RestoreWindow(w);
			w.Activate();
		}


	}
}
