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

namespace AuralFixation.App
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Service _service;
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

			listView.ItemsSource = _model.Readers[0].Categories;
			items.ItemsSource = _model.Readers[0].Categories;
		}
	}
}
