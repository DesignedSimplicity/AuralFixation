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
		protected override void OnStartup(StartupEventArgs e)
		{
			Process self = Process.GetCurrentProcess();

			var others = Process.GetProcessesByName(self.ProcessName);

			if (others.Length > 1)
			{
				var other = others.First(x => x.Id != self.Id);
				Handle.ActivateWindow(other.MainWindowHandle);

				Application.Current.Shutdown();
				return;
			}

			base.OnStartup(e);
		}
	}
}
