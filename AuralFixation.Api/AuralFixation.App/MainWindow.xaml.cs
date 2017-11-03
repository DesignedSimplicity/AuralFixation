using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using AuralFixation.Api;
using System.Windows.Threading;

namespace AuralFixation.App
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private static Service _service;
		private PlayerTree _model;

		public MainWindow()
		{
			InitializeComponent();

			BuildViewModel();
		}

		public void BuildViewModel()
		{
			_service = new Service();

			_model = new PlayerTree();

			foreach (var reader in _service.ListReaders())
			{
				reader.Init();

				var r = new ReaderNode(reader);
				_model.Readers.Add(r);

				foreach (var category in reader.Categories)
				{
					var uri = System.IO.Path.Combine(@"H:\Music\Albums\__ICONS", category + ".jpg");
					var i = new BitmapImage(new Uri(uri));
					var c = new CategoryNode(category, i);
					r.Categories.Add(c);
				}
			}

			items.ItemsSource = _model.Readers[0].Categories;

			
		}


		private static string _category = "";
		private void Click(object sender, RoutedEventArgs e)
		{
			var btn = (Button)e.Source;
			_category = btn.Tag.ToString();
			_timer.Start();
		}

		private void DoubleClick(object sender, MouseButtonEventArgs e)
		{
			_timer.Stop();

			Play(_category, true);

			e.Handled = true;
		}

		private static void Play(string category, bool reset = false)
		{
			var request = new PlayRequest()
			{
				FromCart = _service.ListReaders().First().Key,
				InCategory = category,
				ResetPlaylist = reset,
			};
			_service.Play(request);
		}

		private static DispatcherTimer _timer =
			new DispatcherTimer(
				new TimeSpan(0, 0, 0, 0, Handle.GetDoubleClickTime()),
				DispatcherPriority.Background,
				ClickTick,
				Dispatcher.CurrentDispatcher);

		private static void ClickTick(object sender, EventArgs e)
		{
			_timer.Stop();

			if (!String.IsNullOrWhiteSpace(_category)) Play(_category);
		}
	}
}
