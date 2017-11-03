using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AuralFixation.Api.Media;

namespace AuralFixation.Api.Actions
{
	public enum MediaFileTypes { mp3, wmv, flac }

	public class LoadMedia
	{
		// local cache of loaded media
		private static Dictionary<string, MediaFolder> _folders = new Dictionary<string, MediaFolder>();
		private static Dictionary<string, MediaFile> _files = new Dictionary<string, MediaFile>();

		// definition of media file types
		private static List<string> _mediaFileTypes;
		private static string _pathString;
		private static char _pathChar;

		static LoadMedia()
		{
			lock (_mediaFileTypes)
			{
				_pathChar = Path.PathSeparator;
				_pathString = Path.PathSeparator.ToString();
				_mediaFileTypes = new List<string>();
				foreach (var t in Enum.GetValues(typeof(MediaFileTypes)))
				{
					_mediaFileTypes.Add("." + t.ToString().ToLowerInvariant());
				}
			}
		}

		//================================================================================
		private string GetFolderKey(string uri)
		{
			return uri.ToLowerInvariant() + (uri.EndsWith(_pathString) ? string.Empty : _pathString);
		}

		private MediaFolder GetFolder(string uri)
		{
			string key = GetFolderKey(uri);
			return (_folders.ContainsKey(key) ? _folders[key] : null);
		}

		private MediaFolder AddFolder(string uri)
		{
			string key = GetFolderKey(uri);
			if (_folders.ContainsKey(key)) throw new Exception($"Folder already exists: {uri}");
			var folder = new MediaFolder(uri);
			_folders.Add(key, folder);
			return folder;
		}

		public MediaFolder FromFolder(string uri)
		{
			var folder = GetFolder(uri);
			if (folder != null) return folder;
			return AddFolder(uri);
		}

		//================================================================================
		private MediaFile GetFile(string uri)
		{
			string key = uri.ToLowerInvariant();
			return (_files.ContainsKey(key) ? _files[key] : null);
		}

		private MediaFile AddFile(string uri)
		{
			string key = uri.ToLowerInvariant();
			if (_files.ContainsKey(key)) throw new Exception($"File already exists: {uri}");
			var file = new MediaFile(uri);
			_files.Add(key, file);
			return file;
		}

		public MediaFile FromFile(string uri)
		{
			var file = GetFile(uri);
			if (file != null) return file;
			return AddFile(uri);
		}
	}
}
