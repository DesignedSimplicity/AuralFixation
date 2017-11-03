using AuralFixation.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace AuralFixation.App
{
	public class ClickCategory : ICommand
	{
		private static Service _service;
		public static void Init(Service service) { _service = service; }
		public static ClickCategory Command = new ClickCategory();

		private static int _count = 0;
		private static DispatcherTimer _timer =
			new DispatcherTimer(
				new TimeSpan(0, 0, 0, 1), //Handle.GetDoubleClickTime
				DispatcherPriority.Background,
				ClickTick,
				Dispatcher.CurrentDispatcher);

		private static void ClickTick(object sender, EventArgs e)
		{
			_timer.Stop();

			// Handle Single Click Actions
			//Trace.WriteLine("Single Click");
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			//Handle.GetDoubleClickTime
			return true;
		}

		public void Execute(object parameter)
		{
			var test = parameter;
			_count++;
		}

		private void Play(string category, bool reset = false)
		{
			var request = new PlayRequest()
			{
				InCategory = category,
				ResetPlaylist = reset,
			};
			_service.Play(request);
		}
	}

	public class DoubleClickCategory : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			var test = parameter;
		}
	}


	public class SingleClickCategory : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			var test = parameter;
		}
	}


}
