using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AuralFixation.Api;

namespace AuralFixation.Win
{
    public partial class Picker : Form
    {
		private Service _service;
		private ImageList _icons;
		private Size _size;

		public Picker()
        {
            InitializeComponent();
        }

		private void Picker_Load(object sender, EventArgs e)
		{
			_service = new Service();

			_icons = new ImageList();
			_size = new Size(256, 256);
			_icons.ImageSize = _size;
			foreach(var file in _service.LoadIcons(@"H:\Music\Albums\__ICONS"))
			{
				_icons.Images.Add(file.Name.ToLower(), Image.FromFile(file.Uri));
			}

			list.BeginUpdate();
			list.ShowGroups = true;
			list.View = View.LargeIcon;
			list.TileSize = _size;
			list.LargeImageList = _icons;
			list.Click += List_Click;
			list.DoubleClick += List_DoubleClick;

			foreach (var reader in _service.ListReaders())
			{
				reader.Init();

				var group = new ListViewGroup(reader.Key, reader.Name);
				list.Groups.Add(group);

				foreach(var category in reader.Categories)
				{
					var item = new ListViewItem();
					item.Group = group;
					item.Name = category;
					item.Text = category;
					item.ImageKey = category.ToLowerInvariant();
					list.Items.Add(item);
				}
			}

			list.EndUpdate();
		}

		private void Play(bool reset = false)
		{
			if (list.SelectedItems.Count == 1)
			{
				var item = list.SelectedItems[0];
				var request = new PlayRequest()
				{
					FromCart = item.Group.Name,
					InCategory = item.Name,
					ResetPlaylist = reset,
				};
				_service.Play(request);
			}
		}

		private void List_Click(object sender, EventArgs e)
		{
			Play(false);
		}

		private void List_DoubleClick(object sender, EventArgs e)
		{
			Play(true);
		}
	}
}
