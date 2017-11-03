﻿using System;
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
			var root = PickRoot();
			if (String.IsNullOrEmpty(category)) category = PickGenre();

			var path = Path.Combine(root, category) + Path.DirectorySeparatorChar;
			var albums = _albums.Where(x => x.StartsWith(path.ToLowerInvariant())).ToArray();
			var album = albums[Picker.Pick(albums.Length)];

			return Media.FromPath(album);			
		}

		//================================================================================
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