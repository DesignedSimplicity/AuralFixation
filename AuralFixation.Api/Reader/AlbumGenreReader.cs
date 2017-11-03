using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AuralFixation.Api.Model;

namespace AuralFixation.Api.Reader
{
	public class AlbumGenreReader : IReader
	{
		private bool _init = false;
		private List<Genre> _genres = new List<Genre>();
		private HashSet<string> _albums = new HashSet<string>();
		private string[] _roots = { @"H:\Music\Albums", @"H:\Music\Albums\_FLAC" };

		public string Key { get { return "AlbumGenreReader"; } }
		public string Name { get { return "Albums by genre"; } }
		public string[] Categories { get { return _genres.Select(x => x.Name).OrderBy(x => x).ToArray(); } }

		public void Init()
		{
			if (_init) return;
			foreach (var root in _roots)
			{
				LoadGenres(root);
			}
			_init = true;
		}

		public List<Media> Pick(string category = "")
		{
			var albums = _albums.ToArray();

			if (!String.IsNullOrEmpty(category))
			{
				var path = Path.DirectorySeparatorChar + category.ToLowerInvariant() + Path.DirectorySeparatorChar;
				albums = _albums.Where(x => x.Contains(path)).ToArray();
			}
			var album = albums[Picker.Pick(albums.Length)];

			Console.WriteLine(album);

			return Media.FromPath(album);
		}

		//================================================================================
		private void LoadGenres(string path)
		{
			var dir = new DirectoryInfo(path);
			foreach (DirectoryInfo genre in dir.GetDirectories())
			{
				var key = genre.Name.ToLowerInvariant();
				if (!key.StartsWith("_"))
				{
					if (!_genres.Any(x => x.Key == key)) _genres.Add(new Genre(genre.Name));
					foreach(var album in genre.GetDirectories())
					{
						_albums.Add(album.FullName.ToLowerInvariant());
					}
				}
			}
		}
	}
}
