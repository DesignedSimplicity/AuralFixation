using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AuralFixation.Api.Media;
using AuralFixation.Api.Model;

namespace AuralFixation.Api.Reader
{
	public class AlbumGenreReader : IReader
	{
		private Cart _cart;

		private List<Genre> _genres = new List<Genre>();
		private HashSet<string> _albums = new HashSet<string>();
		private string[] _roots = { @"H:\Music\Albums", @"H:\Music\Albums\_FLAC" };

		public Cart Cart
		{
			get
			{
				if (_cart == null) Init();
				return _cart;
			}
		}

		public List<MediaFile> Pick(string group = "")
		{
			var root = PickRoot();
			if (String.IsNullOrEmpty(group)) group = PickGenre();

			var path = Path.Combine(root, group) + Path.DirectorySeparatorChar;
			var albums = _albums.Where(x => x.StartsWith(path.ToLowerInvariant())).ToArray();
			var album = albums[Picker.Pick(albums.Length)];

			List<MediaFile> files = new List<MediaFile>();
			
			//TODO-load files for album

			return files;
		}

		//================================================================================
		private void Init()
		{
			foreach (var root in _roots)
			{
				LoadGenres(root);
			}

			_cart = new Cart();
			_cart.Name = "Albums by genre";
			_cart.Groups = _genres.Select(x => x.Name).OrderBy(x => x).ToList();
		}


		private string PickRoot()
		{
			return _roots[Picker.Pick(_roots.Length)];
		}

		private string PickGenre()
		{
			return _genres[Picker.Pick(_genres.Count)].Name;
		}

		private void LoadGenres(string path)
		{
			var dir = new DirectoryInfo(path);
			foreach (DirectoryInfo genre in dir.GetDirectories())
			{
				var key = genre.Name.ToLowerInvariant();
				if (!key.StartsWith("_") && !_genres.Any(x => x.Key == key))
				{
					_genres.Add(new Genre(genre.Name));
					foreach(var album in genre.GetDirectories())
					{
						_albums.Add(album.FullName.ToLowerInvariant());
					}
				}
			}
		}
	}
}
