using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AuralFixation.Api.Actions;

namespace AuralFixation.Api.Media
{
	/// <summary>
	/// Encapsulates physical file system and exposes only folders with media items
	/// </summary>
	public class MediaFolder
    {
		private static LoadMedia _loader = new LoadMedia();

		private DirectoryInfo _dir = null;
		private List<MediaFile> _files = null;
		private List<MediaFolder> _folders = null;

		internal MediaFolder(string uri)
		{
			_dir = new DirectoryInfo(uri);
		}

		public DirectoryInfo Dir { get { return _dir; } }
		public string Uri { get { return _dir.FullName; } }
		public string Name { get { return _dir.Name; } }

		public List<MediaFile> Files
		{
			get
			{
				// already loaded so return results
				if (_files != null) return _files;

				// load and populate cache
				_files = new List<MediaFile>();
				

				return _files;
			}
		}

		public List<MediaFolder> Folders
		{
			get
			{
				// already loaded so return results
				if (_folders != null) return _folders;

				// load and populate cache
				_folders = new List<MediaFolder>();

				return _folders;
			}
		}
	}
}